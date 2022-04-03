using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic
{
    public delegate void RefAction<T>(ref T value);
    public delegate void InAction<T>(in T value);

    //TODO : Хендлер также должен работать и с различными эффектами
    [RequireComponent(typeof(Health)), DisallowMultipleComponent]
    public class DamageHandler : MonoBehaviour
    {
        public event RefAction<Damage> OnDamageAdding;
        public event RefAction<Damage> OnPhysicDamageAdding;
        public event RefAction<Damage> OnMagicDamageAdding;

        //private UnitData _owner;

        public Health Health { get; private set; }

        public void Handler(Damage damageData)
        {
            OnDamageAdding?.Invoke(ref damageData);

            float resultDamage = PhysicalDamageHandler(ref damageData);
            resultDamage += MagicalDamageHandler(ref damageData);

            ResultDamageHandler(resultDamage);
        }

        private float PhysicalDamageHandler(ref Damage damageData)
        {
            OnPhysicDamageAdding?.Invoke(ref damageData);
            return damageData.PhysicalDamage;
        }

        private float MagicalDamageHandler(ref Damage damageData)
        {
            OnMagicDamageAdding?.Invoke(ref damageData);
            return damageData.MagicalDamage;
        }

        private void ResultDamageHandler(in float damage)
        {
            Health.ChangeCurrentHealth(-damage);
        }

        /*private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Ammo ammo))
            {
                if (ammo.Fraction != _owner.Fraction)
                {
                    OnAmmoEntering?.Invoke(in ammo);
                    Handler(ammo.Damage);
                    Destroy(ammo.gameObject);
                }
            }
        }*/

        private void Start()
        {
            Health = GetComponent<Health>();
        }
    }
}