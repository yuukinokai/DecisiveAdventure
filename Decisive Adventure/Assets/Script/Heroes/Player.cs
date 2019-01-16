using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Hero {

	static private Player playerInstance = null;
	static private int numberInstance = 0;

	public Dictionary<BaseItem, int> inventory;
	public List<Hero> party;

	public Player GetPlayer(){
		if(playerInstance == null){
			if(numberInstance != 0){
				Debug.Log("Error : Player instance more than 1.");
			}
			playerInstance = this;
			numberInstance++;
		}
		return playerInstance;
	}

	void Start(){
		playerInstance = this;
		numberInstance++;
	}
}
