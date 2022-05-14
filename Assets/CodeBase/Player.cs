using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace CodeBase
{
    public class Player : MonoBehaviour
    {
        private const float TimeDelay = 0.1f;
        private const float DeltaDistance = 1.5f;
        
        private PlayerAnimator _playerAnimator;
        private Weapon _weapon;
        private NavMeshAgent _navMesh;
        private Camera _mainCamera;
        private PlayerControls _playerInput;

        public event Action WaypointReached;

        private void Start()
        {
            GetComponents();
            _playerInput = new PlayerControls();
        }

        private void GetComponents()
        {
            _mainCamera = Camera.main;
            _navMesh = GetComponent<NavMeshAgent>();
            _playerAnimator = GetComponent<PlayerAnimator>();
            _weapon = GetComponent<Weapon>();
        }

        public void ActivateInput()
        {
            _playerInput.Enable();
            _playerInput.Player.Click.performed += Clicked;
        }

        public void DisableInput()
        {
            _playerInput.Player.Click.performed -= Clicked;
            _playerInput.Disable();
        }

        private void Clicked(InputAction.CallbackContext _)
        {
            if (Physics.Raycast(
                _mainCamera.ScreenPointToRay(_playerInput.Player.Position.ReadValue<Vector2>()),
                out var hit))
                _weapon.Shoot(hit.point);
        }

        public void SetPosition(Vector3 startPosition) => 
            transform.position = startPosition;

        public void SetNavMeshPosition(Vector3 position)
        {
            _navMesh.SetDestination(position);
            _playerAnimator.Run();
            StartCoroutine(CheckDestinationReached());
        }

        private IEnumerator CheckDestinationReached()
        {
            yield return new WaitForSeconds(TimeDelay);

            while (_navMesh.remainingDistance > DeltaDistance)
                yield return new WaitForEndOfFrame();
            
            _playerAnimator.Idle();
            WaypointReached?.Invoke();
        }
    }
}
