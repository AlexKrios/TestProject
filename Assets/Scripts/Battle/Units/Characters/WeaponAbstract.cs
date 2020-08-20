using System.Collections;
using UnityEngine;

namespace Battle.Units.Characters
{    
    public abstract class WeaponAbstract : MonoBehaviour, IAim
    {
        public int aimCount;

        public virtual IEnumerator Aim() 
        {
            yield return null;
        }
    }
}
