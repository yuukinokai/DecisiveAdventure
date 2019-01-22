using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class PlayerData
{

    public string sceneName;

    public int health;
    public string heroName;
    public int attack;
    public int defense;
    public int dex;
    public int luck;

    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public Dictionary<string, int> party = new Dictionary<string, int>();

    public PlayerData(Player player, string sName)
    {
        sceneName = sName;
        health = player.GetHealth();
        heroName = player.GetName();
        attack = player.GetAttack();
        defense = player.GetDefense();
        dex = player.GetDex();
        luck = player.GetLuck();

        inventory = player.GetFullInventory();

        party = player.GetPersistentParty();


    }
}
