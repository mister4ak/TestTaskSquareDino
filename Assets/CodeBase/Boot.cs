using System.Collections;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private Player.Player _player;
        [SerializeField] private Waypoint[] _waypoints;
        [SerializeField] private SceneLoader _sceneLoader;
        private int _currentWaypointIndex;
        private bool _isPlayerRotated;
        private Quaternion _lookAt;
        private Waypoint _currentWaypoint;
        private IEnumerator _rotateCoroutine;
        
        private bool IsLocationClear => _waypoints[_currentWaypointIndex].IsLocationClear;

        private void Awake()
        {
            _player.SetPosition(_waypoints[_currentWaypointIndex].transform.position);
            Subscribe();
        }

        private void Subscribe()
        {
            _player.WaypointReached += CheckLocation;
            _player.RotateCompleted += () => _isPlayerRotated = false;
            _gameUI.GameStarted += StartGame;
        }

        private void StartGame()
        {
            _player.ActivateInput();
            CheckLocation();
        }

        private void CheckLocation()
        {
            InitializeCurrentWaypoint();

            if (IsLocationClear == false)
            {
                RotatePlayer(_currentWaypoint.TryGetLiveEnemy());
                _currentWaypoint.EnemyDied += RotatePlayer;
            }
        }

        private void InitializeCurrentWaypoint()
        {
            _currentWaypoint = _waypoints[_currentWaypointIndex];
            _currentWaypoint.LocationCleared += OnLocationCleared;
            _currentWaypoint.Initialize();
        }

        private void RotatePlayer(Enemy.Enemy aliveEnemy)
        {
            Vector3 direction = (aliveEnemy.transform.position - _player.transform.position).normalized;

            if (_isPlayerRotated) 
                StopCoroutine(_rotateCoroutine);

            _lookAt = Quaternion.LookRotation(direction, Vector3.up);
            _rotateCoroutine = _player.Rotate(_lookAt);
            
            StartCoroutine(_rotateCoroutine);
            _isPlayerRotated = true;
        }

        private void OnLocationCleared()
        {
            Unsubscribe();
            UpdateCurrentWaypointIndex();
            MoveToNextWaypoint();
        }

        private void Unsubscribe()
        {
            _currentWaypoint.LocationCleared -= OnLocationCleared;
            _currentWaypoint.EnemyDied -= RotatePlayer;
        }

        private void UpdateCurrentWaypointIndex()
        {
            if (_currentWaypointIndex == _waypoints.Length - 1)
            {
                EndLevel();
                return;
            }
            _currentWaypointIndex++;
        }

        private void MoveToNextWaypoint() => 
            _player.SetNavMeshPosition(_waypoints[_currentWaypointIndex].transform.position);

        private void EndLevel()
        {
            _player.DisableInput();
            _sceneLoader.RestartScene();
        }
    }
}
