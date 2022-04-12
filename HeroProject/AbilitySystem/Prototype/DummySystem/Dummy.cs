using System.Collections;
using UnityEngine;

using BaseGameLogic.DamageSystem;

namespace GameAbilitySystem.DummySystem
{
    public class Dummy : MonoBehaviour
    {
        private Collider2D _collider;
        private Rigidbody2D _rigidbody;

        private Vector2 _startPos;
        private float _distance;
        private bool _destroyWhenMovingEnd;

        public Damage Damage { get; set; }

        /*private void Start()
        {
            Vector3 target = new Vector3(0, -1, 0);
            Move(target, 3f, 5f, false);
        }*/

        public void CreateCircleCollider(float radius)
        {
            CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
            collider.isTrigger = true;
            collider.radius = radius;

            _collider = collider;
        }

        public void CreateBoxCollider(float widgth, float lenght)
        {
            BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
            collider.size = new Vector2(widgth, lenght);

            _collider = collider;
        }

        public void Move(Vector3 posDirect, float speed, float distance, bool destroy = false)
        {
            CreateRigidbody();

            Vector2 direct = (posDirect - transform.position) / (posDirect - transform.position).magnitude;

            StartCoroutine(Moving(direct, speed));
            _distance = distance;
            _destroyWhenMovingEnd = destroy;
        }

        private IEnumerator Moving(Vector2 direct, float speed)
        {
            while (CanMoving())
            {
                yield return new WaitForFixedUpdate();
                transform.Translate(direct * speed * Time.fixedDeltaTime);
            }

            if (_destroyWhenMovingEnd)
                Destroy(gameObject);
        }

        private bool CanMoving()
        {
            return Vector2.Distance(_startPos, transform.position) <= _distance; 
        }

        private void CreateRigidbody()
        {
            _rigidbody = gameObject.AddComponent<Rigidbody2D>();
            _rigidbody.simulated = false;
            _rigidbody.isKinematic = true;
            _startPos = transform.position;
        }
    }
}