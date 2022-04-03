using UnityEngine;

using BaseGameLogic;

namespace Hero
{
    public class HeroDestroy : IDestroyible
    {
        private readonly HeroRespawner _respawner;

        public HeroDestroy(HeroRespawner respawner)
        {
            _respawner = respawner;
        }

        void IDestroyible.Destroy(in Transform obj)
        {
            HeroDisable(obj);
            _respawner.StartRespawn(obj);
        }

        private void HeroDisable(in Transform obj)
        {
            obj.gameObject.SetActive(false);
        }
    }
}