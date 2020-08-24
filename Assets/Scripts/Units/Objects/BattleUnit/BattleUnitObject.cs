using System;
using System.Collections.Generic;
using Units.Objects.Unit;
using UnityEngine;

namespace Units.Objects.BattleUnit
{
    [Serializable]
    public class BattleUnitObject
    {
        public GameObject Parent;
        public GameObject UnitGO;

        public GameObject Weapon;
        public GameObject Armor;
        public GameObject Face;

        public int AimCount;        

        public string Status;
        public bool Turn;

        public int Id;
        public string Team;
        public int Place;

        public List<int> Target;

        public UnitObject Unit;
    }
}
