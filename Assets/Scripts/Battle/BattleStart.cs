using UnityEngine;

public class BattleStart : MonoBehaviour
{
    private readonly Group group = new Group();

    private string _team;

    private int _allCount = 0;
    private int _count = 0;

    void Start() { }

    public UnitStatus[] CreateArmy(string team) 
    {
        UnitStatus[] army = new UnitStatus[5];
        _team = team;
        _count = 0;

        foreach (string member in group.member)
        {
            var unit = CreateUnit(member);

            army[_count] = unit;

            _allCount++;
            _count++;
        }

        return army;
    }

    private UnitStatus CreateUnit(string member) 
    {
        string path = $"Battle/Сharacters/{member}/{member}";

        if (member == "Empty") {
            return null;
        }

        UnitStatus unit = new UnitStatus();

        var unitParent = GameObject.Find($"{_team}{_count + 1}");
        var unitGameObject = Instantiate(Resources.Load(path, typeof(GameObject)) as GameObject);
        var unitClass = unitGameObject.GetComponent<Personage>();

        unitGameObject.name = unitClass.type;
        unitGameObject.transform.parent = unitParent.transform;
        unitGameObject.transform.position = unitParent.transform.position;
        unitGameObject.transform.rotation = InitRotationArray();

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
        unit.defence = unitClass.defence;
        unit.initiative = unitClass.initiative;
        unit.type = unitClass.type;

        return unit;
    }

    private Quaternion InitRotationArray()
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
