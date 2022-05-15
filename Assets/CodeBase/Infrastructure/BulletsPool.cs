using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Players.Gun;

namespace CodeBase.Infrastructure
{
    public class BulletsPool
    {
        private readonly List<Bullet> _bullets = new List<Bullet>();
        private readonly AssetProvider _assetProvider;

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

        public Bullet GetBullet() => 
            _bullets.FirstOrDefault(bullet => !bullet.isActiveAndEnabled);
    }
}