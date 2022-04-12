using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem.EffectSystem
{
    //TODO : ѕодумать насчет того, где и как хранить и получать _icon
    [CreateAssetMenu(fileName = "New GameEffectSO", menuName = "GameAbilitySystem/GameEffect")]
    public class GameEffectSO : ScriptableObject
    {
        [SerializeField]
        private Sprite _icon;
        [SerializeField]
        private List<ConstantSimpleEffectSO> _constEffects = new List<ConstantSimpleEffectSO>();
        [SerializeField]
        private List<PeriodicSimpleEffectSO> _periodicEffects = new List<PeriodicSimpleEffectSO>();

        public List<ISimpleEffect> CreateConstEffects()
        {
            List<ISimpleEffect> effects = new List<ISimpleEffect>();

            foreach(var k in _constEffects)
            {
                effects.Add(k.CreateEffect());
            }
            return effects;
        }

        public List<IPeriodicSimpleEffect> CreatePeriodicEffects()
        {
            List<IPeriodicSimpleEffect> effects = new List<IPeriodicSimpleEffect>();

            foreach (var k in _periodicEffects)
            {
                effects.Add(k.CreatePeriodicEffect());
            }
            return effects;
        }
    }
}