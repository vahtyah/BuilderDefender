using System;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject _spriteGameObject;
    private bool _isShow;

    private void Awake()
    {
        var resourceNearbyOverlay = transform.Find("ResourceGeneratorOverlay").GetComponent<ResourceNearByOverlay>();
        _spriteGameObject = transform.Find("Sprite").gameObject;

        BuildingManager.Instance.SelectBuildingType += (sender, so) =>
        {
            if (so == null)
            {
                Hide();
                resourceNearbyOverlay.Hide();
            }
            else
            {
                Show(so.sprite);
                if (so.hasResourceGeneratorData)
                {
                    resourceNearbyOverlay.Show(so.resourceGeneratorData);  
                }
            }
        };

        Hide();
    }

    private void Update()
    {
        if (_isShow)
        {
            transform.position = UtilsClass.GetMouseWorldPosition();
        }
    }

    private void Hide()
    {
        _spriteGameObject.SetActive(false);
        _isShow = false;
    }

    private void Show(Sprite ghostSprite)
    {
        _spriteGameObject.SetActive(true);
        _spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
        _isShow = true;
    }
}