using System.Collections.Generic;

public class SemanticModel
{
    public static SemanticModel instance;
    public static SemanticModel getInstance()
    {
        if (instance == null)
            instance = new SemanticModel();
        return instance;
    }

    public Dictionary<string, Item> _items = new Dictionary<string, Item>();
}