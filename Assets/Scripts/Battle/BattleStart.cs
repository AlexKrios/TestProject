using UnityEngine;

public class BattleStart : MonoBehaviour
{
    private readonly Group group = new Group();
    private int allCount = 0;

    public UnitStatus[] CreateArmy(string team) 
    {
        UnitStatus[] army = new UnitStatus[6];

        int count = 0;

        Transform parent = GameObject.Find($"{team}Team").GetComponent<Transform>();
        Vector3[] unitPosition = InitPositionArray(team);
        Quaternion unitRotation = InitRotationArray(team);

        foreach (string member in group.member)
        {
            UnitStatus unit = CreateUnit(member, team);

            if (unit != null) {
                unit.gameObject.name = $"{team}{count + 1}";
                unit.gameObject.transform.parent = parent;
                unit.gameObject.transform.position = unitPosition[count];
                unit.gameObject.transform.rotation = unitRotation;
            }

            army[count] = unit;
            allCount++;
            count++;
        }

        return army;
    }

    UnitStatus CreateUnit(string member, string team) 
    {
        string path = $"Сharacters/{member}/{member}";

        if (member == "Empty") {
            return null;
        }

        UnitStatus unit = new UnitStatus();

        GameObject unitGameObject = Instantiate(Resources.Load(path, typeof(GameObject)) as GameObject);
        Personage unitClass = unitGameObject.GetComponent<Personage>();

        /* Unit stats export */
        unit.gameObject = unitGameObject;
        unit.gameObject.tag = team;
        unit.status = "Live";
        unit.turn = true;
        unit.team = team;
        unit.place = allCount;
        unit.level = unitClass.level;
        unit.hp = unitClass.hp;
        unit.currentHp = unitClass.hp;
        unit.attack = unitClass.attack;
        unit.defence = unitClass.defence;
        unit.initiative = unitClass.initiative;
        unit.type = unitClass.type;

        return unit;
    }    

    private Vector3[] InitPositionArray(string team)
    {
        Vector3[] unitPosition = new Vector3[6];

        if (team == "Ally") {
            unitPosition[0] = new Vector3(-3, 1, 3);
            unitPosition[1] = new Vector3(-3, 1, 0);
            unitPosition[2] = new Vector3(-3, 1, -3);
            unitPosition[3] = new Vector3(-6, 1, 3);
            unitPosition[4] = new Vector3(-6, 1, 0);
            unitPosition[5] = new Vector3(-6, 1, -3);
        }

        if (team == "Enemy") {
            unitPosition[0] = new Vector3(3, 1, 3);
            unitPosition[1] = new Vector3(3, 1, 0);
            unitPosition[2] = new Vector3(3, 1, -3);
            unitPosition[3] = new Vector3(6, 1, 3);
            unitPosition[4] = new Vector3(6, 1, 0);
            unitPosition[5] = new Vector3(6, 1, -3);
        }
        
        return unitPosition;
    }

    private Quaternion InitRotationArray(string team)
    {
        Quaternion unitRotation = new Quaternion();

        if (team == "Ally")
        {
            unitRotation = Quaternion.Euler(0, 90, 0);
        }

        if (team == "Enemy")
        {
            unitRotation = Quaternion.Euler(0, -90, 0);
        }

        return unitRotation;
    }
}
