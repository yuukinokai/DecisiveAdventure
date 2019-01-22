using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : Hero
{
    void Awake()
    {
        heroName = "Fairy";
        health = 1;
        attack = 0;
        skillTrigger = 10;
    }
}
