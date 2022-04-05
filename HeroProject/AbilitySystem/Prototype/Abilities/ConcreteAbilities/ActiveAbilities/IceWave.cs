using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem
{
    public class IceWave : Ability, IActiveAbility
    {
        private readonly List<IceWaveData> _iceWaveLevelsData;

        public IceWave(Sprite icon, List<IceWaveData> iceWaveLevelsData) : base(icon, iceWaveLevelsData.Count)
        {
            _iceWaveLevelsData = iceWaveLevelsData;
        }

        public void Init(Transform owner)
        {

        }

        public void Upgrade()
        {
            throw new System.NotImplementedException();
        }

        public void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}