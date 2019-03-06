using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Hero
{
    void Awake()
    {
        heroName = "Monkey";
        health = 3;
        attack = 0;
        luck = 0;   

        // add skill to character
        BaseSkill monkeyOneShot = new MonkeyOneShot();
        monkeyOneShot.SetCharacter(this);
        this.AddSkill(monkeyOneShot);
    }
}
