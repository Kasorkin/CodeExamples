using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic
{
    public class AreaMeleeWeapon : Weapon
    {
        public override void Attack(in UnitData target)
        {
            Vector2 centrPos = CalculateCentrArea(target.transform.position);

            RaycastHit2D[] raycasts = Physics2D.CircleCastAll(centrPos, _splashRadius, Vector2.zero);

            foreach (var k in raycasts)
            {
                if (k.transform.TryGetComponent(out UnitData unit) && unit.Owner.Fraction != _ownerUnitData.Owner.Fraction)
                    Handler(unit);
            }
        }

        private void Handler(in UnitData target)
        {
            //��������
            //Debug.Log("����� �������");
            Damage damage = new Damage(_ownerUnitData, _weaponStats.GetSummaryDamage);
            target.DamageHandler.Handler(damage);
        }

        private Vector2 CalculateCentrArea(in Vector2 targetPos)
        {
            var heading = targetPos - (Vector2)transform.position;

            var k = _splashRadius / Mathf.Sqrt(heading.x * heading.x + heading.y * heading.y);
            return k * heading;
        }

        private void Start()
        {
            WeaponStart();
        }
    }
}