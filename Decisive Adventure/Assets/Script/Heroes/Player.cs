using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] protected string heroName = "Hero";
    [SerializeField] protected int health = 1;
    [SerializeField] protected int attack = 1;
    [SerializeField] protected int defense = 1;
    [SerializeField] protected int dex = 1;
    [SerializeField] protected int luck = 1;

    static private Player playerInstance = null;
	static private int numberInstance = 0;

	public Dictionary<string, int> inventory = new Dictionary<string, int>();
	public List<Hero> party;

	static public Player GetPlayer(){
		if(playerInstance == null){
			Debug.Log("Error : No player instance.");	
		}
		return playerInstance;
	}

	void Awake(){
		playerInstance = this;
		numberInstance++;
	}

	public bool AddItem(string itemName){
		if(inventory == null) return false;
		int amount;
		Debug.Log("AddItem : " +  itemName);
		if(inventory.TryGetValue(itemName, out amount)){
            amount++;
			inventory.Remove(itemName);
        }
        else{
			amount = 1;
        }
		inventory.Add(itemName, amount);
		return true;
	}

	public bool RemoveItem(string itemName){
		if(inventory == null) return false;
		int amount;
		Debug.Log("RemoveItem : " +  itemName);
		if(inventory.TryGetValue(itemName, out amount)){
            amount--;
			inventory.Remove(itemName);
			if(amount > 0) inventory.Add(itemName, amount);
        }
        else{
			Debug.Log("Player did not have item " + itemName);
        }
		return true;
	}

	public bool HasItem(string itemName){
		if(inventory == null) return false;
		int amount;
		if(inventory.TryGetValue(itemName, out amount)){
			Debug.Log("Found " + itemName + " " + amount);
			if(amount <= 0) {
				inventory.Remove(itemName);
				return false;
			}
			return true;
        }
		return false;
	}
    public int GetAmount(string itemName)
    {
        if (inventory == null) return 0;
        int amount;
        if (inventory.TryGetValue(itemName, out amount))
        {
            Debug.Log("Found " + itemName + " " + amount);
            
            return amount;
        }
        return 0;
    }

	public List<string> GetInventory(){
		if(inventory == null) return null;
        //return inventory;
		return new List<string>(this.inventory.Keys);
	}

    public List<Hero> GetParty()
    {
        return party;
    }

    public void PartyJoin(Hero hero)
    {
        party.Add(hero);
        Debug.Log(hero.GetName() + " joined you.");
    }
}
