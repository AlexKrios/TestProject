using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Personage
{
    public Warrior() 
    {
        hp = 50;
        attack = 25;
        attackType = "melee";
        defence = 0;
        initiative = 10;
        type = "warrior";
    }
}
