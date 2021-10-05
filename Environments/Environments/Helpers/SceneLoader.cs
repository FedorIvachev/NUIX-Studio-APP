using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public SceneData sceneData;

    public void LoadEnvironmentScene(string environment)
    {
        string scenePath = sceneData.ReturnScenePath(environment);
        if (scenePath != null)
        {
            SceneManager.LoadScene(scenePath, LoadSceneMode.Single);
        }
    }
}
