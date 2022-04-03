using System.Collections.Generic;
using UnityEngine;

namespace Hero
{
    [DisallowMultipleComponent]
    public sealed class HeroManager : MonoBehaviour
    {
        private readonly List<HeroStats> _heroes = new List<HeroStats>();
        private HeroRespawner _heroSpawner;
        private HeroDestroy _heroDestroy;

        public void AddHero(in HeroStats hero)
        {
            //hero.GetComponent<Death>().SetDestroyible(_heroDestroy);
            hero.UnitData.Health.Death.SetDestroyible(_heroDestroy);
            _heroes.Add(hero);
        }

        private void Awake()
        {
            _heroSpawner = FindObjectOfType<HeroRespawner>();
            _heroDestroy = new HeroDestroy(_heroSpawner);
        }
    }
}