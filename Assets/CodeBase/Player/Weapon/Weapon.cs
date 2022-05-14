using CodeBase.Player.Weapon.Bullets;
using UnityEngine;

namespace CodeBase.Player.Weapon
{
    public class Weapon: MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _startPoint;
        
        private BulletsPool _bulletsPool;

        private void Start()
        {
            _bulletsPool = new BulletsPool();
            _bulletsPool.Initialize(_bulletPrefab, 20);
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