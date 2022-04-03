using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic
{
    //TODO : нужно ли наследовать от оружия или лучше от дальнего?
    public class AreaRangeWeapon : Weapon
    {
        [Header("Дальний бой")]
        [SerializeField]
        private GameObject _rangeAnim;
        [SerializeField]
        private GameObject _areaAnim;

        public override void Attack(in UnitData target)
        {
            RaycastHit2D[] raycasts = Physics2D.CircleCastAll(target.transform.position, _splashRadius, Vector2.zero);

            foreach (var k in raycasts)
            {
                if (k.transform.TryGetComponent(out UnitData unit) && unit.Owner.Fraction != _ownerUnitData.Owner.Fraction)
                    Handler(unit);
            }
        }

        private void Handler(in UnitData target)
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