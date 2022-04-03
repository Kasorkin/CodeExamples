using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace BaseGameLogic
{
    public class HealthUI : MonoBehaviour
    {
        private Image _healthBar;
        private Health _health;

        private void Awake()
        {
            _healthBar = GetComponentInParent<Image>();
            _health = GetComponentInParent<Health>();
            _health.OnCurrentHealthChanged += HealthBarFiller;
        }

        private void HealthBarFiller(in float currentHealth)
        {
            //Debug.LogWarning(currentHealth + " " + gameObject.name);
            //_healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, currentHealth / _health.MaxHealth, 0.06f);
            float endValue = currentHealth / _health.MaxHealth;
            _healthBar.DOFillAmount(endValue, endValue * Consts.LerpSpeed);
            ColorChanger(endValue);
        }

        private void ColorChanger(in float endValue)
        {
            _healthBar.color = Color.Lerp(Color.red, Color.green, endValue);
        }

        private void OnDestroy()
        {
            _health.OnCurrentHealthChanged -= HealthBarFiller;
        }
    }
}