using System.Collections.Generic;
using Units.Objects.BattleUnit;
using UnityEngine;

namespace Parsers
{
    public class ParseBattleUnitData : MonoBehaviour
    {
        public List<BattleUnitObject> Parse(string dataPath)
        {
            var textAsset = Resources.Load(dataPath) as TextAsset;
            var bodyList = JsonUtility.FromJson<BattleUnitList>(textAsset.text);

            return bodyList.BattleUnits;
        }
    }
}