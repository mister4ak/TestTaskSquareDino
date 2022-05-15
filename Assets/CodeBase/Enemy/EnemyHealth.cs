using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public float Max { get; set; }
        public float Current { get; set; }
        public event Action HealthChanged;

        public void TakeDamage(float damage)
        {
            Current -= damage;
            HealthChanged?.Invoke();
        }
    }
}