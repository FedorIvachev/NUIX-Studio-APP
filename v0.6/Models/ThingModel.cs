using System.Collections.Generic;

[System.Serializable]
public class ThingModel
{
    public List<ItemModel2> members;
    public string name;

    public ThingModel(string name)
    {
        this.name = name;
        this.members = new List<ItemModel2>();
    }
}