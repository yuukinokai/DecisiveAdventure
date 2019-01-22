using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDemon : Hero
{
    void Awake()
    {
        heroName = "BigDemon";
        health = 3;
        attack = 3;
        skillTrigger = 10;
    }
}
