using System;
using System.Collections;
using CodeBase.Input;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace CodeBase.Player
{
    public class Player : MonoBehaviour
    {
        private const float TimeDelay = 0.1f;
        private const float DeltaDistance = 1.5f;
        private const float RotationTime = 2f;

        private Camera _mainCamera;
        private NavMeshAgent _navMesh;
        private PlayerAnimator _playerAnimator;
        private Weapon.Weapon _weapon;
        private PlayerControls _playerInput;
        
        public event Action WaypointReached;
        public event Action RotateCompleted;

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
            _weapon = GetComponent<Weapon.Weapon>();
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
            //Debug.Log(hit.collider.name);
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

        public IEnumerator Rotate(Quaternion to)
        {
            float timer = 0f;
            
            while (timer < RotationTime)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, to, timer / RotationTime);
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            RotateCompleted?.Invoke();
        }
    }
}
