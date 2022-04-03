using UnityEngine;

using BaseGameLogic.Managers;

namespace BaseGameLogic
{
    //TODO : ћожно подумать о вынесении маны в обычный класс
    public sealed class Mana : MonoBehaviour
    {
        public event InAction<float> OnCurrentManaChanged;
        public event InAction<float> OnMaxManaChanged;

        public float MaxMana { get; private set; }
        public float CurrentMana { get; private set; }
        public float Regeneration { get; private set; } = 0f;

        public void SetMaxMana(float maxMana)
        {
            MaxMana = maxMana;
            ChangeCurrentMana(maxMana);
        }

        public void ChangeCurrentMana(float value)
        {
            CurrentMana = Mathf.Clamp(CurrentMana + value, 0f, MaxMana);

            OnCurrentManaChanged?.Invoke(CurrentMana);
        }

        public void ChangeMaxMana(float value)
        {
            MaxMana = Mathf.Clamp(MaxMana + value, 0f, Mathf.Infinity);

            ChangeCurrentMana(value);

            OnMaxManaChanged?.Invoke(MaxMana);
        }

        public void ChangeRegeneration(float value)
        {
            Regeneration = Mathf.Clamp(Regeneration + value, 0f, Mathf.Infinity);
        }

        private void Restoration()
        {
            if (MaxMana <= CurrentMana || Regeneration == 0f)
                return;

            ChangeCurrentMana(Regeneration * Time.fixedDeltaTime);
        }

        private void Start()
        {
            GameManager.Singleton.GlobalRegeneration += Restoration;
        }

        private void OnDestroy()
        {
            GameManager.Singleton.GlobalRegeneration -= Restoration;
        }
    }
}