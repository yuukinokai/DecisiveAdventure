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
    [SerializeField] protected int luck = 0;
    [SerializeField] protected int dex = 0;
    [SerializeField] protected List<BaseSkill> skillList = new List<BaseSkill>();

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

    public int GetLuck()
    {
        return luck;
    }

    public void SetLuck(int newLuck)
    {
        luck = newLuck;
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

    public int GetDex()
    {
        return dex;
    }

    public void SetDex(int newDex)
    {
        dex = newDex;
    }

    public List<BaseSkill> GetSkills()
    {
        return skillList;
    }

    public void AddSkill(BaseSkill skill)
    {
        this.skillList.Add(skill);
    }

    public void RemoveSkill(BaseSkill skill)
    {
        this.skillList.Remove(skill);
    }

}
