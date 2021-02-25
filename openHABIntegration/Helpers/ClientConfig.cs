using System.Collections.Generic;
using UnityEngine;

public class ClientConfig
{
    public string _ServerURL = "http://oh.Token.e8X5h5IrTU7wqXtNXvGzcaYbzVhIqVC8IO88yH5CToKzGtUfO6jIM0ldj6RrxQHO5TPnSEbkO5CM6uvJB5Ag:@localhost:8080"; //URL to server rest api ie. http://openhab:8080/rest

    // Links to the Item Widgets prefabs
    public Dictionary<string, GameObject> _widgetPrefabs = new Dictionary<string, GameObject>();


    // Links to the Category classes mprefabs
    public Dictionary<string, GameObject> _categoryPrefabs = new Dictionary<string, GameObject>();

    private static ClientConfig instance;
    public static ClientConfig getInstance()
    {
        if (instance == null)
            instance = new ClientConfig();
        return instance;
    }

    public string Bearer { get; set; } = "eyJraWQiOm51bGwsImFsZyI6IlJTMjU2In0.eyJpc3MiOiJvcGVuaGFiIiwiYXVkIjoib3BlbmhhYiIsImV4cCI6MTYxNDI2MzM3NywianRpIjoiSXZxTnFCT0p6UVU0cVRLTW5YZ1lMQSIsImlhdCI6MTYxNDI1OTc3NywibmJmIjoxNjE0MjU5NjU3LCJzdWIiOiJhZG1pbiIsImNsaWVudF9pZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6ODA4MCIsInNjb3BlIjoiYWRtaW4iLCJyb2xlIjpbImFkbWluaXN0cmF0b3IiXX0.GB_X0Kyr_LCKG_jUa7h9RNdODTLhXYSQVOunZMyhfJ2t9HbBjVBV1CsAOgehmrc9xAExa5tZmaS-IkdgTkzRKDlcnoDo6HC_ZX7YB5xyxpMDmj24zpHhVQwWQ-WJDG8IILJC-qBAe3_6rO-nnWRLjYweowQoES0TGgM8i-l7J81jC_XC-yspS5atDGMCDIP4PqSMHZq7PV_CaTy_8YR00Be-i297zWjiCe5ncSqioG1U7QylbbbvTetfEJkUiySYzXCk6BQtTZzuaaXtbfr7FkLwHlD0hPGbXOa8s8nm55XiqXwygo9D_ZbyKDwNe5VYX8NYuUC5TmtF1vGyI-9zBQ";

    public string APIToken { get; set; } = "oh.Token.e8X5h5IrTU7wqXtNXvGzcaYbzVhIqVC8IO88yH5CToKzGtUfO6jIM0ldj6RrxQHO5TPnSEbkO5CM6uvJB5Ag";
}