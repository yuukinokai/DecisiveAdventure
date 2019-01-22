using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
	[SerializeField] protected string heroName = "Hero";
	[SerializeField] protected int health = 1;
	[SerializeField] protected int attack = 1;
    [SerializeField] protected int defense = 0;
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

    public void SetLoyalty(int newLoyalty)
    {
        loyalty = newLoyalty;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }

    public int GetSkillChance()
    {
        return skillTrigger;
    }

    public void SetSkillChance(int newSkillChance)
    {
        skillTrigger = newSkillChance;
    }

    public int GetAttack()
    {
        return attack;
    }

    public void SetAttack(int newAttack)
    {
        attack = newAttack;
    }

    public int GetDefense()
    {
        return defense;
    }

    public void SetDefense(int newDefense)
    {
        defense = newDefense;
    }

    public List<ISkill> GetSkills()
    {
        return null;
    }

}
