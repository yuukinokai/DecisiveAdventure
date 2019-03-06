using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Hero
{
    void Awake()
    {
        heroName = "Dog";
        health = 2;
        attack = 0;
    }
}
