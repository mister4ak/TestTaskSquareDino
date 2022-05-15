using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Player.Weapon.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
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
                if (other.gameObject.layer.Equals(Constants.EnemyLayer))
                    other.gameObject.GetComponentInParent<IHealth>().TakeDamage(_damage);
            }
        }
    }
}