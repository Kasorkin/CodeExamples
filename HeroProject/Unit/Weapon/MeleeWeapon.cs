using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic
{
    public class MeleeWeapon : Weapon
    {
        public override void Attack(in UnitData target)
        {
            //анимация
            //Debug.Log("Атака оружием");
            Damage damage = new Damage(_ownerUnitData, _weaponStats.GetSummaryDamage);
            target.DamageHandler.Handler(damage);
        }

        private void Start()
        {
            WeaponStart();
        }
    }
}