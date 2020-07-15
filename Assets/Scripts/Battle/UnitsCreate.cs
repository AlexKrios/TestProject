using Battle.Units;
using Battle.Units.Behaviour;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{    
    public class UnitsCreate : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }
        private readonly Group _group = new Group();

        private string _team;

        private int _allCount = 0;
        private int _count;

        private void Start() { }

        public List<UnitStatus> CreateArmy(string team)
        {
            List<UnitStatus> army = new List<UnitStatus>();
            _count = 0;
            _team = team;            

            foreach (string member in _group.member)
            {
                var unit = CreateUnit(member);

                army.Add(unit);

                _allCount++;
                _count++;
            }

            return army;
        }

        private UnitStatus CreateUnit(string member)
        {
            string path = $"Battle/Сharacters/{member}";

            if (member == "Empty")
            {
                return null;
            }

            UnitStatus unit = new UnitStatus();

            var unitParent = GameObject.Find($"{_team}{_count + 1}");
            var unitGameObject = Instantiate(Resources.Load(path, typeof(GameObject)) as GameObject);
            var unitClass = unitGameObject.GetComponent<Unit>();

            unitGameObject.name = unitClass.type;
            unitGameObject.transform.parent = unitParent.transform;
            unitGameObject.transform.position = unitParent.transform.position;
            unitParent.transform.rotation = InitRotation();

            var unitBody = unitGameObject.transform.Find("Body").gameObject;
            var unitArmor = unitGameObject.transform.Find("Armor").gameObject;
            var unitGunRight = unitGameObject.transform.Find("GunRight").gameObject;
            var unitGunLeft = unitGameObject.transform.Find("GunLeft").gameObject;

            /* Unit stats export */
            unit.parent = unitParent;
            unit.gameObject = unitGameObject;
            unit.gameObject.tag = _team;

            unit.body = unitBody;
            unit.armor = unitArmor;
            unit.gunRight = unitGunRight;
            unit.gunLeft = unitGunLeft;

            unit.status = "Live";
            unit.turn = true;
            unit.team = _team;
            unit.place = _allCount;
            unit.hp = unitClass.hp;
            unit.currentHp = unitClass.hp;
            unit.attack = unitClass.attack;
            unit.attackType = unitClass.attackType;
            unit.defence = unitClass.defence;
            unit.initiative = unitClass.initiative;

            return unit;
        }

        private Quaternion InitRotation()
        {
            Quaternion unitRotation = new Quaternion();

            if (_team == "Ally")
            {
                unitRotation = Quaternion.Euler(0, 90, 0);
            }

            if (_team == "Enemy")
            {
                unitRotation = Quaternion.Euler(0, -90, 0);
            }

            return unitRotation;
        }
    }
}
