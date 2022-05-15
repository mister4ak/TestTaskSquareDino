using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Players.Gun
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private LayerMask _enemyLayer;
        private Vector3 _direction;
        private bool _isCollided;
        
        public void Initialize(Vector3 direction)
        {
            _direction = direction;
            _isCollided = false;
            gameObject.SetActive(true);
            
        }

        private void FixedUpdate() => 
            _rigidbody.MovePosition(transform.position + _direction * _speed * Time.fixedDeltaTime);

        private void OnCollisionEnter(Collision other)
        {
            if (_isCollided == false)
            {
                _isCollided = true;
                gameObject.SetActive(false);
                if (1 << other.gameObject.layer == _enemyLayer.value)
                    other.gameObject.GetComponentInParent<IHealth>().TakeDamage(_damage);
            }
        }
    }
}
