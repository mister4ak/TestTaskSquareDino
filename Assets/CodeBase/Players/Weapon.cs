using CodeBase.Players.Bullets;
using UnityEngine;

namespace CodeBase.Players
{
    public class Weapon: MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private int _bulletsCount;
        
        private BulletsPool _bulletsPool;

        public void Construct(BulletsPool bulletsPool)
        {
            _bulletsPool = bulletsPool;
            _bulletsPool.Initialize(_bulletsCount);
        }

        public void Shoot(Vector3 endPosition)
        {
            Bullet bullet = _bulletsPool.GetBullet();
            Vector3 startPointPosition = _startPoint.position;
            Vector3 direction = (endPosition - startPointPosition).normalized;
            
            bullet.transform.position = startPointPosition;
            bullet.transform.rotation = Quaternion.LookRotation(direction);
            
            bullet.Initialize(direction);
        }
    }
}