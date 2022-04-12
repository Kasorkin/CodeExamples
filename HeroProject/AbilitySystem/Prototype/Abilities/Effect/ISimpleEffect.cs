using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem.EffectSystem
{
    public interface ISimpleEffect
    {
        public void Init(Transform owner);

        public void Start(Transform target);

        public void Stop();

        public void Upgrade();
    }
}