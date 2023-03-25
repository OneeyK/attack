using Core.Movement.Data;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class DirectionalMover
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;
        private readonly DirectionalMovementData _directionalMovementData;
        private readonly float _sizeModificator; 

        private Vector2 _movement;

        public bool _faceRight { get; set; }
        public bool IsMoving => _movement.magnitude > 0;
        
        public DirectionalMover(Rigidbody2D rigidbody2D, DirectionalMovementData directionalMovementData)
        {
            _rigidbody = rigidbody2D;
            _transform = rigidbody2D.transform;
            _directionalMovementData = directionalMovementData;
            float positionDifference = _directionalMovementData.MaxVerticalPosition - _directionalMovementData.MinVerticalPosition;
            float sizeDifference = _directionalMovementData.MaxSize - _directionalMovementData.MinSize;
            _sizeModificator = sizeDifference / positionDifference;
            _faceRight = _directionalMovementData.FaceRight;
        }
        
        public void MoveHorizontally(float direction)
        {
            _movement.x = direction;
            SetDirrection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _directionalMovementData.HorizontalSpeed;
            _rigidbody.velocity = velocity;
        }

        public void MoveVertically(float direction) 
        {
            _movement.y = direction;

            Vector2 velocity = _rigidbody.velocity;
            velocity.y = direction * _directionalMovementData.VerticalSpeed;
            _rigidbody.velocity = velocity;

            if (direction == 0) {
                return;
            } 

            float verticalPosition = Mathf.Clamp(_rigidbody.position.y, _directionalMovementData.MinVerticalPosition, _directionalMovementData.MaxVerticalPosition);
            _rigidbody.position = new Vector2(_rigidbody.position.x, verticalPosition);
            UpdateSize();

        }
        
        public void UpdateSize () {
            float verticalDelta = _directionalMovementData.MaxVerticalPosition - _transform.position.y;
            float currentSizeModificator = _directionalMovementData.MinSize + _sizeModificator * verticalDelta;
            _transform.localScale = Vector2.one * currentSizeModificator;
        }

        private void SetDirrection (float direction) {
            if ((_faceRight && direction < 0) || (!_faceRight && direction > 0)) {
                Flip();
            }
        }

        private void Flip() {
            _transform.Rotate(0, 180, 0);
            _faceRight = !_faceRight;
        }
    }
}