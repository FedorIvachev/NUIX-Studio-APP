using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SceneDataScriptableObject", order = 1)]
public class SceneData : ScriptableObject
{

    public string home_Scene_Location = "";
    public string classroom_Scene_Location = "";
    public string car_Scene_Location = "";
    public string main_scene_location = "";



    public string ReturnScenePath(string environment)
    {
        string scenePath;
        switch (environment)
        {
            case "Home":
                //SceneManager.LoadScene(home_Scene_Location, LoadSceneMode.Single);
                scenePath = home_Scene_Location;
                break;
            case "Classroom":
                //SceneManager.LoadScene(classroom_Scene_Location, LoadSceneMode.Single);
                scenePath = classroom_Scene_Location;
                break;
            case "Car":
                //SceneManager.LoadScene(car_Scene_Location, LoadSceneMode.Single);
                scenePath = car_Scene_Location;
                break;
            case "Main":
                scenePath = main_scene_location;
                break;
            default:
                scenePath = null;
                break;
        }
        return scenePath;
    }
}
