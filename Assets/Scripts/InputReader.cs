using System;
using Player;
using UnityEngine;

public class InputReader : MonoBehaviour 
{
    [SerializeField] private PlayerEntity _playerEntity;
    private float _horizontalDirection;
    private float _verticalDirection;

    private void Update () 
    {

        if(Input.GetButtonDown("Jump")){
            _playerEntity.Jump();
        }

       _horizontalDirection =  Input.GetAxisRaw("Horizontal");
       _verticalDirection =  Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate () {
        _playerEntity.MoveHorizontally(_horizontalDirection);
        _playerEntity.MoveVertically(_verticalDirection);
    }

} 