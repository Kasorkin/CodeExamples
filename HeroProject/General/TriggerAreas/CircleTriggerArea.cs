using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic
{
    [DisallowMultipleComponent, RequireComponent(typeof(CircleCollider2D))]
    public sealed class CircleTriggerArea : TriggerArea
    {
        [SerializeField]
        private float _radius;

        private CircleCollider2D _collider;

        public override Vector2 RandomPoint()
        {
            Vector2 point;
            point.x = Random.Range(-_radius, _radius);
            point.y = Random.Range(-_radius, _radius);
            return (Vector2)transform.position + point;
        }

        public override void ChangeArea()
        {
            _collider.radius = _radius;
        }

        public override void ZoneEvent()
        {
            throw new System.NotImplementedException();
        }

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
        }
    }
}