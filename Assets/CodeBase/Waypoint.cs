using System;
using System.Linq;
using UnityEngine;

namespace CodeBase
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private Enemy[] _enemies;
        private int _enemiesCount;
        private int _diedEnemiesCounter;

        public event Action LocationCleared;
        public event Action<Enemy> EnemyDied;
        
        public bool IsLocationClear => _diedEnemiesCounter == _enemiesCount;

        public void Initialize()
        {
            CountLiveEnemies();
            CheckLocationIsCleared();
        }

        private void CheckLocationIsCleared()
        {
            if (IsLocationClear)
                LocationCleared?.Invoke();
        }

        private void CountLiveEnemies()
        {
            foreach (Enemy enemy in _enemies)
                if (enemy.IsDied == false)
                {
                    enemy.Died += OnEnemyDied;
                    _enemiesCount++;
                }
        }

        private void OnEnemyDied(Enemy enemy)
        {
            enemy.Died -= OnEnemyDied;
            _diedEnemiesCounter++;

            Enemy aliveEnemy = TryGetLiveEnemy();
            if (aliveEnemy != null)
                EnemyDied?.Invoke(aliveEnemy);

            CheckLocationIsCleared();
        }

        public Enemy TryGetLiveEnemy() => 
            _enemies.FirstOrDefault(enemy => enemy.IsDied == false);
    }
}
