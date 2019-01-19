using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    enum State { Dialog, Choice, Response, Wait , Giving, Notice};

    static private DialogueController thisInstance = null;
    static private int instanceNumber = 0;

    [SerializeField] private Text textDisplay;                  //main display for dialog text
    [SerializeField] private Dialog[] dialog;                   //array of all the dialogs   
    [SerializeField] private GameObject continueButton;         //click area to continue text
    [SerializeField] private Text[] choiceDisplay;              //array of text display for choices
    [SerializeField] private GameObject[] choiceButtons;        //array of buttons for choices

    private State oldState;
    private string noticeMessage = "";
    private State currentState;               //current state machine for dialog
    private int dialogIndex = 0;              //index of the scene dialog
    private int sentenceIndex = 0;            //index of sentence within dialog
    private int responseIndex = 0;            //index of sentence for the response
    private int responseNumber = 0;           //response clicked by player
    private Dialog currentDialogue;           //dialog being player now
    private List<int> repeatedTimes = new List<int>();          //list of responses clicked by player

    IEnumerator Type(string sentence)
    {
        //type sentence letter by letter
        foreach (char letter in sentence)
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    static public DialogueController GetController(){
        if(thisInstance == null){
            Debug.Log("Error : No instance of Controller.");
        }
        return thisInstance;
    }

    void Awake(){
        thisInstance = this;
        instanceNumber++;
       // Debug.Log("Set Controller");
    }
    //initialize
    void Start()
    {
        //all buttons start disabled
        foreach (GameObject button in choiceButtons)
        {
            button.SetActive(false);
        }
        continueButton.SetActive(false);
        dialogIndex = 0;
        //Debug.Log("DIALOG CONTOLLER: State Wait");
        currentState = State.Wait;
        oldState = State.Wait;
        if(dialog.Length == 0) return;
        currentDialogue = dialog[0];        
    }

    //update controls the continue button
    void Update()
    {
        if (currentState == State.Dialog && currentDialogue.sentences.Length > sentenceIndex
            && textDisplay.text == currentDialogue.sentences[sentenceIndex])
        {
            if (sentenceIndex >= currentDialogue.sentences.Length - 1 && currentDialogue.choices.Length > 0)
            {
                DisplayChoices();
            }
            else
            {
                continueButton.SetActive(true);
            }
        }

        if(currentState == State.Notice && textDisplay.text == noticeMessage)
        {
            //continueButton.SetActive(true);
        }
        //Debug.Log("DIALOG INDEX " + dialogIndex);

        /*if(currentState == State.Response)
        {
            Debug.Log("DIALOG CONTOLLER: responseNumber " + responseNumber);
            Debug.Log("DIALOG CONTOLLER: responseIndex " + responseIndex);
            Debug.Log("DIALOG CONTOLLER: " + (currentDialogue.responses[responseNumber].sentences.Length > responseIndex));
            Debug.Log("DIALOG CONTOLLER: " + (textDisplay.text == currentDialogue.responses[responseNumber].sentences[responseIndex]));

        }*/
        if (currentState == State.Response && currentDialogue.responses.Length > responseNumber 
            && currentDialogue.responses[responseNumber].sentences.Length > responseIndex && textDisplay.text == currentDialogue.responses[responseNumber].sentences[responseIndex])
        {
            continueButton.SetActive(true);
            //Debug.Log("DIALOG CONTOLLER: Continue active");
        }
    }

    /// <summary>
    /// This method starts the next Dialogue of the scene
    /// </summary>
    public void NextDialog()
    {
        //Debug.Log("DIALOG CONTOLLER: State Dialog");
        currentState = State.Dialog;
        if(dialogIndex < dialog.Length)
        {
            sentenceIndex = 0;
            responseIndex = 0;
            responseNumber = 0;
            repeatedTimes.Clear();
            currentDialogue = dialog[dialogIndex];
            textDisplay.text = "";
            StartCoroutine(Type(currentDialogue.sentences[sentenceIndex]));
            continueButton.SetActive(false);
            dialogIndex++;
        }
    }

    /// <summary>
    /// Displays the choices of the dialog
    /// </summary>
    public void DisplayChoices()
    {
        //Debug.Log("DIALOG CONTOLLER: State Choice");
        currentState = State.Choice;
        continueButton.SetActive(false);
        
        int buttonIndex = 0;
        foreach(string choice in currentDialogue.choices)
        {
            //Debug.Log("DIALOG CONTOLLER: Activating buttons");
            choiceButtons[buttonIndex].SetActive(true);
            choiceDisplay[buttonIndex++].text = choice;
        }
    }

    public void GiveResponse()
    {
        foreach(Dialog response in currentDialogue.responses)
        {
            response.isGiveOrSell = false;
        }

        DisplayResponse(0);
 
    }

    /// <summary>
    /// Displays the response of a choice
    /// </summary>
    /// <param name="responseChoice"></param>
    public void DisplayResponse(int responseChoice)
    {
        foreach (GameObject button in choiceButtons)
        {
            button.SetActive(false);                      //turn buttons off
        }

        EventManager.TriggerEvent("Button" + responseChoice);

        if (currentDialogue.responses.Length > responseChoice && currentDialogue.responses[responseChoice].isGiveOrSell)
        {
            currentState = State.Giving;
            textDisplay.text = "";
            StartCoroutine(Type("What do you not need?"));
            EventManager.TriggerEvent("Giving");
            return;
        }
  
        //Debug.Log("DIALOG CONTOLLER: State Response");
        currentState = State.Response;
        textDisplay.text = "";

        //adds to the list of "clicked" choices if not seen before
        if (!repeatedTimes.Contains(responseChoice))
        {
            repeatedTimes.Add(responseChoice);
        }

        responseNumber = responseChoice;
        responseIndex = 0;
        if (responseIndex > currentDialogue.responses.Length) return;
        if (currentDialogue.responses[responseNumber].sentences.Length <= 0) return;
        StartCoroutine(Type(currentDialogue.responses[responseNumber].sentences[responseIndex]));
    }

    void CheckWait(){
        textDisplay.text = "";
        if(dialogIndex < dialog.Length && dialog[dialogIndex].startRightAway){
           NextDialog();
        }
        else{
            //Debug.Log("DIALOG CONTOLLER: State Wait");
            EventManager.TriggerEvent ("Done Dialog");
            currentState = State.Wait;
        }
    }

    public void Notice(string notify)
    {
        oldState = currentState;
        currentState = State.Notice;
        noticeMessage = notify;
        textDisplay.text = "";
        StartCoroutine(Type(notify));
    }

    /// <summary>
    /// This method starts the next sentence of the dialog
    /// </summary>
    public void NextSentence()
    {
        if(currentState == State.Notice)
        {
            currentState = oldState;
            NextSentence();
        }
        if(currentState == State.Giving)
        {
            textDisplay.text = "";
            CheckWait();
        }

        if(currentState == State.Dialog)
        {
            //Debug.Log("click");
            continueButton.SetActive(false);
            if (sentenceIndex >= currentDialogue.sentences.Length - 1)
            {
                if(currentDialogue.choices.Length == 0){
                    CheckWait();
                }
            }
            else
            {
                textDisplay.text = "";

                if (sentenceIndex < currentDialogue.sentences.Length - 1 && currentDialogue.sentences[sentenceIndex].Length > 0)
                {
                    sentenceIndex++;
                    StartCoroutine(Type(currentDialogue.sentences[sentenceIndex]));
                }
            }
        }

        else if(currentState == State.Response)
        {
            continueButton.SetActive(false);
            textDisplay.text = "";

            if (currentDialogue.responses.Length > responseNumber 
                && responseIndex < currentDialogue.responses[responseNumber].sentences.Length - 1)
            {
                responseIndex++;
                StartCoroutine(Type(currentDialogue.responses[responseNumber].sentences[responseIndex]));
            }
            else
            {
                if (currentDialogue.repeat && repeatedTimes.Count < currentDialogue.responses.Length)
                {
                    DisplayChoices();
                }
                else
                {
                    CheckWait();
                }
            }
        }
    }

    public void OverrideDialog(Dialog d){
        dialog[dialogIndex] = d;
        Debug.Log("DIALOG CONTROLLER : Replaced Dialog");
    }

}
