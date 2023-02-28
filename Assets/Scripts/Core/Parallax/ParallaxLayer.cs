using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.Parallax
{
public class ParallaxLayer
{



    private readonly float _speed;
    private readonly List<Transform> _layers;
  
    private readonly float _layerHorizontalSize;
    // Start is called before the first frame update


    public ParallaxLayer (SpriteRenderer initialPart, float speed, Transform parentTransform) 
    {
        _speed = speed;

         Sprite sprite = initialPart.sprite;
        _layerHorizontalSize = sprite.texture.width / sprite.pixelsPerUnit;

        _layers = new List<Transform>
        {
            initialPart.transform
        };
        float positionX = _layers[0].position.x;
        float positionY = _layers[0].position.y;
        Vector2 secondPartPosition = new Vector2(_layerHorizontalSize + (float)positionX, positionY);
        Transform secondPart = Object.Instantiate(initialPart, secondPartPosition, Quaternion.identity).transform;
        secondPart.parent = parentTransform;
        _layers.Add(secondPart);

    }

 

    public void UpdateLayer(float targetPosition, float previousTargetPosition) {
        MoveParts(previousTargetPosition - targetPosition);
        FixLayerPositions(targetPosition);

    }

    private void MoveParts(float deltaMovement) 
    {
        foreach (var layer in _layers)
        {
            Vector2 layerPosition = layer.position;
            layerPosition.x += + deltaMovement * _speed;
            layer.position = layerPosition;
        }
        
    }

    private void FixLayerPositions (float targetPosition) 
    {
        Transform activeLayer = _layers.Find(layer => IsLayerActive(layer, targetPosition));
        Transform layerToMove = _layers.Find(layer => !IsLayerActive(layer, targetPosition));
        if(activeLayer == null || layerToMove == null) 
            return;
        
        float relativePosition = activeLayer.position.x;
        int direction = relativePosition > targetPosition ? -1 : 1;
        if (layerToMove.position.x > relativePosition && direction > 0 || 
            layerToMove.position.x < relativePosition && direction < 0)
            return;

        layerToMove.position = new Vector3(relativePosition + _layerHorizontalSize * direction, 0);

    }

    private bool IsLayerActive (Transform layer, float targetPosition) => 
        Mathf.Abs(layer.position.x - targetPosition) <= _layerHorizontalSize / 2;
    

   
}
}
