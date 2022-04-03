using System.Collections;
using UnityEngine;

using BaseGameLogic.Managers;

namespace BaseGameLogic
{
    public class RespawnDestoy : IDestroyible
    {
        private readonly float _timeToRespawn;

        public RespawnDestoy(float timeToRespawn = Consts.TimeToCreepRespawn)
        {
            _timeToRespawn = timeToRespawn;
        }

        void IDestroyible.Destroy(in Transform obj)
        {
            obj.gameObject.SetActive(false);
            GameManager.Singleton.CoroutineManager.StartCoroutine(WaitingToRespawn(obj));
        }

        private IEnumerator WaitingToRespawn(Transform obj)
        {
            yield return new WaitForSecondsRealtime(_timeToRespawn);
            Respawn(obj);
        }

        private void Respawn(in Transform obj)
        {
            FullRestoration(obj);
            obj.gameObject.SetActive(true);
        }

        private void FullRestoration(in Transform obj)
        {
            obj.GetComponent<UnitData>().Health.Death.Ressurection();
        }
    }
}