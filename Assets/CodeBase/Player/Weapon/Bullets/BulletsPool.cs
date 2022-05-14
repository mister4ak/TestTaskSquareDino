using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Player.Weapon.Bullets
{
    public class BulletsPool
    {
        private readonly List<Bullet> _bullets = new List<Bullet>();
        
        public void Initialize(Bullet bulletPrefab, int poolSize)
        {
            for (int i = 0; i < poolSize; i++)
            {
                Bullet bullet = Object.Instantiate(bulletPrefab);
                bullet.gameObject.SetActive(false);
                _bullets.Add(bullet);
            }
        }

        public Bullet GetBullet() => 
            _bullets.FirstOrDefault(bullet => !bullet.isActiveAndEnabled);
    }
}