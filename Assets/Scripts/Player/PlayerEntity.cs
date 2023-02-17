using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player 
{
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEntity : MonoBehaviour 
{
    [SerializeField] private float _horizontalSpeed;
    private Rigidbody2D _rigidbody;
    [SerializeField] private bool _faceRight;
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        

    }


    public void MoveHorizontally(float direction) {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = direction * _horizontalSpeed;
        _rigidbody.velocity = velocity;
    }

    private void SetDirrection (float direction) {
        if ((_faceRight && direction < 0) || (!_faceRight && direction > 0)) {

        }
    }

    private void Flip() {
        transform.Rotate(0, 180, 0);
        _faceRight = !_faceRight;
    }
}

}

