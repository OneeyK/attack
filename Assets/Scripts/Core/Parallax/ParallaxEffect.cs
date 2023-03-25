using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


namespace Core.Parallax
{
  public class ParallaxEffect : MonoBehaviour
  {

    [SerializeField] private List<ParallaxPlayer> _layers;
    [SerializeField] private Transform _target;

    private float _previousTargetPosition;

    private void Start() {
        _previousTargetPosition = _target.position.x;
    }

    private void LateUpdate() {
        float deltaMovement = _previousTargetPosition - _target.position.x;
        foreach (var layer in _layers)
        {
            Vector2 layerPosition = layer.Transform.position;
            layerPosition.x = layerPosition.x + deltaMovement * layer.Speed;
            layer.Transform.position = layerPosition;
        }
        _previousTargetPosition = _target.position.x;
    }

    [Serializable]
    private class ParallaxPlayer
    {
        [field: SerializeField] public Transform Transform;
        [field: SerializeField] public float Speed;
        

    }
    
}  
}

