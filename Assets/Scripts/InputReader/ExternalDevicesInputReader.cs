using System;
using Core.Services.Updater;
using InputReader;
using Player;
using UnityEngine;

public class ExternalDevicesInputReader : IEntityInputSource, IDisposable
{
    
    public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
    public float VerticalDirection => Input.GetAxisRaw("Vertical");
    public bool Jump { get; private set; }
    public bool Attack { get; private set; }

    public ExternalDevicesInputReader()
    {
        ProjectUpdater.Instance.UpdateCalled += OnUpdate;
        
    }

    public void Dispose() => ProjectUpdater.Instance.UpdateCalled -= OnUpdate;
    
    
    public void ResetOneTimeAction()
    {
        Jump = false;
        Attack = false;
    }
    
    private void OnUpdate () 
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

   

} 