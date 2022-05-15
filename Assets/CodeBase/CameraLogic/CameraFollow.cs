using Cinemachine;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        
        public void Follow(Transform player)
        {
            _camera.Follow = player;
            _camera.LookAt = player;
        }
    }
}