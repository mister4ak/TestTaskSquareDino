using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private Player.Player _player;
        [SerializeField] private Location[] _locations;
        [SerializeField] private SceneLoader _sceneLoader;
        
        private Game _game;

        private void Awake()
        {
            _player.SetPosition(_locations[0].Waypoint.position);

            StaticDataService staticData = new StaticDataService();
            staticData.Load();
            EnemyFactory enemyFactory = new EnemyFactory(staticData);

            foreach (var location in _locations)
            {
                location.InitializeEnemies(enemyFactory);
            }

            _game = new Game();
            Subscribe();
        }

        private void Subscribe()
        {
            _gameUI.GameStarted += StartGame;
            _game.LevelEnded += RestartLevel;
        }

        private void StartGame() => 
            _game.StartLevel(_player, _locations);

        private void RestartLevel() => 
            _sceneLoader.RestartScene();
    }
}
