using UnityEngine;

namespace GameAbilitySystem.CostSystem
{
    public abstract class CostSO : ScriptableObject
    {
        public abstract Cost CreateCost(Transform owner);
    }

    public enum AttributeCostType
    {
        Mana,
        Health
    }
}