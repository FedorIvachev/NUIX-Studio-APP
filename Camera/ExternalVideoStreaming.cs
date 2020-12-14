using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Very basic class I created for the needs of teams who need to get the camera or video value
    /// If you want to stream live video better not use this class but use Video Player Component instead
    /// Similar to ExternalVideoPerFrameStreaming, although I found out that 
    /// Unity cleans garbage (causing memory leak) without Invokerepeating more often
    /// Suggest you to use video resolution 320 x 240
    /// </summary>
    public class ExternalVideoStreaming : MonoBehaviour
    {
        public string url;

        public float GetRate = 0.15f;
        private float nextGet = 0;


        IEnumerator GetTexture()
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                yield return www.SendWebRequest();
                MeshRenderer renderer = GetComponent<MeshRenderer>();
                renderer.material.mainTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                www.Dispose(); // extra garbage cleaning because the heap size grows very fast at some moment
            }
        }

        void Update()
        {
            if (Time.time > nextGet)
            {
                StartCoroutine(GetTexture());
                nextGet = Time.time + GetRate;
            }
        }





        /*
         * Legacy code, where i tried to receive the video data, but the code does not run well
        public GameObject _textureApplyPlane;
        private Texture2D IpCameraTexture;
        UnityWebRequest VideoRequest;
        //private string sourceURL = "http://192.168.100.107/axis-cgi/mjpg/video.cgi";
        private string sourceURL = "http://192.168.3.2:8080/video";

        void Start()
        {
            IpCameraTexture = new Texture2D(1, 1, TextureFormat.RGB24, false);
            //_textureApplyPlane.GetComponent<MeshRenderer>().material.mainTexture = IpCameraTexture;
            StartCoroutine(GetFrame());
        }

        public IEnumerator GetFrame()
        {
            //string authorization = "Basic " + System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("admin:admin"));
            VideoRequest = new UnityWebRequest(sourceURL, UnityWebRequest.kHttpVerbGET);
            //VideoRequest.SetRequestHeader("AUTHORIZATION", authorization);
            VideoRequest.downloadHandler = new DownloadHandlerBuffer();
            //VideoRequest.Send();
            VideoRequest.SendWebRequest();
            while (true)
            {
                yield return null;
                if (!string.IsNullOrEmpty(VideoRequest.error))
                    throw new UnityException(VideoRequest.error);
                if (VideoRequest.downloadHandler.data != null)
                {
                    print(VideoRequest.downloadHandler.data.Length);
                    IpCameraTexture.LoadRawTextureData(VideoRequest.downloadHandler.data);
                    IpCameraTexture.Apply();
                }
            }

        }*/
    }
}