using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "UnitData")]
public class UnitData : ScriptableObject
{
    public int hp;
    public int attack;
    public string attackType;
    public int defence;
    public int initiative;
    public string type;
}
