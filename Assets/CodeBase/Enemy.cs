using System;
using UnityEngine;

namespace CodeBase
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private Rigidbody[] _ragdollRigidbodies;
        private Collider[] _ragdollColliders;

        public bool IsDied { get; private set; }
        public event Action Died;

        private void Start()
        {
            _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
            _ragdollColliders = GetComponentsInChildren<Collider>();
            
            SetRigidbodiesKinematic(true);
            SetCollidersState(true);
        }

        private void SetRigidbodiesKinematic(bool isKinematic)
        {
            foreach (Rigidbody ragdollRigidbody in _ragdollRigidbodies) 
                ragdollRigidbody.isKinematic = isKinematic;
        }
        
        private void SetCollidersState(bool state)
        {
            foreach (Collider ragdollCollider in _ragdollColliders) 
                ragdollCollider.enabled = state;
        }

        private void ActivateRagdoll()
        {
            _animator.enabled = false;
            
            SetRigidbodiesKinematic(false);
        }

        public void TakeDamage()
        {
            IsDied = true;
            ActivateRagdoll();
            Died?.Invoke();
        }
    }
}