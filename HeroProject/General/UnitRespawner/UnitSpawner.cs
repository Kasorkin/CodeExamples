using System.Collections;
using UnityEngine;

namespace BaseGameLogic
{
    /*public class UnitSpawner : MonoBehaviour
    {
        [SerializeField]
        private UnitData _unitRespawn;

        [SerializeField]
        private bool _isLocalTimeToRespawn;
        [SerializeField]
        private float _time;

        private void StartSpawn()
        {
            StartCoroutine(Spawning());
        }

        private IEnumerator Spawning()
        {
            if (_isLocalTimeToRespawn)
                yield return new WaitForSecondsRealtime(_time);
            else
                yield return new WaitForSecondsRealtime(Consts.TimeToCreepRespawn);

            Spawn();
        }

        private void Spawn()
        {
            UnitData unit = Instantiate(_unitRespawn, transform);
            unit.Health.Death.OnDied += StartSpawn;
        }

        private void Start()
        {
            Spawn();
        }
    }*/
}