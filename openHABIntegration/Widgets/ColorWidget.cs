using UnityEngine;

public class ColorWidget : ItemWidget
{
    private Color CustomColor;


    void Start()
    {
        // Add or get controller component
        if (GetComponent<ItemController>() != null)
        {
            _itemController = GetComponent<ItemController>();
        }
        else
        {
            _itemController = gameObject.AddComponent<ItemController>();
        }

        _itemController.Initialize(_Server, _Item, _SubscriptionType);

    }


    public void ExtractColorFromMaterial(MeshRenderer meshRenderer)
    {

        CustomColor = meshRenderer.material.color;

        _itemController.SetItemStateAsColor(CustomColor);

    }
}