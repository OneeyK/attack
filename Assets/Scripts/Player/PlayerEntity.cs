using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player 
{
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEntity : MonoBehaviour 
{

    [Header("HorizontalMovement")]
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private bool _faceRight;

    [Header("verticalMovement")]
    [SerializeField] private float _veticalSpeed;
    [SerializeField] private float _minSize;
    [SerializeField] private float _maxSize;
    [SerializeField] private float _maxVerticalPosition;
    [SerializeField] private float _minVerticalPosition;

    private Rigidbody2D _rigidbody;
    private float _sizeModificator;


 
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        float positionDifference = _maxVerticalPosition - _minVerticalPosition;
        float sizeDifference = _maxSize - _minSize;
        _sizeModificator = sizeDifference / positionDifference;
        UpdateSize();
    }


    public void MoveHorizontally(float direction) {
        SetDirrection(direction);
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = direction * _horizontalSpeed;
        _rigidbody.velocity = velocity;
    }

    public void MoveVertically(float direction) {
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
}

}

