using UnityEngine;
using System.Collections;

public class MonkeyOneShot : BaseSkill
{
    private bool triggered = false;

    public override float GetSkillTriggerChance()
    {
        Hero player = Player.GetPlayer();
        return player.GetLuck();
    }

    // Use this for initialization
    public override bool TriggerPreAttack(Hero attacker, Hero defender)
    {
        if (attacker == GetCharacter())
        {
            if (Random.Range(0f, 1.0f) <= GetSkillTriggerChance())
            {
                // call dialog controller to display text of skill activated
                // set defender's health to 0
                triggered = true;
            }
        }
        return false;
    }

    public override bool TriggerAttack(Hero attacker, Hero defender, ref bool skipAttack)
    {
        if (triggered)
        {
            skipAttack |= true;
            return true;
        }
        return false;
    }

}
