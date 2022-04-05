using UnityEngine;

namespace GameAbilitySystem
{
    public interface IAbility
    {
        public void Init(Transform owner);

        public void Upgrade();
    }
}