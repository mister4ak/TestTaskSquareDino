using CodeBase.AssetManagment;
using CodeBase.Factories;
using CodeBase.Locations;
using CodeBase.Players;
using CodeBase.Players.Bullets;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private Location[] _locations;
        [SerializeField] private SceneLoader _sceneLoader;
        
        private Game _game;
        private Player _player;
        private StaticDataService _staticData;
        private EnemyFactory _enemyFactory;
        private AssetProvider _assetProvider;
        private BulletsPool _bulletsPool;

        private void Awake()
        {
            _assetProvider = new AssetProvider();
            _bulletsPool = new BulletsPool(_assetProvider);
            CreateStaticData();
            CreateEnemies();
            InstantiatePlayer();
            InstantiatePlayerCamera();

            _game = new Game();
            Subscribe();
        }
        
        private void CreateStaticData()
        {
            _staticData = new StaticDataService();
            _staticData.Load();
        }

        private void CreateEnemies()
        {
            _enemyFactory = new EnemyFactory(_staticData, _assetProvider);
            
            foreach (var location in _locations) 
                location.InitializeEnemies(_enemyFactory);
        }

        private void InstantiatePlayer()
        {
            Vector3 startPosition = _locations[0].Waypoint.position;
            Quaternion rotation = _locations[0].Waypoint.rotation;
            //_player = Instantiate(_playerPrefab, startPosition, rotation);
            _player = _assetProvider
                .Instantiate(AssetsAddress.PlayerPath, startPosition, rotation)
                .GetComponent<Player>();
            
            _player.GetComponent<Weapon>().Construct(_bulletsPool);
        }

        private void InstantiatePlayerCamera()
        {
            //CameraFollow playerCamera = Instantiate(_playerCameraPrefab);
            //playerCamera.Follow(_player.transform);
            _assetProvider.Instantiate(AssetsAddress.PlayerCameraPath)
                    .GetComponent<CameraFollow>()
                    .Follow(_player.transform);
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
