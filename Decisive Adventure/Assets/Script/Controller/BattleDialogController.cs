using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleDialogController : MonoBehaviour
{

    [SerializeField] private Text textDisplay;
    [SerializeField] private Button btnNext;


    private static BattleDialogController _instance;
    private BattleDialogManager _managerInstance;


    public volatile bool forceBreak;

    enum State { Ready, Typing , Waiting};

    private State currentState;

    public static BattleDialogController GetInstance()
    {
        if (_instance == null)
        {
            Debug.LogError("No instance of BattleDialogController");
        }
        return _instance;
    }

    public BattleDialogManager GetManager()
    {
        if (_managerInstance == null)
        {
            Debug.LogError("No instance of BattleDialogManager");
        }
        return _managerInstance;
    }

    public void Process(BattleDialogType type, string message)
    {

        StartCoroutine(Type(type, message));
    }

    IEnumerator Type(BattleDialogType type, string sentence)
    {
        if (currentState == State.Typing)
        {
            forceBreak = true;
            yield return new WaitUntil (() => currentState == State.Ready);
            ResetState();
            // skip event trigger
            forceBreak = false;
        } else if (currentState == State.Waiting) { }
        {
            ResetState();

        }

        this.currentState = State.Typing;
        //type sentence letter by letter
        foreach (char letter in sentence)
        {
            if (this.forceBreak == true)
            {
                this.currentState = State.Ready;
                yield break;
            }
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        TriggerEvent(type);
    }

    void TriggerEvent(BattleDialogType type)
    {
        switch (type)
        {
            case BattleDialogType.Next:
                EventManager.TriggerEvent("BattleDialogSimple");
                break;
            case BattleDialogType.Simple:
                EventManager.TriggerEvent("BattleDialogNext");
                break;
            default:
                break;

        }
    }


    void HandleBattleDialogSimple()
    {
        ResetState();
        EventManager.TriggerEvent("BattleDialogControllerReady");
    }

    void HandleBattleDialogNext()
    {
        this.currentState = State.Waiting;
        btnNext.gameObject.SetActive(true);
        EventManager.TriggerEvent("BattleDialogControllerWaiting");
    }

    void HandleBtnNextClick()
    {
        ResetState();
        EventManager.TriggerEvent("BattleDialogControllerReady");
    }

    void ResetState()
    {
        btnNext.gameObject.SetActive(false);
        this.currentState = State.Ready;
    }

    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start()
    {
        _managerInstance = new BattleDialogManager();
        ResetState();
        btnNext.onClick.AddListener(HandleBtnNextClick);
        EventManager.TriggerEvent("BattleDialogControllerReady");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
