using UnityEngine;

namespace GameAbilitySystem
{
    public interface IAbility
    {
        public Sprite Icon { get; }

        public void Init(Transform owner);

        public void Upgrade();
    }
}