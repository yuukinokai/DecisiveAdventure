using UnityEngine;
using System.Collections;

public interface ISkill
{
    BattleController.BattleStage GetActivationStage();

    BattleController.BattleTurn GetActivationTurn();

    bool FighterRequired();

    IBaseCharacter GetCharacter();

    bool TriggerPreAttack(IBaseCharacter attacker, IBaseCharacter defender);
    bool TriggerAttack(IBaseCharacter attacker, IBaseCharacter defender);
    bool TriggerDamageCalc(IBaseCharacter attacker, IBaseCharacter defender, ref int damage);
    bool TriggerPostAttack(IBaseCharacter attacker, IBaseCharacter defender);
    bool TriggerEnd(IBaseCharacter attacker, IBaseCharacter defender);


}

