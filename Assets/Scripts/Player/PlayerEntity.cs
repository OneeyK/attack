using System;
using Core.Movement.Controller;
using Core.Movement.Data;
using Core.StatSystem;
using UnityEngine;
using AnimatorController = Player.PlayerAnimations.AnimatorController;

namespace Player 
{
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEntity : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    [SerializeField] private DirectionalMovementData _directionalMovementData;
    [SerializeField] private JumpData _jumpData;
    
    private Rigidbody2D _rigidbody;
    private bool _isJump;
    private float _startJumpVerticalPosition;

    private DirectionalMover _directionalMover;
    private Jumper _jumper;
    
    private AnimationType _currenAnimationtype;

    private event Action ActionRequested;
    private event Action AnimationEnded;


 
    // Start is called before the first frame update
    public void Initialize(IStatValueGiver statValueGiver)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _directionalMover = new DirectionalMover(_rigidbody, _directionalMovementData, statValueGiver);
        _jumper = new Jumper(_rigidbody, _jumpData, _directionalMovementData.MaxSize, statValueGiver);
        _directionalMover.UpdateSize();
        
    }

    private void Update() {
        if (_jumper.isJump)
        {
            _jumper.UpdateJump();
        }
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        PlayAnimation(AnimationType.Idle, true);
        PlayAnimation(AnimationType.Walk, /*_movement.magnitude > 0*/ _directionalMover.IsMoving);
        PlayAnimation(AnimationType.Jump, /*_isJump*/ _jumper.isJump);
        /*PlayAnimation(AnimationType.Idle, true);
        PlayAnimation(AnimationType.Walk, _movement.magnitude > 0);
        PlayAnimation(AnimationType.Jump, _isJump);*/
    }
    

    public void MoveHorizontally(float direction) => _directionalMover.MoveHorizontally(direction);
    

    public void MoveVertically(float direction) {
        
        if (_jumper.isJump) {
            return;
        }

        _directionalMover.MoveVertically(direction);

    }
    
    public void Jump() => _jumper.Jump();
    
    
    public bool PlayAnimation(AnimationType animationType, bool active)
    {
        if (!active)
        {
            if (_currenAnimationtype == AnimationType.Idle || _currenAnimationtype != animationType)
            {
                return false;
            }
            _currenAnimationtype = AnimationType.Idle;
            PlayAnimation(_currenAnimationtype);
            return false;
        }
        
        if (_currenAnimationtype > animationType)
        {
            return false;
        }

        _currenAnimationtype = animationType;
        PlayAnimation(_currenAnimationtype);
        return true;
    }
    
    protected  void PlayAnimation(AnimationType animationType)
    {
        _animator.SetInteger(nameof(AnimationType), (int)animationType);
    }

    public void StartAttck()
    {
        /*if (!PlayAnimation(AnimationType.Attack, true))
        {
            return;
        }*/

        ActionRequested += Attack;
        AnimationEnded += EndAttack;
    }
    
    
    
    protected void OnActionRequested() => ActionRequested?.Invoke();
    protected void OnAnimationEnded() => AnimationEnded?.Invoke();

    private void Attack()
    {
        Debug.Log("Attack");
    }
    
    private void EndAttack()
    {
        ActionRequested -= Attack;
        AnimationEnded -= EndAttack;
        PlayAnimation(AnimationType.Attack, false);
    }
}

}

