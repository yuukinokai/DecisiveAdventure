using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class BattleController : MonoBehaviour
{

    public enum BattleStage {PreAttack, Attack, DamageCalc, PostAttack, End };
    public enum BattleTurn { Ally, Opponent };

    private IBaseCharacter ally;
    private IBaseCharacter enemy;

    private IBaseCharacter attacker;
    private IBaseCharacter defender;

    public List<ISkill> SkillsList;


    private List<IBaseCharacter> party;
    private BattleTurn CurrentTurn;
    private bool battleActive;

    public BattleController(List<IBaseCharacter> party, IBaseCharacter enemy)
    {
        this.party = party;

        // determine the ally

        this.ally = party[0];
        this.enemy = enemy;

        // create new list with enemy inside to avoid special case at the end
        List<IBaseCharacter> all = new List<IBaseCharacter>(party);
        all.Add(enemy);

        // add all the skills of all the characters inside their respective lists
        foreach (IBaseCharacter character in all)
        {
            List < ISkill > skills = character.GetSkills();
            foreach(ISkill skill in skills)
            {
                if ((skill.FighterRequired() && character == ally) || (!skill.FighterRequired()))
                {
                    SkillsList.Add(skill);
                }
            }
        }

        this.battleActive = true;

    }

    public IBaseCharacter GetAttacker()
    {
        return CurrentTurn == BattleTurn.Ally ? ally : enemy;
    }

    public IBaseCharacter GetDefender()
    {
        return CurrentTurn == BattleTurn.Ally ? ally : enemy;
    }

    private void DoPreAttack()
    {
        SkillsList.ForEach(skill => skill.TriggerPreAttack(attacker, defender));
    }

    private void DoAttack()
    {
        SkillsList.ForEach(skill => skill.TriggerAttack(attacker, defender));
        // attackanimation()
    }

    private void DoDamageCalc()
    {
        int damage = ComputeNetDamage();
        SkillsList.ForEach(skill => skill.TriggerDamageCalc(attacker, defender, ref damage));
        //actually take damage()
    }

    private void DoPostAttack()
    {
        SkillsList.ForEach(skill => skill.TriggerPostAttack(attacker, defender));

    }

    private void DoEnd()
    {
        SkillsList.ForEach(skill => skill.TriggerEnd(attacker, defender));
        CurrentTurn = (CurrentTurn == BattleTurn.Ally) ? BattleTurn.Opponent : BattleTurn.Ally;
        IBaseCharacter temp = attacker;
        attacker = defender;
        defender = temp;

        //check if anyone died
        if (ally.GetHealth() == 0 || enemy.GetHealth() == 0)
        {
            battleActive = false;
        }
    }


    public void Battle()
    {
        while(battleActive)
        {
            DoPreAttack();
            DoAttack();
            DoDamageCalc();
            DoPostAttack();
            DoEnd();
        }

    }

    private int ComputeNetDamage()
    {
        int attack = attacker.GetAttack();
        int defense = defender.GetDefense();
        return attack - Mathf.Max(defense - 5, 0);
    }


}

