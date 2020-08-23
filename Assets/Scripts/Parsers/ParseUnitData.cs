using System.Collections.Generic;
using Units.Objects.Unit;
using UnityEngine;

public class ParseUnitData : MonoBehaviour
{
    public List<UnitObject> Parse()
    {
        var textAsset = Resources.Load("Data/Units/UnitList") as TextAsset;
        var bodyList = JsonUtility.FromJson<UnitList>(textAsset.text);

        return bodyList.Units;
    }
}
