using CodeBase.Enemies;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Static Data/Enemy")]
    public class EnemyStaticData: ScriptableObject
    {
        public EnemyType EnemyType;
    
        [Range(1,10)]
        public int Hp = 1;

        public GameObject EnemyPrefab;
    }
}