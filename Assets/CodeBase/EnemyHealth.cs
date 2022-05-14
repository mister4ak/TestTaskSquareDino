using System;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float _current;

        [SerializeField] private float _max;

        public event Action HealthChanged;

        public float Max
        {
            get => _max;
            set => _max = value;
        }
        public float Current
        {
            get => _current;
            set => _current = value;
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            HealthChanged?.Invoke();
        }
    }
}