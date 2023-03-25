using System;
using UnityEngine;
using AnimatorController = Player.PlayerAnimations.AnimatorController;

namespace Player 
{
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEntity : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [Header("HorizontalMovement")]
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private bool _faceRight;

    [Header("verticalMovement")]
    [SerializeField] private float _veticalSpeed;
    [SerializeField] private float _minSize;
    [SerializeField] private float _maxSize;
    [SerializeField] private float _maxVerticalPosition;
    [SerializeField] private float _minVerticalPosition;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityScale;

    private Rigidbody2D _rigidbody;
    private float _sizeModificator;
    private bool _isJump;
    private float _startJumpVerticalPosition;

    private Vector2 _movement;
    private AnimationType _currenAnimationtype;

    private event Action ActionRequested;
    private event Action AnimationEnded;


 
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        float positionDifference = _maxVerticalPosition - _minVerticalPosition;
        float sizeDifference = _maxSize - _minSize;
        _sizeModificator = sizeDifference / positionDifference;
        UpdateSize();
    }

    private void Update() {
        if (_isJump) {
            UpdateJump();
        }
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        PlayAnimation(AnimationType.Idle, true);
        PlayAnimation(AnimationType.Walk, _movement.magnitude > 0);
        PlayAnimation(AnimationType.Jump, _isJump);
        /*PlayAnimation(AnimationType.Idle, true);
        PlayAnimation(AnimationType.Walk, _movement.magnitude > 0);
        PlayAnimation(AnimationType.Jump, _isJump);*/
    }


    public void MoveHorizontally(float direction)
    {
        _movement.x = direction;
        SetDirrection(direction);
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = direction * _horizontalSpeed;
        _rigidbody.velocity = velocity;
    }

    public void MoveVertically(float direction) {

        if (_isJump) {
            return;
        }

        _movement.y = direction;

        Vector2 velocity = _rigidbody.velocity;
        velocity.y = direction * _veticalSpeed;
        _rigidbody.velocity = velocity;

         if (direction == 0) {
            return;
        } 

        float verticalPosition = Mathf.Clamp(transform.position.y, _minVerticalPosition, _maxVerticalPosition);
        _rigidbody.position = new Vector2(_rigidbody.position.x, verticalPosition);
        UpdateSize();

    }

    public void Jump () {
        if (_isJump) {
            return;
        }

        _isJump = true;
        _rigidbody.AddForce(Vector2.up * _jumpForce);
        _rigidbody.gravityScale = _gravityScale;
        _startJumpVerticalPosition = transform.position.y;

    }

    private void UpdateSize () {
        float verticalDelta = _maxVerticalPosition - transform.position.y;
        float currentSizeModificator = _minSize + _sizeModificator * verticalDelta;
        transform.localScale = Vector2.one * currentSizeModificator;
    }

    private void SetDirrection (float direction) {
        if ((_faceRight && direction < 0) || (!_faceRight && direction > 0)) {
            Flip();
        }
    }

    private void Flip() {
        transform.Rotate(0, 180, 0);
        _faceRight = !_faceRight;
    }

    private void UpdateJump() {
        if(_rigidbody.velocity.y < 0 && _rigidbody.position.y <= _startJumpVerticalPosition){
            ResetJump();
            return;
        }
    }

    private void ResetJump() {
        _isJump = false;
        _rigidbody.position = new Vector2(_rigidbody.position.x, _startJumpVerticalPosition);
        _rigidbody.gravityScale = 0;
    }
    
    
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

