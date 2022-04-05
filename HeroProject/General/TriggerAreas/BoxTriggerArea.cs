using UnityEngine;

namespace BaseGameLogic.TriggerAreas
{
    [DisallowMultipleComponent, RequireComponent(typeof(BoxCollider2D))]
    public sealed class BoxTriggerArea : TriggerArea
    {
        [SerializeField]
        private Vector2 _size;

        private BoxCollider2D _collider;

        public override Vector2 RandomPoint()
        {
            Vector2 point;
            point.x = Random.Range(-_size.x / 2, _size.x / 2);
            point.y = Random.Range(-_size.y / 2, _size.y / 2);
            return (Vector2)transform.position + point;
        }

        public override void ChangeArea()
        {
            _collider.size = _size;
        }

        public override void ZoneEvent()
        {
            throw new System.NotImplementedException();
        }

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }
    }
}