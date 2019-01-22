using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
	[SerializeField] protected string heroName = "Hero";
	[SerializeField] protected int health = 1;
	[SerializeField] protected int attack = 1;
    [SerializeField] protected int loyalty = 5;
    [SerializeField] protected int skillTrigger = 0;

    public string GetName()
    {
        return heroName;
    }

    public void AddLoyalty()
    {
        loyalty++;
    }

    public int GetLoyalty()
    {
        return loyalty;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetSkillChance()
    {
        return skillTrigger;
    }

    public int GetAttack()
    {
        return attack;
    }
    

}
