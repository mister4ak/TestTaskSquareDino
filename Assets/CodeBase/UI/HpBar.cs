using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class HpBar: MonoBehaviour
    {
        [SerializeField] private Image _currentHp;

        public void SetValue(float current, float max)
        {
            _currentHp.fillAmount = current / max;
        }
    }
}