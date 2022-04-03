using UnityEngine;

using BaseGameLogic.Managers;

namespace BaseGameLogic
{
    //TODO : Можно подумать о вынесении здоровья в обычный класс
    [DisallowMultipleComponent]//, RequireComponent(typeof(Death))]
    public sealed class Health : MonoBehaviour
    {
        public event InAction<float> OnCurrentHealthChanged;
        public event InAction<float> OnMaxHealthChanged;

        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
        public float Regeneration { get; private set; } = 0f;

        public Death Death { get; private set; }

        public void SetMaxHealth(float maxHealth)
        {
            MaxHealth = maxHealth;
            ChangeCurrentHealth(maxHealth);
        }

        public void ChangeCurrentHealth(float value)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0f, MaxHealth);

            OnCurrentHealthChanged?.Invoke(CurrentHealth);

            if (CurrentHealth == 0f)
            {
                Debug.Log(gameObject.name + " имеет 0 хп");
                Death.Die();
            }
        }

        public void ChangeMaxHealth(float value)
        {
            MaxHealth = Mathf.Clamp(MaxHealth + value, Consts.MinValueOfMaxHealth, Mathf.Infinity);

            ChangeCurrentHealth(value);

            OnMaxHealthChanged?.Invoke(MaxHealth);
        }

        public void ChangeRegeneration(float value)
        {
            Regeneration = Mathf.Clamp(Regeneration + value, 0f, Mathf.Infinity);
        }

        private void FullRestoration()
        {
            Debug.LogWarning("Полное восстановленеи");
            ChangeCurrentHealth(MaxHealth);
        }

        private void Restoration()
        {
            if (Death.IsDead || MaxHealth == CurrentHealth || Regeneration == 0f)
                return;

            ChangeCurrentHealth(Regeneration * Time.fixedDeltaTime);
        }

        private void Awake()
        {
            //Death = GetComponent<Death>();

            Death = new Death(transform);
        }

        private void Start()
        {
            GameManager.Singleton.GlobalRegeneration += Restoration;
            Death.OnRessurected += FullRestoration;
        }

        private void OnDestroy()
        {
            GameManager.Singleton.GlobalRegeneration -= Restoration;
            Death.OnRessurected -= FullRestoration;
        }

        /*[SerializeField]
        private bool _isPidor = false;
        private void Update()
        {
            if(_isPidor && Input.GetKeyDown(KeyCode.Space))
            {
                Death.SetImmortality(8f);
            }

            if (_isPidor && Input.GetKeyDown(KeyCode.Tab))
            {
                Death.StopImmortality();
            }
        }*/
    }
}
