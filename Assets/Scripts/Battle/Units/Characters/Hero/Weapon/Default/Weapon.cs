using Battle.Units.Characters;
using System.Collections;
using UnityEngine;

namespace Battle.Units.Hero.Weapon.Default
{
    public class Weapon : WeaponAbstract
    {
        [SerializeField] private GameObject[] _aimPoints = new GameObject[4];

        private void Start() { }

        public override IEnumerator Aim()
        {
            var pathBullet = "Battle/Сharacters/Hero/Weapon/Default/Bullet";

            for (int i = 0; i < _aimPoints.Length; i++)
            {
                var bullet = Instantiate(Resources.Load(pathBullet, typeof(GameObject)) as GameObject);                
                bullet.gameObject.transform.position = _aimPoints[i].transform.position;
                yield return new WaitForSeconds(.05f);
            }
        }
    }
}
