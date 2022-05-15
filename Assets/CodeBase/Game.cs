﻿using System;
using UnityEngine;

namespace CodeBase
{
    public class Game
    {
        private Player.Player _player;
        private Location[] _locations;
        private Location _currentLocation;
        private bool _isPlayerRotated;
        private int _currentLocationIndex;

        private bool IsLocationClear => _locations[_currentLocationIndex].IsLocationClear;

        public event Action LevelEnded;

        public void StartLevel(Player.Player player, Location[] locations)
        {
            _player = player;
            _locations = locations;
            
            _player.ActivateInput();
            Subscribe();
            CheckLocation();
        }
        
        private void Subscribe()
        {
            _player.WaypointReached += CheckLocation;
            _player.RotateCompleted += () => _isPlayerRotated = false;
        }
        
        private void CheckLocation()
        {
            InitializeCurrentLocation();

            if (IsLocationClear == false)
            {
                RotatePlayer(_currentLocation.TryGetLiveEnemy());
                _currentLocation.EnemyDied += RotatePlayer;
            }
        }

        private void InitializeCurrentLocation()
        {
            _currentLocation = _locations[_currentLocationIndex];
            _currentLocation.LocationCleared += OnLocationCleared;
            _currentLocation.OnEnter();
        }
        
        private void RotatePlayer(Enemy.Enemy aliveEnemy)
        {
            Vector3 direction = (aliveEnemy.transform.position - _player.transform.position).normalized;
            Quaternion lookAt = Quaternion.LookRotation(direction, Vector3.up);
            _player.Rotate(lookAt);
        }

        private void OnLocationCleared()
        {
            Unsubscribe();
            UpdateCurrentLocationIndex();
            MoveToNextWaypoint();
        }
        
        private void Unsubscribe()
        {
            _currentLocation.LocationCleared -= OnLocationCleared;
            _currentLocation.EnemyDied -= RotatePlayer;
        }

        private void UpdateCurrentLocationIndex()
        {
            if (_currentLocationIndex == _locations.Length - 1)
            {
                EndLevel();
                return;
            }
            _currentLocationIndex++;
        }

        private void MoveToNextWaypoint() => 
            _player.SetNavMeshPosition(_locations[_currentLocationIndex].Waypoint.position);
        
        private void EndLevel()
        {
            _player.DisableInput();
            LevelEnded?.Invoke();
        }
    }
}