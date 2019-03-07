using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Hero {

    /*[SerializeField] protected string heroName = "Hero";
    [SerializeField] protected int health = 1;
    [SerializeField] protected int attack = 1;
    [SerializeField] protected int defense = 1;
    [SerializeField] protected int dex = 1;
    [SerializeField] protected int luck = 1;
<<<<<<< HEAD
    */
    [SerializeField] protected int coin = 0;

    static private Player playerInstance = null;

    [SerializeField] protected Dictionary<string, int> inventory = new Dictionary<string, int>();
    [SerializeField] protected List<Hero> party;

	static public Player GetPlayer(){
		if(playerInstance == null){
			Debug.Log("Error : No player instance.");	
		}
		return playerInstance;
	}

    private void Start()
    {
        if (!party.Contains(this))
        {
            PartyJoin(this);
        }
    }

    void Awake(){
		playerInstance = this;
	}

    public Dictionary<string, int> GetFullInventory()
    {
        return inventory;
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

    public void AddCoin(int i)
    {
        coin += i;
    }
    public void LoseCoin(int i)
    {
        coin -= i;
        coin = Mathf.Max(coin, 0);
    }
    public int GetCoin()
    {
        return coin;
    }

    public Dictionary<string, int> GetPersistentParty()
    {
        Dictionary<string, int> persistentParty = new Dictionary<string, int>();

        foreach(Hero h in party)
        {
            persistentParty.Add(h.GetName(), h.GetLoyalty());
        }

        return persistentParty;
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null) return;
        health = data.health;
        heroName = data.heroName;
        attack = data.attack;
        defense = data.defense;
        dex = data.dex;
        luck = data.luck;
        inventory = data.inventory;
        coin = data.coin;
        
        int index = 0;
        if(data.party.Count == 0)
        {
            return;
        }
        foreach(string hero in data.party.Keys)
        {
            Hero ally;
            if(string.Compare(hero, "Monkey") == 0)
            {
                ally = GameObject.FindGameObjectsWithTag(hero)[0].GetComponent<Monkey>();
            }
            else if (string.Compare(hero, "Dog") == 0)
            {
                ally = GameObject.FindGameObjectsWithTag(hero)[0].GetComponent<Dog>();
            }
            else if (string.Compare(hero, "Fairy") == 0)
            {
                ally = GameObject.FindGameObjectsWithTag(hero)[0].GetComponent<Fairy>();
            }
            else if (string.Compare(hero, "BigDemon") == 0)
            {
                ally = GameObject.FindGameObjectsWithTag(hero)[0].GetComponent<BigDemon>();
            }
            else if (string.Compare(hero, "SmallDemon") == 0)
            {
                ally = GameObject.FindGameObjectsWithTag(hero)[0].GetComponent<SmallDemon>();
            }
            else
            {
                ally = null;
                Debug.Log("Saved an unknown hero");
                index++;
                continue;
            }

            int loyalty = 5;
            if (data.party.TryGetValue(hero, out loyalty))
            {
                Debug.Log("Found " + hero + " " + loyalty);
            }
            ally.SetLoyalty(loyalty);
            PartyJoin(ally);

            index++;
        }
    }

}
