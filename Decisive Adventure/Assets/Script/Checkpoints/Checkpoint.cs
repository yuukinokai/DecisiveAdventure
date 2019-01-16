using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	protected GameObject player;
	protected Rigidbody2D rigidBody2D;
	protected Vector3 _Velocity = Vector3.zero;
	[SerializeField] protected Dialog _Dialog;
	protected DialogueController dialogController;

   	virtual protected void OnTriggerEnter2D(Collider2D other)
    {
      	if (other.gameObject.CompareTag("Player")) {
        	EventManager.TriggerEvent ("Checkpoint");
	 		Debug.Log("check");
			if(_Dialog != null && dialogController != null){
				dialogController.OverrideDialog(_Dialog);
			}
			EventManager.TriggerEvent ("DoneCheckpoint");
		}
    }

	protected void Start(){
		player = GameObject.Find("Player");
		if(player == null) return;
		rigidBody2D = player.GetComponent<Rigidbody2D>();
		dialogController = DialogueController.GetController();
	}

    protected void OnEnable ()
    {
        EventManager.StartListening ("Button0", Event1);
		EventManager.StartListening ("Button1", Event2);
		EventManager.StartListening ("Button2", Event3);
		EventManager.StartListening ("Button3", Event4);
    }

	virtual protected void Event1(){
		Debug.Log ("Button 0!");
	}
	virtual protected void Event2(){
		Debug.Log ("Button 1!");
	}
	virtual protected void Event3(){
		Debug.Log ("Button 2!");
	}
	virtual protected void Event4(){
		Debug.Log ("Button 3!");
	}
}
