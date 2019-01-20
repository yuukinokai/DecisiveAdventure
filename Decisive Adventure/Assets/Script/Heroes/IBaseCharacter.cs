using System;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseCharacter
{

    string GetName();

    void AddLoyalty();

    int GetLoyalty();

    int GetHealth();

    int GetSkillChance();

    int GetAttack();

    int GetDefense();

    List<ISkill> GetSkills();

}
