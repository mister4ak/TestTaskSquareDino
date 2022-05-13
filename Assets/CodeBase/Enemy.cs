using System;
using UnityEngine;

namespace CodeBase
{
    public class Enemy : MonoBehaviour
    {
        public event Action Died;
        public void TakeDamage()
        {
            gameObject.SetActive(false);
            Died?.Invoke();
        }
    }
}