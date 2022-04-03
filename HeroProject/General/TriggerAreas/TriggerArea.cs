using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BaseGameLogic
{
    public abstract class TriggerArea : MonoBehaviour
    {
        //TODO понадобиться ли?
        //public UnityEvent OnEnterArea = new UnityEvent();

        protected readonly List<UnitData> _unitsInArea = new List<UnitData>();

        public abstract Vector2 RandomPoint();
        public abstract void ChangeArea();
        public abstract void ZoneEvent();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out UnitData unitData))
                _unitsInArea.Add(unitData);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out UnitData unitData))
                _unitsInArea.Remove(unitData);
        }
    }
}