using System;
using Units.Objects.Armor;
using Units.Objects.Weapon;

namespace Units.Objects.Unit
{
    [Serializable]
    public class UnitObject
    {
        public string Type;
        public string PathModel;

        public int Level;
        public int Expirence;
        public int CurrentExpirence;

        public int Hp;
        public int CurrentHp;

        public int Attack;
        public string AttackType;

        public int Defence;
        public int Initiative;

        public WeaponObject Weapon;
        public ArmorObject Armor;
    }
}
