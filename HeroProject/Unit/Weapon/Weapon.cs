using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic
{
    //TODO : можно передавать цель один раз, когда включается модуль атаки
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField, Min(0)]
        protected float _splashRadius;

        protected WeaponStats _weaponStats;
        protected UnitData _ownerUnitData;

        public abstract void Attack(in UnitData target);

        protected void WeaponStart()
        {
            _ownerUnitData = GetComponent<UnitData>();
            _weaponStats = _ownerUnitData.WeaponStats;
        }
    }
}