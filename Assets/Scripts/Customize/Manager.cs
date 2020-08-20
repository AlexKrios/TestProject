using System;
using UnityEngine;

namespace Customize
{
    public class Manager : MonoBehaviour
    {
        public static Manager Instance;

        public GameObject spawnPoint;
        [NonSerialized] public GameObject unit;
        [NonSerialized] public GameObject weapon;
        [NonSerialized] public GameObject armor;

        /* Sub module */
        public CreateManager createManager;

        public readonly Group group = new Group();

        private void Start()
        {
            Instance = this;

            createManager.UnitCreate(group.allyTeam[0]);
        }
    }
}
