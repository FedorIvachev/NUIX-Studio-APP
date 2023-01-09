using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Classes to cache the parameters of items and actions and to be serialized from/deserialized into
/// </summary>
[System.Serializable]
public class ItemData
{
    public string itemID;
    public float position_x;
    public float position_y;
    public float position_z;
    public float rotation_x;
    public float rotation_y;
    public float rotation_z;
    public float rotation_w;
    public ItemType itemType;


    public ItemData(ItemType itemType = ItemType.DEFAULT, 
        float position_x = 0f, float position_y = 0f, float position_z = 0f, 
        float rotation_x = 0f, float rotation_y = 0f, float rotation_z = 0f, float rotation_w = 0f,
        string itemID = "")
    {
        this.position_x = position_x;
        this.position_y = position_y;
        this.position_z = position_z;
        this.rotation_x = rotation_x;
        this.rotation_y = rotation_y;
        this.rotation_z = rotation_z;
        this.rotation_w = rotation_w;
        this.itemType = itemType;

        this.itemID = itemID;   //Guid.NewGuid().ToString(); ;
    }


    public override string ToString()
    {
        return $"Item of type {itemType} at " +
            $"position x = {string.Format("{0:0.00}", position_x)}, " +
            $"y = {string.Format("{0:0.00}", position_y)}, " +
            $"z = {string.Format("{0:0.00}", position_z)}, " +
            $"rotation x = {string.Format("{0:0.00}", rotation_x)}, " +
            $"y = {string.Format("{0:0.00}", rotation_y)}, " +
            $"z = {string.Format("{0:0.00}", rotation_z)}, " +
            $"w = {string.Format("{0:0.00}", rotation_w)}";
    }

}

[System.Serializable]
public class TextPlateItemData : ItemData
{
    public string text = "";
    public bool isKeyboardOpen = true;

    public TextPlateItemData(ItemData itemData, string text, bool isKeyboardOpen) 
        : base (itemData.itemType, itemData.position_x, itemData.position_y, itemData.position_z,
    itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w, itemData.itemID)
    {
        this.text = text;
        this.isKeyboardOpen = isKeyboardOpen;
    }
    public override string ToString()
    {
        return base.ToString() + $" text = {text}, keyboard is open = {isKeyboardOpen}";
    }
}

[System.Serializable]
public class LightItemData : ItemData
{
    public bool isTurnedON;
    public int colorIndex;

    public LightItemData(ItemData itemData, bool isTurnedON, int colorIndex)
        : base(itemData.itemType, itemData.position_x, itemData.position_y, itemData.position_z,
    itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w, itemData.itemID)
    {
        this.isTurnedON = isTurnedON;
        this.colorIndex = colorIndex;
    }

    public override string ToString()
    {
        return base.ToString() + $" light is on : {isTurnedON}, color number : {colorIndex}";
    }
}


[System.Serializable]
public class ButtonItemData : ItemData
{
    // TODO: change to enum
    public int type;

    public ButtonItemData(ItemData itemData, int type)
        : base(itemData.itemType, itemData.position_x, itemData.position_y, itemData.position_z,
    itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w, itemData.itemID)
    {
        this.type = type;
    }
    public override string ToString()
    {
        return base.ToString() + $" type : {type}";
    }
}

[System.Serializable]
public class ImageItemData : ItemData
{
    public int imageIndex;
    public ImageItemData(ItemData itemData, int imageIndex)
        : base(itemData.itemType, itemData.position_x, itemData.position_y, itemData.position_z,
    itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w, itemData.itemID)
    {
        this.imageIndex = imageIndex;
    }
    public override string ToString()
    {
        return base.ToString();
    }
}

[System.Serializable]
public class VideoItemData : ItemData
{
    public int videoClipIndex;
    public VideoItemData(ItemData itemData, int videoClipIndex)
        : base(itemData.itemType, itemData.position_x, itemData.position_y, itemData.position_z,
    itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w, itemData.itemID)
    {
        this.videoClipIndex = videoClipIndex;
    }
    public override string ToString()
    {
        return base.ToString();
    }
}

[System.Serializable]
public class AudioItemData : ItemData
{
    public int audioClipIndex;
    public AudioItemData(ItemData itemData, int audioClipIndex)
        : base(itemData.itemType, itemData.position_x, itemData.position_y, itemData.position_z,
    itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w, itemData.itemID)
    {
        this.audioClipIndex = audioClipIndex;
    }
    public override string ToString()
    {
        return base.ToString();
    }
}

[System.Serializable]
public class CameraItemData : ItemData
{
    public CameraItemData(ItemData itemData)
        : base(itemData.itemType, itemData.position_x, itemData.position_y, itemData.position_z,
    itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w, itemData.itemID)
    {
        
    }
    public override string ToString()
    {
        return base.ToString();
    }
}

[System.Serializable]
public class WeightScalerItemData : ItemData
{
    public float requiredWeight;

    public WeightScalerItemData(ItemData itemData, float requiredWeight)
        : base(itemData.itemType, itemData.position_x, itemData.position_y, itemData.position_z,
    itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w, itemData.itemID)
    {
        this.requiredWeight = requiredWeight;
    }
    public override string ToString()
    {
        return base.ToString() + $" required weight : {requiredWeight}";
    }
}


[System.Serializable]
public class ItemsData
{
    public List<ItemData> itemsData;

    public List<TextPlateItemData> textPlateItemsData;

    public List<LightItemData> lightItemsData;

    public List<ButtonItemData> buttonItemsData;

    public List<ImageItemData> imageItemsData;

    public List<VideoItemData> videoItemsData;

    public List<AudioItemData> audioItemsData;

    public List<CameraItemData> cameraItemsData;

    public List<WeightScalerItemData> weightScalerItemsData;

    public List<ActionData> actionData;


    public IEnumerable<ItemData> ConcatItemsData()
    {
        return itemsData.Concat(textPlateItemsData).
            Concat(lightItemsData).
            Concat(buttonItemsData).
            Concat(imageItemsData).
            Concat(videoItemsData).
            Concat(audioItemsData).
            Concat(cameraItemsData).
            Concat(weightScalerItemsData);
    }

    public ItemsData()
    {
        this.itemsData = new List<ItemData>();
        this.textPlateItemsData = new List<TextPlateItemData>();
        this.lightItemsData = new List<LightItemData>();
        this.buttonItemsData = new List<ButtonItemData>();
        this.imageItemsData = new List<ImageItemData>();
        this.videoItemsData = new List<VideoItemData>();
        this.audioItemsData = new List<AudioItemData>();
        this.cameraItemsData = new List<CameraItemData>();
        this.weightScalerItemsData = new List<WeightScalerItemData>();

        this.actionData = new List<ActionData>();
    }

    public override string ToString()
    {
        string res = "";
        foreach (ItemData itemData in ConcatItemsData())
        {
            res += itemData.ToString() + Environment.NewLine;
        }
        foreach (ActionData action in actionData)
        {
            res += action.ToString() + Environment.NewLine;
        }
        return res;
    }
}


[System.Serializable]
public class ActionData
{
    public string actionID;

    public string senderID;
    public string senderMethod;
    public List<String> senderArgs;

    public string receiverID;
    public string receiverMethod;
    public List<String> receiverArgs;

    public ActionData()
    {

    }

    public ActionData(string actionID,
        string senderID, string senderMethod, List<String> senderArgs,
        string receiverID, string receiverMethod, List<String> receiverArgs)
    {
        this.senderID = senderID;
        this.senderMethod = senderMethod;
        this.senderArgs = senderArgs;

        this.receiverID = receiverID;
        this.receiverMethod = receiverMethod;
        this.receiverArgs = receiverArgs;
        this.actionID = actionID;
    }

    public ActionData(string senderID, string senderMethod)
    {
        this.senderID = senderID;
        this.senderMethod = senderMethod;
    }


    public override string ToString()
    {
        return $" Action id {actionID}" +
            $" sender {senderID} " +
            $"method {senderMethod} " +
            $"args {senderArgs} " +
            $"receicer {receiverID} " +
            $"method {receiverMethod} " +
            $"args {receiverArgs}";
    }
}