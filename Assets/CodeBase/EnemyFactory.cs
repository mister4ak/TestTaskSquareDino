using CodeBase.Enemy;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase
{
    public class EnemyFactory
    {
        private readonly StaticDataService _staticData;

        public EnemyFactory(StaticDataService staticData)
        {
            _staticData = staticData;
        }
        
        public GameObject Create(EnemyType enemyType, Transform parent)
        {
            EnemyStaticData enemyData = _staticData.ForEnemy(enemyType);
            
            GameObject enemy = Object.Instantiate(enemyData.EnemyPrefab, parent.position, parent.rotation, parent);
            
            enemy.GetComponent<Enemy.Enemy>().Initialize();
            
            IHealth health = enemy.GetComponent<IHealth>();
            health.Current = enemyData.Hp;
            health.Max = enemyData.Hp;

            enemy.GetComponent<EnemyUI>().Initialize(health);
            
            return enemy;
        }
    }
}