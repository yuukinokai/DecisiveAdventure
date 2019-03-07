using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class BattleController : MonoBehaviour
{
    public enum State { Idle, Battle1, Battle2, End }

    public enum BattleStage { PreAttack, Attack, DamageCalc, PostAttack, End };
    public enum BattleTurn { Ally, Opponent };


    public List<BaseSkill> SkillsList = new List<BaseSkill>();

    private DialogueController dialogueController;
    private List<Hero> party;
    private BattleTurn currentTurn;
    private State currentState;
    private bool battleActive;

    private Hero ally;
    private Hero enemy;


    // @TODO: create ui element to choose the attacker
    // @TODO: integrate battle dialog controller with this

    public BattleController(List<Hero> party, Hero enemy)
    {
        this.party = party;
    }


    public void Start() // (List<Hero> party, Hero enemy )
    {
        this.dialogueController = DialogueController.GetController();

        //this.party = party;
        //this.enemy = enemy;

        // create new list with enemy inside to avoid special case at the end
        List<Hero> all = new List<Hero>();
        all.AddRange(party);
        all.Add(enemy);

        // add all the skills of all the characters inside their respective lists
        foreach (Hero character in all)
        {
            List <BaseSkill> skills = character.GetSkills();
            foreach(BaseSkill skill in skills)
            {
                SkillsList.Add(skill);
            }
        }

        this.battleActive = true;
        this.currentState = State.Idle;
    }

    public void SelectCharacter(Hero ally, BattleTurn starting = BattleTurn.Ally)
    {
        this.ally = ally;
        this.currentTurn = starting;
        dialogueController.ClearDialog();

        this.currentState = State.Battle1;
    }

    public Hero GetAttacker()
    {
        return currentTurn == BattleTurn.Ally ? ally : enemy;
    }

    public Hero GetDefender()
    {
        return currentTurn == BattleTurn.Ally ? ally : enemy;
    }

    private void DoPreAttack()
    {
        SkillsList.ForEach(skill => {
            bool remove = skill.TriggerPreAttack(GetAttacker(), GetDefender());
            if (remove)
            {
                SkillsList.Remove(skill);
            }
        });
    }

    private void DoAttack()
    {
        bool skipAttack = false;
        SkillsList.ForEach(skill => {
            bool remove = skill.TriggerAttack(GetAttacker(), GetDefender(), ref skipAttack);
            if (remove)
            {
                SkillsList.Remove(skill);
            }
        });
        if (!skipAttack)
        {

        }
        // attackanimation()
    }

    private void DoDamageCalc()
    {
        int damage = ComputeNetDamage();
        SkillsList.ForEach(skill => {
            bool remove = skill.TriggerDamageCalc(GetAttacker(), GetDefender(), ref damage);
            if (remove)
            {
                SkillsList.Remove(skill);
            }
        });
        //actually take damage()
    }

    private void DoPostAttack()
    {
        SkillsList.ForEach(skill => {
            bool remove = skill.TriggerPostAttack(GetAttacker(), GetDefender());
            if (remove)
            {
                SkillsList.Remove(skill);
            }
        });

    }

    private void DoEnd()
    {
        SkillsList.ForEach(skill => {
            bool remove = skill.TriggerEnd(GetAttacker(), GetDefender());
            if (remove)
            {
                SkillsList.Remove(skill);
            }
        });

        currentTurn = (currentTurn == BattleTurn.Ally) ? BattleTurn.Opponent : BattleTurn.Ally;

        //check if anyone died
        if (ally.GetHealth() == 0 || enemy.GetHealth() == 0)
        {
            battleActive = false;
        }
    }


    public void Battle()
    {

        DoPreAttack();
        DoAttack();
        DoDamageCalc();
        DoPostAttack();
        DoEnd();

        if (battleActive)
        {
            if (currentState == State.Battle1)
            {
                currentState = State.Battle2;
            } else if (currentState == State.Battle2)
            {
                currentState = State.Idle;
            }
            else
            {
                // euhh not supposed to be thereee
            }
        }
        else
        {
            // end battle scene
        }

    }

    private int ComputeNetDamage()
    {
        int attack = GetAttacker().GetAttack();
        int defense = GetDefender().GetDefense();
        return attack - Mathf.Max(defense - 5, 0);
    }


}

