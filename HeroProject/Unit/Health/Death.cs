using System;
using System.Collections;
using UnityEngine;

using BaseGameLogic.Managers;

namespace BaseGameLogic
{
    //[DisallowMultipleComponent]
    public class Death
    {
        public event Action OnDied;
        public event Action OnRessurected;

        private IDestroyible _destroyible = new ClassicDestroy();
        private float _time;

        private Transform _transform;

        public Death(Transform transform)
        {
            _transform = transform;
        }

        public bool IsMortal { get; private set; } = true;
        public bool IsDead { get; private set; } = false;

        public void SetDestroyible(IDestroyible destroyible)
        {
            _destroyible = destroyible;
        }

        public void Ressurection()
        {
            IsDead = false;

            OnRessurected?.Invoke();
        }

        public void Die()
        {
            if (IsMortal == false)
                return;

            IsDead = true;
            OnDied?.Invoke();
            _destroyible.Destroy(_transform);
        }

        
        public void SetImmortality(in float time)
        {
            if(_time != 0)
                StopImmortality();

            _time = time;
            GameManager.Singleton.CoroutineManager.StartCoroutine(Immortality(time));
        }

        public void StopImmortality()
        {
            GameManager.Singleton.CoroutineManager.StopCoroutine(Immortality(_time));
            IsMortal = true;
            _time = 0;
        }

        private IEnumerator Immortality(float time)
        {
            IsMortal = false;
            yield return new WaitForSecondsRealtime(time);
            IsMortal = true;
        }
    }
}