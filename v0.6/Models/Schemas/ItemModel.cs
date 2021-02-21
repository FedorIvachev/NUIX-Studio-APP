using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommandOption
{
    public string command;
    public string label;
}

public class CommandDescription
{
    public List<CommandOption> commandOptions;
}

public class StateOption
{
    public string value;
    public string label;
}
public class StateDescription
{
    public int step;
    public int minimum;
    public int maximum;
    public string pattern;
    public bool readOnly;
    public List<StateOption> options;
}

[System.Serializable]
public class GroupFunctionDTO
{
    public string name;
    public List<string> Params; // using capitalization to make it different from c# params
}

[System.Serializable]
public class ItemListModel
{
    public List<EnrichedItemDTO> itemList;
}

[System.Serializable]
public class EnrichedItemDTO : ItemDTO
{
    public string link;
    public string state;
    public string transformedState;
    public StateDescription stateDescription;
    public CommandDescription commandDescription;
    public object metadata;
    public bool editable;
}

[System.Serializable]
public class EquipmentItemModelList
{
    public List<EnrichedGroupItemDTO> result;
}

[System.Serializable]
public class EnrichedGroupItemDTO : EnrichedItemDTO
{
    public List<EnrichedItemDTO> members;
    public string groupType;
    public GroupFunctionDTO function;
}

[Serializable]
public class GroupItemDTO
{
    public string type;
    public string name;
    public string label;
    public string category;
    public List<string> tags;
    public List<string> groupNames;
    public string groupType;
    public GroupFunctionDTO function;
}

public class ItemDTO
{
    public string type;
    public string name;
    public string label;
    public string category;
    public List<string> tags;
    public List<string> groupNames;
}