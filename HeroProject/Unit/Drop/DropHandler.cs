using System.Collections.Generic;
using UnityEngine;

using Hero;
using BaseGameLogic.Drop;

namespace BaseGameLogic
{
    [DisallowMultipleComponent, RequireComponent(typeof(Health), typeof(DamageHandler))]
    public class DropHandler : MonoBehaviour
    {
        [Header("Награда золота за убийство")]
        [SerializeField, Min(0)]
        private int _minValue;
        [SerializeField, Min(0)]
        private int _maxValue;

        [Header("Награда опыта")]
        [SerializeField, Min(0)]
        private int _experience;

        [Header("Конкретные предметы")]
        [SerializeField]
        private List<ClassicItemDrop> _items = new List<ClassicItemDrop>();
        [Header("Сеты предметов")]
        [SerializeField]
        private List<SetItemDrop> _itemSets = new List<SetItemDrop>();

        private Health _health;
        private DamageHandler _damageHandler;

        private UnitData _attaker;

        private void Handler()
        {
            GiveGoldRandom();
            GiveExperience();
            DropItems();
        }

        private void LastAttacking(ref Damage damageData)
        {
            _attaker = damageData.OwnerOfDamage;
        }

        private void GiveGoldRandom()
        {
            int gold = Random.Range(_minValue, _maxValue);
            _attaker.Owner.ChangeMoney(gold);
        }

        private void GiveExperience()
        {
            RaycastHit2D[] raycasts = Physics2D.CircleCastAll(transform.position, Consts.RadiusOfDropExperience, Vector2.zero);

            List<HeroStats> foundHeroes = new List<HeroStats>();
            foreach (var k in raycasts)
            {
                if(k.transform.TryGetComponent(out HeroStats hero))
                    foundHeroes.Add(hero);
            }

            int experience = Mathf.CeilToInt(_experience / foundHeroes.Count);
            foreach(var k in foundHeroes)
            {
                k.HeroLevel.AddExperience(experience);
            }
        }

        private void DropItems()
        {
            foreach(var k in _items)
            {
                k.Drop(transform.position);
            }

            foreach(var k in _itemSets)
            {
                k.Drop(transform.position);
            }
        }

        private void Awake()
        {
            _health = GetComponent<Health>();
            _damageHandler = GetComponent<DamageHandler>();
        }

        private void Start()
        {
            _health.Death.OnDied += Handler;
            _damageHandler.OnDamageAdding += LastAttacking;
        }

        private void OnDestroy()
        {
            _health.Death.OnDied -= Handler;
            _damageHandler.OnDamageAdding -= LastAttacking;
        }
    }
}