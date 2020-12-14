using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Stream a per-frame video inside a texture
    /// There is a bug I still try to fix - memory leak
    /// </summary>
    public class ExternalVideoPerFrameStreaming : MonoBehaviour
    {
        public GameObject _textureApplyPlane;
        Texture myTexture;

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

                myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                _textureApplyPlane.GetComponent<MeshRenderer>().material.mainTexture = myTexture;

            }
            www.Dispose();
        }
    }
}