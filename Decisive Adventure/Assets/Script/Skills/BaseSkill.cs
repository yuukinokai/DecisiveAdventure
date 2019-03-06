using UnityEngine;
using System.Collections;

public abstract class BaseSkill
{

    public Hero character = null;

    public virtual float GetSkillTriggerChance()
    {
        return 1.0f;
    }

    public virtual bool IsPassive()
    {
        return false;
    }

    public virtual Hero GetCharacter()
    {
        return character;
    }

    public virtual void SetCharacter(Hero hero)
    {
        character = hero;
    }

    public virtual bool TriggerPreAttack(Hero attacker, Hero defender)
    {
        return false;
    }

    public virtual bool TriggerAttack(Hero attacker, Hero defender, ref bool skipAttack)
    {
        return false;
    }

    public virtual bool TriggerDamageCalc(Hero attacker, Hero defender, ref int damage)
    {
        return false;
    }

    public virtual bool TriggerPostAttack (Hero attacker, Hero defender)
    {
        return false;
    }

    public virtual bool TriggerEnd(Hero attacker, Hero defender)
    {
        return false;
    }

}
