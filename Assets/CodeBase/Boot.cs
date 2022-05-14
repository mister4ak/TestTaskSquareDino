using UnityEngine;

namespace CodeBase
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private Player _player;
        [SerializeField] private Waypoint[] _waypoints;
        [SerializeField] private SceneLoader _sceneLoader;
        private int _currentWaypointIndex;

        private void Awake()
        {
            _player.SetPosition(_waypoints[_currentWaypointIndex].transform.position);
            _gameUI.GameStarted += StartGame;
            _player.WaypointReached += UpdateCurrentIndex;
        }

        private void StartGame()
        {
            foreach (var waypoint in _waypoints)
            {
                waypoint.LocationCleared += UpdateCurrentIndex;
                waypoint.Initialize();
            }

            UpdateCurrentIndex();
            
            _player.ActivateInput();
        }

        private void UpdateCurrentIndex()
        {
            if (_waypoints[_currentWaypointIndex].IsLocationCleared())
            {
                if (_currentWaypointIndex == _waypoints.Length - 1)
                {
                    _player.DisableInput();
                    _sceneLoader.RestartScene();
                    return;
                }

                _currentWaypointIndex++;
                MoveToNextWaypoint();
            }
        }

        private void MoveToNextWaypoint()
        {
            _player.SetNavMeshPosition(_waypoints[_currentWaypointIndex].transform.position);
        }
    }
}
