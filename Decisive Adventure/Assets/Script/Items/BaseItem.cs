using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseItem : MonoBehaviour {

	[SerializeField] protected string itemName = "Unknown item";
	[SerializeField] protected Sprite spriteName;
	[SerializeField] protected int worth = 1;
	
	public string GetName(){
		return itemName;
	}

	public int GetWorth(){
		return worth;
	}

	public Sprite GetSprite(){
		return spriteName;
	}
}
