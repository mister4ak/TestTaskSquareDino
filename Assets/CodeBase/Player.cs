using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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

        public event Action WaypointReached;

        private void Start()
        {
            _mainCamera = Camera.main;
            _navMesh = GetComponent<NavMeshAgent>();
            _playerAnimator = GetComponent<PlayerAnimator>();
            _weapon = GetComponent<Weapon>();
        }

        public void SetPosition(Vector3 startPosition) => 
            transform.position = startPosition;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out var hit))
                    _weapon.Shoot(hit.point);
        }
        
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
