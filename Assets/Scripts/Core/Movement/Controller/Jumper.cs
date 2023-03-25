using Core.Movement.Data;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class Jumper
    {
        private readonly JumpData _jumpData;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _maxVerticalSize;
        private readonly Transform _transform;

        private float _startJumpVerticalPosition;
        
        public bool isJump { get; private set; }

        public Jumper(Rigidbody2D rigidbody2D, JumpData jumpData, float maxVerticalSize)
        {
            _rigidbody = rigidbody2D;
            _jumpData = jumpData;
            _maxVerticalSize = maxVerticalSize;
            _transform = _rigidbody.transform;
        }
        
        
        public void Jump () {
            if (isJump) {
                return;
            }

            isJump = true;
            _rigidbody.AddForce(Vector2.up * _jumpData.JumpForce);
            _rigidbody.gravityScale = _jumpData.GravityScale;
            _startJumpVerticalPosition = _rigidbody.position.y;

        }
        
        public void UpdateJump() {
            if(_rigidbody.velocity.y < 0 && _rigidbody.position.y <= _startJumpVerticalPosition){
                ResetJump();
                return;
            }
        }

        private void ResetJump() {
            isJump = false;
            _rigidbody.position = new Vector2(_rigidbody.position.x, _startJumpVerticalPosition);
            _rigidbody.gravityScale = 0;
        }
    }
}