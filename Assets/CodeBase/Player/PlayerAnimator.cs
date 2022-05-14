using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private readonly int _isStayingHash = Animator.StringToHash("IsStaying");

        public void Run() => 
            _animator.SetBool(_isStayingHash, false);

        public void Idle() => 
            _animator.SetBool(_isStayingHash, true);
    }
}