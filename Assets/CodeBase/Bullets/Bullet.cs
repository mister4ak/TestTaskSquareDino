using System;
using UnityEngine;

namespace CodeBase.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
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
                gameObject.SetActive(false);
                _isCollided = true;
                if (other.gameObject.layer.Equals(Constants.EnemyLayer))
                    other.gameObject.GetComponentInParent<Enemy>().TakeDamage();
            }
        }
    }
}
