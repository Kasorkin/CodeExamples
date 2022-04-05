using System;
using UnityEngine;

namespace BaseGameLogic.DamageSystem
{
    [Serializable]
    public class DamageData
    {
        [SerializeField]
        private float _physicalDamage;
        [SerializeField]
        private float _magicalDamage;
        [SerializeField]
        private float _pureDamage;

        public float PhysicalDamage => _physicalDamage;
        public float MagicalDamage => _magicalDamage;
        public float PureDamage => _pureDamage;
    }
}