using System;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField] private bool runOnce;
    [SerializeField] private float positionOffsetY;
    
    
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        _spriteRenderer.sortingOrder = (int)(-(transform.position.y + positionOffsetY) * 5);
        if (runOnce)
        {
            Destroy(this);
        }
    }
}