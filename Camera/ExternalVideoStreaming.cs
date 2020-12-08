using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Very basic class I created for the needs of teams who need to get the camera or video value
    /// If you want to stream live video better not use this class but use Video Player Component instead
    /// </summary>
    public class ExternalVideoStreaming : MonoBehaviour
    {
        public GameObject _textureApplyPlane;


        // Use this for initialization
        void Start()
        {
            InvokeRepeating("SetTexture", 2.0f, 0.1f);
        }

        void SetTexture()
        {
            StartCoroutine(GetTexture());
        }
        IEnumerator GetTexture()
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture("http://192.168.3.2:8080/shot.jpg");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                _textureApplyPlane.GetComponent<MeshRenderer>().material.mainTexture = myTexture;

            }
        }
    }
}