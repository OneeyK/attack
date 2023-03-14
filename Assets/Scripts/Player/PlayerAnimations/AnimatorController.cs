using System;
using UnityEngine;

namespace Player.PlayerAnimations
{
    public abstract class AnimatorController : MonoBehaviour
    {

        private AnimationType _currentAnimationType;

        public event Action ActionRequested;
        public event Action AnimationEnded;
        
        public void PlayAnimation(AnimationType animationType, bool active)
        {
            if (!active)
            {
                if (_currentAnimationType == AnimationType.Idle || _currentAnimationType != animationType)
                {
                    return;
                }
                _currentAnimationType = AnimationType.Idle;
                PlayAnimation(_currentAnimationType);
                return;
            }
        
            if (_currentAnimationType > animationType)
            {
                return;
            }

            _currentAnimationType = animationType;
            PlayAnimation(_currentAnimationType);

        }
        
        protected abstract void PlayAnimation(AnimationType animationType);

        protected void OnActionRequested() => ActionRequested?.Invoke();
        protected void OnAnimationEnded() => AnimationEnded?.Invoke();
    }
}