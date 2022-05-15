using System.Collections.Generic;
using System.Linq;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService
    {
        private const string EnemiesDataPath = "StaticData/Enemies";

        private Dictionary<EnemyType, EnemyStaticData> _enemies;

        public void Load()
        {
            _enemies = Resources
                .LoadAll<EnemyStaticData>(EnemiesDataPath)
                .ToDictionary(x => x.EnemyType, x => x);
        }

        public EnemyStaticData ForEnemy(EnemyType enemyType) => 
            _enemies.TryGetValue(enemyType, out EnemyStaticData enemyData) 
                ? enemyData 
                : null;
    }
}