using System;
using System.Collections.Generic;
using UnityEngine;
using Units.Objects.Unit;
using Units.Objects.Weapon;
using Units.Objects.Armor;

namespace Customize
{
    public class Manager : MonoBehaviour
    {
        public static Manager Instance;

        public GameObject spawnPoint;
        [NonSerialized] public GameObject unit;
        [NonSerialized] public GameObject weapon;
        [NonSerialized] public GameObject armor;

        [NonSerialized] public List<UnitObject> unitList;
        [NonSerialized] public List<WeaponObject> weaponList;
        [NonSerialized] public List<ArmorObject> armorList;

        /* Sub module */
        public CreateManager createManager;
        public ParseManager parseManager;

        public UnitSelector unitSelector;
        public SelectorHandler selectorHandler;

        private void Awake()
        {
            Instance = this;

            unitList = parseManager.parseUnitData.Parse();
            weaponList = parseManager.parseWeaponData.Parse();
            armorList = parseManager.parseArmorData.Parse();
        }

        private void Start()
        {
            createManager.CreateUnit(unitList[0]);
            unitSelector.CreateUnitSelector();
        }
    }
}
