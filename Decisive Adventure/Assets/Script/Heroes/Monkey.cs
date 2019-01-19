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
        skillTrigger = 1;
    }
}
