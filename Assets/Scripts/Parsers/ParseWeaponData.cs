using System.Collections.Generic;
using Units.Objects.Weapon;
using UnityEngine;

public class ParseWeaponData : MonoBehaviour
{
    public List<WeaponObject> Parse()
    {
        var textAsset = Resources.Load<TextAsset>("Data/Units/Hero/Weapon") as TextAsset;
        var weaponList = JsonUtility.FromJson<WeaponList>(textAsset.text);

        return weaponList.Weapons; 
    }
}
