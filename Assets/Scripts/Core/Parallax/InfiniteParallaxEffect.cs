using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
namespace Core.Parallax
{
public class InfiniteParallaxEffect : MonoBehaviour 
{
    [SerializeField] private List<ParallaxPart> _parts;
    [SerializeField] private Transform _target;

    private List<ParallaxLayer> _layers; 

    private float _previousTargetPosition;

    private void Start() {
        _previousTargetPosition = _target.position.x;
        _layers = new List<ParallaxLayer>();
        foreach (var part in _parts)
        {
            Transform layerParent = new GameObject().transform;
            layerParent.transform.parent = transform;
            part.SpriteRenderer.transform.parent = layerParent;
            ParallaxLayer parallaxLayer = new ParallaxLayer(part.SpriteRenderer, part.Speed, layerParent);
            _layers.Add(parallaxLayer);
        }
    }

    private void LateUpdate() {
    
        foreach (var layer in _layers)
        {
           layer.UpdateLayer( _target.position.x, _previousTargetPosition);
        }
        _previousTargetPosition = _target.position.x;
    }

    [Serializable]
    private class ParallaxPart
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer;
        [field: SerializeField] public float Speed;
        

    }
}
}