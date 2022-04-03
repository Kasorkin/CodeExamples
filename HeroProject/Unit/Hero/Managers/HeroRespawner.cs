using System.Collections;
using UnityEngine;
using System;

using BaseGameLogic;

namespace Hero
{
    [DisallowMultipleComponent]
    public sealed class HeroRespawner : MonoBehaviour
    {
        public event Action OnStartedRespawn;
        public event Action OnRespawned;

        [SerializeField]
        private TriggerArea _respawnArea;

        public void StartRespawn(in Transform hero)
        {
            Debug.Log("Начат процесс воскрешения");
            StartCoroutine(Respawning(hero.GetComponent<HeroStats>()));
        }

        private IEnumerator Respawning(HeroStats hero)
        {
            OnStartedRespawn?.Invoke();

            yield return new WaitForSecondsRealtime(TimeCalculate(hero));
            Respawn(hero);
        }

        private void Respawn(in HeroStats hero)
        {
            TeleportToRespawnPos(hero);
            hero.gameObject.SetActive(true);
            FullRestoration(hero);
            OnRespawned?.Invoke();
        }

        private void FullRestoration(in HeroStats hero)
        {
            hero.UnitData.Health.Death.Ressurection();
        }

        private void TeleportToRespawnPos(in HeroStats hero)
        {
            Vector2 spawnPoint = _respawnArea.RandomPoint();
            hero.transform.position = spawnPoint;
        }

        private float TimeCalculate(in HeroStats hero)
        {
            return HeroConsts.BaseTimeToRespawn + HeroConsts.GrowthTimeToRespawnPerLevel * hero.HeroLevel.CurrentLevel;
        }
    }
}