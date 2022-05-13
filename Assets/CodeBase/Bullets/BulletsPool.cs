using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Bullets
{
    public class BulletsPool
    {
        private List<Bullet> _bullets = new List<Bullet>();
        
        public void Initialize(Bullet bulletPrefab, int poolSize)
        {
            for (int i = 0; i < poolSize; i++)
            {
                Bullet bullet = Object.Instantiate(bulletPrefab);
                bullet.gameObject.SetActive(false);
                _bullets.Add(bullet);
            }
        }

        public Bullet GetBullet()
        {
            return _bullets.FirstOrDefault(bullet1 => !bullet1.isActiveAndEnabled);
        }
    }
}