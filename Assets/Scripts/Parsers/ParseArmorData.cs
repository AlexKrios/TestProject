using System.Collections.Generic;
using Units.Objects.Armor;
using UnityEngine;

public class ParseArmorData : MonoBehaviour
{
    public List<ArmorObject> Parse()
    {
        var textAsset = Resources.Load<TextAsset>("Data/Units/Hero/Weapon") as TextAsset;
        var armorList = JsonUtility.FromJson<ArmorList>(textAsset.text);

        return armorList.Armors;
    }
}
