using System;
using UnityEngine;

namespace CodeBase
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private Rigidbody[] _rigidbodies;

        public bool IsDied { get; private set; }
        public event Action<Enemy> Died;

        private void Start()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
            SetRigidbodiesKinematic(true);
        }

        private void SetRigidbodiesKinematic(bool isKinematic)
        {
            foreach (Rigidbody enemyRigidbody in _rigidbodies) 
                enemyRigidbody.isKinematic = isKinematic;
        }

        public void TakeDamage()
        {
            IsDied = true;
            ActivateRagdoll();
            Died?.Invoke(this);
        }

        private void ActivateRagdoll()
        {
            _animator.enabled = false;
            SetRigidbodiesKinematic(false);
        }
    }
}