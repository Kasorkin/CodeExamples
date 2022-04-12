using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem.EffectSystem
{
    public class GameEffect
    {
        private List<ISimpleEffect> _simpleEffects;
        private List<IPeriodicSimpleEffect> _periodicEffects;

        public IEnumerable<ISimpleEffect> SimpleEffects => _simpleEffects;
        public IEnumerable<IPeriodicSimpleEffect> PeriodicEffects => _periodicEffects;

        public GameEffect(GameEffectSO data)
        {
            _simpleEffects = data.CreateConstEffects();
            _periodicEffects = data.CreatePeriodicEffects();
        }

        public void Upgrade()
        {
            foreach (var k in _simpleEffects)
            {
                k.Upgrade();
            }

            foreach (var k in _periodicEffects)
            {
                k.Upgrade();
            }
        }
    }
}