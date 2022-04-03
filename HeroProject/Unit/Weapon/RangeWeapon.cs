using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic
{
    public sealed class RangeWeapon : Weapon
    {
        [Header("������� ���")]
        [SerializeField]
        private GameObject _rangeAnim;

        public override void Attack(in UnitData target)
        {
            //��������
            //Debug.Log("����� �������");
            Damage damage = new Damage(_ownerUnitData, _weaponStats.GetSummaryDamage);
            target.DamageHandler.Handler(damage);
        }

        private void Start()
        {
            WeaponStart();
        }
    }
}