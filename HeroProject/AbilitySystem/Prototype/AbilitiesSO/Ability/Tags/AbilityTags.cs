using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem
{
    [Serializable]
    public class AbilityTags
    {
        [SerializeField]
        private AbilityTagSO _ownTag;
        [SerializeField]
        private List<AbilityTagSO> _cancelAbilityWithTag = new List<AbilityTagSO>();
        [SerializeField]
        private List<AbilityTagSO> _blockAbilityWithTag = new List<AbilityTagSO>();
        [SerializeField]
        private List<AbilityTagSO> _refreshAbilityWithTag = new List<AbilityTagSO>();
    }
}