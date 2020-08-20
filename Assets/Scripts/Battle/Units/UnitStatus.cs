using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Units
{
    public class UnitStatus
    {
        public GameObject parent;
        public GameObject gameObject;
        
        public GameObject armor;
        public GameObject weapon;
        public GameObject face;

        public Text hpText;

        public string status;
        public bool turn;

        public string team;

        public int place;

        public List<int> target;

        public string name;

        public int level;
        public int expirence;
        public int currentExpirence;

        public int hp;
        public int currentHp;

        public int attack;
        public int aimCount;
        public string attackType;

        public int defence;

        public int initiative;
    }
}