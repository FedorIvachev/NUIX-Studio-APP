using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDescription
{
    //public int step;
    public string pattern;
    public bool readOnly;
    public List<object> options;
}

[System.Serializable]
public class GroupFunctionDTO
{
    public string name;
    public List<string> Params; // using capitalization to make it differ from c# params
}

public class ItemModel
{
    public string link;
    public string state;
    public StateDescription stateDescription;
    public bool editable;
    public string type;
    public string name;
    public string label;
    public string category;
    public List<object> tags;
    public List<object> groupNames;
}


[System.Serializable]
public class ItemListModel
{
    public List<ItemModel2> itemList;
}

[System.Serializable]
public class ItemModel2
{
    public string link;
    public string state;
    public bool editable;
    public string type;
    public string name;
    public string label;
    public string category;
    public List<object> tags;
    public List<object> groupNames;
}

[System.Serializable]
public class EquipmentItemModelList
{
    public List<EquipmentItemModel> result;
}

[System.Serializable]
public class EquipmentItemModel
{
    public List<ItemModel2> members;
    public string link;
    public string state;
    public bool editable;
    public string type;
    public string name;
    public string label;
    public string category;
    public List<object> tags;
    public List<object> groupNames;
}

[Serializable]
public class PutItemResponseModel
{
    public string link;
    public string state;
    public string type;
    public string name;
    public string label;
    public string category;
    public List<object> tags;
    public List<object> groupNames;
}

[Serializable]
public class GroupItemDTO
{
    public string type;
    public string name;
    public string label;
    public string category;
    public List<object> tags;
    public List<object> groupNames;
    public string groupType;
    public GroupFunctionDTO function;
}