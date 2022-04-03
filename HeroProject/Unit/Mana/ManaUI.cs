using UnityEngine;
using UnityEngine.UI;

namespace BaseGameLogic
{
    public class ManaUI : MonoBehaviour
    {
        private Image _healthBar;
        private Mana _mana;

        private void Awake()
        {
            _healthBar = GetComponentInParent<Image>();
            _mana = GetComponentInParent<Mana>();

            if (_mana == null)
                Destroy(gameObject);

            _mana.OnCurrentManaChanged += HealthBarFiller;
        }

        private void HealthBarFiller(in float currentHealth)
        {
            _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, currentHealth / _mana.MaxMana, Consts.LerpSpeed);
            ColorChanger(currentHealth);
        }

        private void ColorChanger(in float currentHealth)
        {
            _healthBar.color = Color.Lerp(Color.red, Color.green, currentHealth / _mana.MaxMana);
        }

        private void OnDestroy()
        {
            _mana.OnCurrentManaChanged -= HealthBarFiller;
        }
    }
}