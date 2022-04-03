using UnityEngine;

using BaseGameLogic.Player;

namespace BaseGameLogic
{
    //TODO : Убрать отсюда старт. фракцию?
    [DisallowMultipleComponent, RequireComponent(typeof(Health), typeof(DamageHandler), typeof(CircleCollider2D))]
    public class UnitData : MonoBehaviour
    {
        [SerializeField]
        private Fraction _startFraction;
        [SerializeField]
        private WeaponStats _weaponStats;
        [SerializeField]
        private Armor _armor;
        [SerializeField, Min(1)]
        private float _maxHealth;
        [SerializeField, Min(0)]
        private float _regeneration;
        [SerializeField, Min(0)]
        private float _maxMana;
        [SerializeField, Min(0)]
        private float _manaRegeneration;
        [SerializeField, Min(0)]
        private float _speed;
        [SerializeField, Min(2)]
        private float _visionRange;

        public float Speed { get => _speed; set => _speed = value; }
        public WeaponStats WeaponStats { get => _weaponStats; set => _weaponStats = value; }
        public Armor Armor { get => _armor; set => _armor = value; }
        public float VisionRange { get => _visionRange; set => _visionRange = value; }

        public Weapon Weapon { get; private set; }
        public Health Health { get; private set; }
        public Mana Mana { get; private set; }
        public DamageHandler DamageHandler { get; private set; }

        //public UnitController UnitController { get; private set; }
        public Fraction StartFraction => _startFraction;
        public PlayerData Owner { get; set; }
        public CircleCollider2D CircleCollider { get; private set; }

        private void Awake()
        {
            //UnitController = GetComponent<UnitController>();
            Health = GetComponent<Health>();
            Mana = GetComponent<Mana>();
            DamageHandler = GetComponent<DamageHandler>();
            Weapon = GetComponent<Weapon>();
            CircleCollider = GetComponent<CircleCollider2D>();
        }

        private void Start()
        {
            Health.SetMaxHealth(_maxHealth);
            Health.ChangeRegeneration(_regeneration);
            if (Mana)
            {
                Mana.SetMaxMana(_maxMana);
                Mana.ChangeRegeneration(_manaRegeneration);
            }
        }
    }
}