using System;
using System.Text;
using Cysharp.Threading.Tasks;
using GAS.Common;
using GAS.Config;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace GAS.Network
{
    public class GASHttpClient
    {
        /// <summary>
        /// POST
        /// </summary>
        public async UniTask<T> PostAsync<T>(string rawUrl, object body, int type = -1)
        {
            string url;
            if (type == -1)
            {
                url = $"{rawUrl}?lang={GASConfigManager.LangString}";
            }
            else
            {
                url = $"{rawUrl}?type={type}&lang={GASConfigManager.LangString}";
            }
            
            string json = JsonConvert.SerializeObject(body);

            GASResponseLogger.LogRequest("POST", url, json);

            using (var req = new UnityWebRequest(url, "POST"))
            {
                var bodyRaw = Encoding.UTF8.GetBytes(json);
                req.uploadHandler = new UploadHandlerRaw(bodyRaw);
                req.downloadHandler = new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");

                float startTime = Time.realtimeSinceStartup;
                var op = req.SendWebRequest();

                while (!op.isDone) await UniTask.Yield();
                float duration = (Time.realtimeSinceStartup - startTime) * 1000f;

                return await HandleResponse<T>(req, duration);
            }
        }

        /// <summary>
        /// GET
        /// </summary>
        public async UniTask<T> GetAsync<T>(string url)
        {
            GASResponseLogger.LogRequest("GET", url, null);

            using (var req = UnityWebRequest.Get(url))
            {
                req.downloadHandler = new DownloadHandlerBuffer();

                float startTime = Time.realtimeSinceStartup;
                var op = req.SendWebRequest();

                while (!op.isDone) await UniTask.Yield();
                float duration = (Time.realtimeSinceStartup - startTime) * 1000f;

                return await HandleResponse<T>(req, duration);
            }
        }

        /// <summary>
        /// Response Handler
        /// </summary>
        private async UniTask<T> HandleResponse<T>(UnityWebRequest req, float ms)
        {
            string respText = req.downloadHandler?.text;
            long code = req.responseCode;

            if (req.result == UnityWebRequest.Result.ConnectionError ||
                req.result == UnityWebRequest.Result.ProtocolError)
            {
                GASResponseLogger.LogError(req.method, req.url, code, respText, req.error, ms);
                throw new GASNetworkException((int)code, req.error, respText);
            }

            GASResponseLogger.LogResponse(req.method, req.url, code, respText, ms);

            try
            {
                return JsonConvert.DeserializeObject<T>(respText);
            }
            catch (Exception ex)
            {
                Debug.LogError($"{GASResponseLogger.TAG} <color=#FF4040><b>JSON Parse Error</b></color>\n{ex}\nRaw: {respText}");
                throw new GASParseException("Failed parse JSON: " + ex.Message + "\nRaw:" + respText);
            }
        }
    }
}
