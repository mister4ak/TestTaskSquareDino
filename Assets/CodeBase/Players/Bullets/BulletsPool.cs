using System.Collections.Generic;
using System.Linq;
using CodeBase.AssetManagment;

namespace CodeBase.Players.Bullets
{
    public class BulletsPool
    {
        private readonly List<Bullet> _bullets = new List<Bullet>();
        private AssetProvider _assetProvider;

        public BulletsPool(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public void Initialize(int poolSize)
        {
            for (int i = 0; i < poolSize; i++)
            {
                Bullet bullet = _assetProvider.Instantiate(AssetsAddress.BulletPath).GetComponent<Bullet>();
                bullet.gameObject.SetActive(false);
                _bullets.Add(bullet);
            }
        }
        // public void Initialize(Bullet bulletPrefab, int poolSize)
        // {
        //     for (int i = 0; i < poolSize; i++)
        //     {
        //         Bullet bullet = Object.Instantiate(bulletPrefab);
        //         bullet.gameObject.SetActive(false);
        //         _bullets.Add(bullet);
        //     }
        // }

        public Bullet GetBullet() => 
            _bullets.FirstOrDefault(bullet => !bullet.isActiveAndEnabled);
    }
}