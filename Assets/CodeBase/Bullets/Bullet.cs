using UnityEngine;

namespace CodeBase.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        private Vector3 _direction;

        public void Initialize(Vector3 direction)
        {
            _direction = direction;
            gameObject.SetActive(true);
        }

        private void FixedUpdate() => 
            _rigidbody.MovePosition(transform.position + _direction * _speed * Time.fixedDeltaTime);

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage();
            gameObject.SetActive(false);
        }
    }
}
