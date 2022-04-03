using UnityEngine;
using System;

using BaseGameLogic;

namespace Hero
{
    //TODO : ѕосле проверок скрыть доп. характеристики из инспектора
    [DisallowMultipleComponent, RequireComponent(typeof(UnitData))]
    public class HeroStats : MonoBehaviour
    {
        public event Action<HeroStats> StrengthChanged;
        public event Action<HeroStats> AgilityChanged;
        public event Action<HeroStats> IntelligenceChanged;

        [Header("ќсновные характеристики")]
        [SerializeField]
        private MainAttribute _mainAttribute;
        [SerializeField, Min(0)]
        private float _defaultStrength;
        [SerializeField, Min(0)]
        private float _defaultAgility;
        [SerializeField, Min(0)]
        private float _defaultIntelligence;

        [Header("–ост характеристик")]
        [SerializeField, Min(0)]
        private float _growthStrength;
        [SerializeField, Min(0)]
        private float _growthAgility;
        [SerializeField, Min(0)]
        private float _growthIntelligence;

        [Header("ƒополнительные характеристики")]
        [SerializeField, Min(0)]
        private float _bonusStrength;
        [SerializeField, Min(0)]
        private float _bonusAgility;
        [SerializeField, Min(0)]
        private float _bonusIntelligence;

        public HeroLevel HeroLevel { get; private set; } = new HeroLevel();

        public UnitData UnitData { get; private set; }

        public MainAttribute MainAttribute => _mainAttribute;

        public float Strength { get; private set; }
        public float Agility { get; private set; }
        public float Intelligence { get; private set; }

        public float GetBonusStrength => _bonusStrength;
        public float GetBonusAgility => _bonusAgility;
        public float GetBonusIntelligence => _bonusIntelligence;

        public float GetSummaryStrength => Strength + _bonusStrength;
        public float GetSummaryAgility => Agility + _bonusAgility;
        public float GetSummaryIntelligence => Intelligence + _bonusIntelligence;

        public void ChangeStrength(in float value)
        {
            Strength = Mathf.Clamp(Strength + value, 0, Mathf.Infinity);
                
            UnitData.Health.ChangeMaxHealth(value * HeroConsts.StrengthMaxHealthValue);
            UnitData.Health.ChangeRegeneration(value * HeroConsts.StrengthRegenerationValue);

            if (_mainAttribute == MainAttribute.Strength)
                ChangeMainAttribute(value);

            StrengthChanged?.Invoke(this);
        }

        public void ChangeAgility(in float value)
        {
            Agility = Mathf.Clamp(Agility + value, 0, Mathf.Infinity);

            UnitData.Armor.ChangePhysicalArmor(value * HeroConsts.AgilityArmorValue);
            UnitData.WeaponStats.ChangeCooldown(-value * HeroConsts.AgilityAttackSpeedValue);

            if (_mainAttribute == MainAttribute.Agility)
                ChangeMainAttribute(value);

            AgilityChanged?.Invoke(this);
        }

        public void ChangeIntelligence(in float value)
        {
            Intelligence = Mathf.Clamp(Intelligence + value, 0, Mathf.Infinity);

            if (UnitData.Mana)
            {
                UnitData.Mana.ChangeMaxMana(value * HeroConsts.IntelligenceMaxManaValue);
                UnitData.Mana.ChangeRegeneration(value * HeroConsts.IntelligenceManaRegenerationValue);
            }

            if (_mainAttribute == MainAttribute.Intelligence)
                ChangeMainAttribute(value);

            IntelligenceChanged?.Invoke(this);
        }

        public void ChangeBonusStrength(in float value)
        {
            _bonusStrength += value;

            if (_mainAttribute == MainAttribute.Strength)
                ChangeMainAttribute(value);

            StrengthChanged?.Invoke(this);
        }

        public void ChangeBonusAgility(float value)
        {
            _bonusAgility += value;

            if (_mainAttribute == MainAttribute.Agility)
                ChangeMainAttribute(value);

            AgilityChanged?.Invoke(this);
        }

        public void ChangeBonusIntelligence(float value)
        {
            _bonusIntelligence += value;

            if (_mainAttribute == MainAttribute.Intelligence)
                ChangeMainAttribute(value);

            IntelligenceChanged?.Invoke(this);
        }

        private void ChangeMainAttribute(in float value)
        {
            UnitData.WeaponStats.ChangeDamage(value * HeroConsts.MainAttributeDamageFactor);
        }

        private void GrowthStats()
        {
            ChangeStrength(_growthStrength);
            ChangeIntelligence(_growthIntelligence);
            ChangeAgility(_growthAgility);
        }

        private void Awake()
        {
            UnitData = GetComponent<UnitData>();
        }

        private void Start()
        {
            Initialization();
            HeroLevel.LevelChanged += GrowthStats;

            //TODO : «аглушка? потом это должно быть делегировано пику геро€
            FindObjectOfType<HeroManager>().AddHero(this);
        }

        private void Initialization()
        {
            ChangeStrength(_defaultStrength);
            ChangeIntelligence(_defaultIntelligence);
            ChangeAgility(_defaultAgility);
        }

        private void OnDestroy()
        {
            HeroLevel.LevelChanged -= GrowthStats;
        }

        /*private void Update()
       {
          if(Input.GetKeyDown(KeyCode.Space))
           {
               //HeroLevel.AddExperience(200);
               UnitData.Health.ChangeCurrentHealth(-20);
           }

           if(Input.GetKeyDown(KeyCode.Tab))
           {
               //HeroLevel.AddExperience(600);
               UnitData.Health.ChangeCurrentHealth(500);
           }
    }*/
    }
}