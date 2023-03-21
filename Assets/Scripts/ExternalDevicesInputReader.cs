using System;
using Player;
using UnityEngine;

public class ExternalDevicesInputReader : IEntityInputSource
{
    
    public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
    public float VerticalDirection => Input.GetAxisRaw("Vertical");
    public bool Jump { get; private set; }
    public bool Attack { get; private set; }

    public void OnUpdate () 
    {

        if(Input.GetButtonDown("Jump"))
        {
            Jump = true;
        }

        if (Input.GetButtonDown("Fire1"))
            Attack = true;
        
    }

    /*private void FixedUpdate () {
        _playerEntity.MoveHorizontally(_horizontalDirection);
        _playerEntity.MoveVertically(_verticalDirection);
    }*/

    public void ResetOneTimeAction()
    {
        Jump = false;
        Attack = false;
    }

} 