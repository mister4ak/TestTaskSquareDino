using System;
using CodeBase.AssetManagment;
using CodeBase.Enemies;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Factories
{
    public class EnemyFactory
    {
        private readonly StaticDataService _staticData;
        private readonly AssetProvider _assetProvider;

        public EnemyFactory(StaticDataService staticData, AssetProvider assetProvider)
        {
            _staticData = staticData;
            _assetProvider = assetProvider;
        }
        
        public GameObject Create(EnemyType enemyType, Transform parent)
        {
            EnemyStaticData enemyData = _staticData.ForEnemy(enemyType);

            GameObject enemy = _assetProvider.Instantiate(GetEnemyPath(enemyType), parent.position, parent.rotation, parent);
            
            enemy.GetComponent<Enemy>().Initialize();
            
            IHealth health = enemy.GetComponent<IHealth>();
            health.Current = enemyData.Hp;
            health.Max = enemyData.Hp;

            enemy.GetComponent<EnemyUI>().Initialize(health);
            
            return enemy;
        }

        private string GetEnemyPath(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Base:
                    return AssetsAddress.BaseEnemyPath;
                case EnemyType.Strong:
                    return AssetsAddress.StrongEnemyPath;;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null);
            }
        }
    }
}