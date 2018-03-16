using System.Collections;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.Networking;

namespace goedle_sdk
{

    public interface IGoedleHttpClient
    {
        JSONNode getStrategy(IUnityWebRequests www, string url);
        void sendPost(IUnityWebRequests www, string url, string content, string authentification);
        void sendGet(IUnityWebRequests www, string url);
        IEnumerator getRequest(IUnityWebRequests www, string url);
        IEnumerator getJSONRequest(IUnityWebRequests www, string url);
        IEnumerator postJSONRequest(UnityWebRequest www, string url, string content, string authentification);
    }

    public interface IUnityWebRequests
    {
        UnityWebRequest SendWebRequest();
        UnityWebRequest Post(string url, string content);
        UnityWebRequest Get(string url, string content);
        int responseCode { get; set; }
        bool isNetworkError { get; set; }
        bool isHttpError { get; set; }
        string url{ get; set; }

    }

    public class GoedleHttpClient: MonoBehaviour, IGoedleHttpClient 
	{

        public GoedleHttpClient(){}

        public void sendGet(IUnityWebRequests www, string url)
        {
            StartCoroutine(getJSONRequest(www, url));
        }

        public JSONNode getStrategy(IUnityWebRequests www, string url)
        {
            StartCoroutine(getJSONRequest(www, url));
            // TODO RETURN JSON from REQUEST
            return null;
        }

        public void sendPost(IUnityWebRequests www, string url, string content, string authentification)
        {
            UnityWebRequest client = www as UnityWebRequest;
            client = new UnityWebRequest(url, "POST");
            Console.WriteLine(client.url);
            Console.WriteLine(client.method);

            StartCoroutine(postJSONRequest(client, url, content, authentification));
        }

        public IEnumerator getRequest(IUnityWebRequests www, string url)
        {
            UnityWebRequest client = (UnityWebRequest)www;

            using (client = new UnityWebRequest(url, "GET"))
            {
                yield return client.SendWebRequest();
                if (client.isNetworkError || client.isHttpError)
                {
                    Debug.Log(client.error);
                }
                else
                {
                    // Show results as text
                    Debug.Log(client.downloadHandler.text);
                    // Or retrieve results as binary data
                    byte[] results = client.downloadHandler.data;
                }
            }
        }

        public IEnumerator getJSONRequest(IUnityWebRequests www, string url)
        {
            UnityWebRequest client = (UnityWebRequest)www;
            using (client = new UnityWebRequest(url, "GET"))
            {
                yield return www.SendWebRequest();
                if (client.isNetworkError || client.isHttpError)
                {
                    Debug.Log(client.error);
                }
                else
                {
                    // Show results as text
                    Debug.Log(client.downloadHandler.text);
                    // Or retrieve results as binary data
                    byte[] results = client.downloadHandler.data;
                }
            }

        }

        public IEnumerator postJSONRequest(UnityWebRequest client, string url, string content ,string authentification)
	    {
            /*
            Console.WriteLine(content);
            Console.WriteLine(authentification);
            Console.WriteLine(url);
            */
			//byte[] bytes = Encoding.UTF8.GetBytes(pass[0]);
            using (client)
            {
                byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(content);
                client.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
                client.SetRequestHeader("Content-Type", "application/json");
                if (!string.IsNullOrEmpty(authentification))
                    client.SetRequestHeader("Authorization", authentification);
                client.chunkedTransfer = false;
                yield return client.SendWebRequest();
                Console.WriteLine(client.responseCode);
                Console.WriteLine(client.isNetworkError);
                Console.WriteLine(client.isHttpError);
                if (client.isNetworkError || client.isHttpError)
                {
                    Debug.Log(client.error);
                }
                else
                { 
                   Debug.Log(content);
                }
            }
	    }
	}
}


