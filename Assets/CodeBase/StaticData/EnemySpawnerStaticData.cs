using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class EnemySpawnerStaticData
    {
        public EnemyType EnemyType;
        public Vector3 Position;
        public Quaternion Rotation;

        public EnemySpawnerStaticData(EnemyType enemyType, Vector3 position, Quaternion rotation)
        {
            EnemyType = enemyType;
            Position = position;
            Rotation = rotation;
        }
    }
}