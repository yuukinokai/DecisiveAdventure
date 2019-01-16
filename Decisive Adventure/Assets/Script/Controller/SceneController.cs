﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	enum GameState { Intro, Dialog, Move, EOD, Transition, Done, GameOver, Checkpoint};
	[SerializeField] private GameObject[] checkpoints;
	[SerializeField] private DialogueController dialogController;
	[Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f; 
	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private Animator transitionAnim;
	[SerializeField] private string sceneName;
	[SerializeField] private string gameOverSceneName = "gameOver";

	private GameState gameState;
	private int checkpointIndex = 0;
	private GameObject player;
	private Rigidbody2D rigidBody2D;
	private Vector3 _Velocity = Vector3.zero;

	IEnumerator GOver()
	{
		yield return new WaitForSeconds(5f);
		transitionAnim.SetTrigger("GameOver");
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(gameOverSceneName);
	}

	IEnumerator Intro()
	{
		yield return new WaitForSeconds(2f);
		if(checkpoints.Length > 0){
			gameState = GameState.Move;
		}
		else{
			gameState = GameState.Dialog;
			dialogController.NextDialog();
		}
	}

	IEnumerator Transit()
	{
		transitionAnim.SetTrigger("Transition");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(sceneName);
	}

	void Start(){
		dialogController = DialogueController.GetController();
		gameState = GameState.Intro;
		StartCoroutine(Intro());
	}

    void Awake ()
    {
		player = GameObject.Find("Player");
		if(player == null) return;
		rigidBody2D = player.GetComponent<Rigidbody2D>();
    }
	
    void OnEnable ()
    {
		EventManager.StartListening("Done Dialog", DoneDialog);
		EventManager.StartListening("Checkpoint", CheckpointReached);
		EventManager.StartListening("GameOver", GameOver);
		EventManager.StartListening("DoneCheckpoint", DoneCheckpoint);
    }

	void GameOver(){
		gameState = GameState.GameOver;
		StartCoroutine(GOver());
	}

	void CheckpointReached(){
		if(gameState != GameState.Move){
			Debug.Log("ERROR: GameState was not Move");
			return;
		}
		rigidBody2D.velocity = Vector3.zero;
		Debug.Log("Checkpoint reached");
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
		if(gameState != GameState.Dialog){
			Debug.Log("ERROR: GameState was not Dialog");
			return;
		}
		Debug.Log ("Dialog is done");
		if(checkpoints.Length == 0){
			gameState = GameState.Transition;
		}
		else if(checkpointIndex == checkpoints.Length){
			gameState = GameState.EOD;
		}
		else{
			gameState = GameState.Move;
		}
	}

	void Update(){
		if(gameState == GameState.Transition){
			Debug.Log("Transition here.");
			gameState = GameState.Done;
			StartCoroutine(Transit());
		}
		if(gameState == GameState.Move){
			//Debug.Log("Add Speed here.");
			
			Vector3 targetVelocity = new Vector2(moveSpeed * 10f, rigidBody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref _Velocity, MovementSmoothing);
		}
	}
}