using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Enemies;
using CodeBase.Infrastructure.Factories;
using CodeBase.Logic.Markers;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Location : MonoBehaviour
    {
        [SerializeField] private SpawnMarker[] _spawnMarkers;
        [SerializeField] private Transform _waypoint;
        private EnemyFactory _enemyFactory;
        private List<Enemy> _enemies;
        private int _enemiesCount;
        private int _diedEnemiesCounter;

        public Transform Waypoint => _waypoint;
        public bool IsLocationClear => _diedEnemiesCounter == _enemiesCount;
        
        public event Action LocationCleared;
        public event Action<Enemy> EnemyDied;

        public void InitializeEnemies(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
            _enemies = new List<Enemy>();

            foreach (SpawnMarker spawnMarker in _spawnMarkers)
            {
                GameObject enemy = _enemyFactory.Create(spawnMarker.enemyType, spawnMarker.transform);
                _enemies.Add(enemy.GetComponent<Enemy>());
            }
        }

        public void OnEnter()
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