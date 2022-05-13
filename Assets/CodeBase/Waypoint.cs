using System;
using UnityEngine;

namespace CodeBase
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private Enemy[] _enemies;
        private int _enemiesCount;
        private int _diedEnemiesCounter;

        public event Action LocationCleared;
        
        public void Initialize()
        {
            _enemiesCount = _enemies.Length;
            _diedEnemiesCounter = 0;

            foreach (var enemy in _enemies) 
                enemy.Died += OnEnemyDied;
        }

        private void OnEnemyDied()
        {
            _diedEnemiesCounter++;
            if (IsLocationCleared())
                LocationCleared?.Invoke();
        }

        public bool IsLocationCleared() => 
            _diedEnemiesCounter == _enemiesCount;
    }
}
