using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem.EffectSystem
{
    public interface IPeriodicSimpleEffect : ISimpleEffect
    {
        public void Step();
    }
}