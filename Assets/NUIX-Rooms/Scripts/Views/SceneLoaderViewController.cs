using Oculus.Interaction.Samples;


public class SceneLoaderViewController : ItemViewController
{
    
    public void Start()
    {
        receiverMethods.Add(nameof(Load));
    }

    public void Load(string sceneName)
    {
        GetComponent<SceneLoader>().Load(sceneName);
    }
}
