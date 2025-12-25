using UnityEngine;

namespace GAS.Common
{
    public static class GASResponseLogger
    {
        public const string TAG = "<color=#00C0FF><b>[GAS HTTP]</b></color>";

        public static void LogRequest(string method, string url, string body)
        {
            Debug.Log(
$@"{TAG} <color=#A0FF80><b>REQUEST BEGIN ▶</b></color>
<color=#00FFCC>Method:</color> {method}
<color=#00FFCC>URL:</color> {url}
<color=#00FFCC>Body:</color> {body}
──────────────────────────────────────────"
            );
        }

        public static void LogResponse(string method, string url, long status, string response, float ms)
        {
            Debug.Log(
$@"{TAG} <color=#80FF80><b>RESPONSE ✔</b></color>
<color=#00FFCC>Method:</color> {method}
<color=#00FFCC>URL:</color> {url}
<color=#00FFCC>Status:</color> {status}
<color=#00FFCC>Time:</color> {ms:F1} ms
<color=#00FFCC>Response:</color>
{response}
<color=#A0FF80><b>──────────────────────────────────────────</b></color>"
            );
        }

        public static void LogError(string method, string url, long status, string response, string error, float ms)
        {
            Debug.LogError(
$@"{TAG} <color=#FF6060><b>RESPONSE ERROR ✖</b></color>
<color=#FF8080>Method:</color> {method}
<color=#FF8080>URL:</color> {url}
<color=#FF8080>Status:</color> {status}
<color=#FF8080>Time:</color> {ms:F1} ms
<color=#FF8080>Error:</color> {error}
<color=#FF8080>Response:</color>
{response}
<color=#FF6060><b>──────────────────────────────────────────</b></color>"
            );
        }
    }
}