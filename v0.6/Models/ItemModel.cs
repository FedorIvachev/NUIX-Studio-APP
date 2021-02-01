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
