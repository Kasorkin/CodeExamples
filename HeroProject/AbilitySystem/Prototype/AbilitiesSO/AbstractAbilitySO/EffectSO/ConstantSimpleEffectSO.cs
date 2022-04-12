using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem.EffectSystem
{
    public abstract class ConstantSimpleEffectSO : SimpleEffectSO
    {
        public abstract ISimpleEffect CreateEffect();
    }
}