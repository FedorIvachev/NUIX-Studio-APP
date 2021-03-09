using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Stream a per-frame video inside a texture
    /// https://forum.unity.com/threads/excessive-memory-consumption-when-loading-images-and-unable-to-free-the-memory.801306/
    /// memory leak. Probably will need to write my own encoder
    /// </summary>
    public class ExternalVideoPerFrameStreaming : MonoBehaviour
    {
        Texture myTexture;
        MeshRenderer _renderer;

        // Use this for initialization
        void Start()
        {
            InvokeRepeating("SetTexture", 2.0f, 0.15f);
            _renderer = GetComponent<MeshRenderer>();
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
                _renderer.material.mainTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;

            }
        }
    }
}