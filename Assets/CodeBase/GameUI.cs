using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        
        public event Action GameStarted;
        
        private void Awake()
        {
            _startGameButton.gameObject.SetActive(true);
            _startGameButton.onClick.AddListener(OnStartGameClicked);
        }

        private void OnStartGameClicked()
        {
            _startGameButton.gameObject.SetActive(false);
            _startGameButton.onClick.RemoveListener(OnStartGameClicked);
            GameStarted?.Invoke();
        }
    }
}