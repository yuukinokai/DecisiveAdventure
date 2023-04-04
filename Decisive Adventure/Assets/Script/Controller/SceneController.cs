using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class SceneController : MonoBehaviour {

    public GameObject audioObject;

	enum GameState { Intro, Dialog, Move, EOD, Transition, Done, GameOver, Checkpoint, Giving, Selling};
	[Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f; 
	[SerializeField] private GameObject[] checkpoints;
	private DialogueController dialogController;
	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private Animator transitionAnim;
	[SerializeField] private string sceneName;
	[SerializeField] private string gameOverSceneName = "gameOver";
	private UIController uiController;

	private GameState gameState;
	private int checkpointIndex = 0;
	private GameObject player;
	private Rigidbody2D rigidBody2D;
	private Vector3 _Velocity = Vector3.zero;
    private Boolean audioSet = false;


    IEnumerator GOver()
	{
		yield return new WaitForSeconds(5f);
		transitionAnim.SetTrigger("GameOver");
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(gameOverSceneName);
	}

	IEnumerator EOD()
	{
		yield return new WaitForSeconds(2f);
		gameState = GameState.EOD;
	}

	IEnumerator Intro()
	{   
		yield return new WaitForSeconds(2f);
		if(checkpoints.Length > 0){
			gameState = GameState.Move;
		}
		else{
			gameState = GameState.Dialog;
            Debug.Log("Started with dialog");
			dialogController.NextDialog();
		}
        if (audioObject != null)
        {
            AudioSource audioData = audioObject.GetComponent<AudioSource>();
            audioData.Play(0);
            Debug.Log("audio started");
            audioSet = true;
        }
    }

    void FadeIn()
    {
        if (audioObject != null)
        {
            AudioSource audioData = audioObject.GetComponent<AudioSource>();
            if (audioData.volume < 0.5)
            {
                audioData.volume += 1 * Time.deltaTime;
                print(audioData.volume + "goingin");
            }
        }       
    }

    void Start(){
		dialogController = DialogueController.GetController();
        uiController = UIController.GetController();
        gameState = GameState.Intro;
        player = GameObject.Find("Player");
        if (player == null) return;
        rigidBody2D = player.GetComponent<Rigidbody2D>();
        Player playerData = player.GetComponent<Player>();
        if (playerData != null )
        {
            if(string.Compare(SceneManager.GetActiveScene().name, "scene1") != 0 && string.Compare(SceneManager.GetActiveScene().name, "introduction") != 0)
            {
                playerData.LoadPlayer();
                Debug.Log("PlayerDataLoaded");
            }
            
        }
        else
        {
            Debug.Log("ERROR : No playerdata found");
        }
        StartCoroutine(Intro());
    }

    void Awake()
    {
		
    }
	
    void OnEnable ()
    {
		EventManager.StartListening("Done Dialog", DoneDialog);
		EventManager.StartListening("Checkpoint", CheckpointReached);
		EventManager.StartListening("GameOver", GameOver);
		EventManager.StartListening("DoneCheckpoint", DoneCheckpoint);
        EventManager.StartListening("Giving", Giving);
        EventManager.StartListening("DoneGiving", DoneGiving);
        EventManager.StartListening("DoneNotice", DoneGiving);
    }

    private void DoneGiving()
    {
        gameState = GameState.Dialog;
        dialogController.NextSentence();
    }

    private void Giving()
    {
        gameState = GameState.Giving;
        uiController.DisplayGive();
    }

    void GameOver(){
		gameState = GameState.GameOver;
		StartCoroutine(GOver());
	}

	void CheckpointReached(){
		if(gameState != GameState.Move){
			Debug.Log("ERROR: GameState was not Move. It was " + gameState);
			return;
		}
		rigidBody2D.velocity = Vector3.zero;
		//Debug.Log("Checkpoint reached");
		gameState = GameState.Checkpoint;
	}

	void DoneCheckpoint(){
		if(gameState != GameState.Checkpoint){
			Debug.Log("ERROR: GameState was not Checkpoint");
			return;
		}
		gameState = GameState.Dialog;
		dialogController.NextDialog();
		checkpointIndex++;
	}

	void DoneDialog(){
		if(gameState != GameState.Dialog && gameState != GameState.Giving)
        {
			Debug.Log("ERROR: GameState was not Dialog or Giving");
			return;
		}
		//Debug.Log ("Dialog is done");
		if(checkpoints.Length == 0){
			gameState = GameState.Transition;
		}
		else{
			gameState = GameState.Move;
		}
	}

	void Update(){
        if (audioSet && audioObject != null)
        {
            AudioSource audioData = audioObject.GetComponent<AudioSource>();
            if (audioData.volume <= 1)
            {
                FadeIn();
            }
        }
        
        if (gameState != GameState.Giving)
        {
            uiController.HideGive();
        }
		if(gameState == GameState.Transition){
			Debug.Log("Transition here.");
			gameState = GameState.Done;
            transitionAnim.SetTrigger("Transition");
            SceneManager.LoadScene(sceneName);
        }
		if(gameState == GameState.Move){
			//Debug.Log("Add Speed here.");
			
			Vector3 targetVelocity = new Vector2(moveSpeed * 10f, rigidBody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref _Velocity, MovementSmoothing);

			if(checkpointIndex == checkpoints.Length){
				StartCoroutine(EOD());
			}
		}

		if(gameState == GameState.EOD){
			Debug.Log("Calling EOD");
            Player playerData = player.GetComponent<Player>();
            if (playerData != null)
            {
                SaveSystem.SavePlayer(playerData, sceneName);
            }
            else
            {
                Debug.Log("ERROR : No playerdata found");
            }
            uiController.DisplayInventory();
        }
		else{
			uiController.HideInventory();
		}
	}

	public void EndDay(){
        uiController.HideInventory();
		gameState = GameState.Transition;
	}
}