﻿using System;
using System.Collections;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private const float SecondsBeforeDestroy = 3f;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private EnemyUI _enemyUI;
        private Rigidbody[] _rigidbodies;

        public bool IsDied { get; private set; }
        public event Action<Enemy> Died;

        public void Initialize()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
            SetRigidbodiesKinematic(true);
            
            InitializeHealth();
        }

        private void SetRigidbodiesKinematic(bool isKinematic)
        {
            foreach (Rigidbody enemyRigidbody in _rigidbodies) 
                enemyRigidbody.isKinematic = isKinematic;
        }

        private void InitializeHealth()
        {
            _enemyUI.Initialize(_enemyHealth);
            _enemyHealth.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (_enemyHealth.Current == 0)
                Die();
        }

        private void Die()
        {
            _enemyHealth.HealthChanged -= OnHealthChanged;
            IsDied = true;
            Died?.Invoke(this);
            
            ActivateRagdoll();
            StartCoroutine(DestroyTimer());
        }

        private void ActivateRagdoll()
        {
            _animator.enabled = false;
            SetRigidbodiesKinematic(false);
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(SecondsBeforeDestroy);
            Destroy(gameObject);
        }
    }
}