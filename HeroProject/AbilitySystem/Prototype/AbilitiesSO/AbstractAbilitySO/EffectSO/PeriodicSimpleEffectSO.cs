using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem.EffectSystem
{
    public abstract class PeriodicSimpleEffectSO : SimpleEffectSO
    {
        public abstract IPeriodicSimpleEffect CreatePeriodicEffect();
    }
}