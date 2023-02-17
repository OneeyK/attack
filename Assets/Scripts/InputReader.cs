using System;
using Player;
using UnityEngine;

public class InputReader : MonoBehaviour 
{
    [SerializeField] private PlayerEntity _playerEntity;
    private float _direction;
    private void Update () 
    {
       _direction =  Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate () {
        _playerEntity.MoveHorizontally(_direction);
    }

} 