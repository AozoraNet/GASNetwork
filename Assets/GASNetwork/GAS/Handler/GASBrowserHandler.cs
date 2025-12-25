using UnityEngine;

namespace GAS.Handler
{
    public static class GASBrowserHandler
    {
        private const string oauthUrl = "https://gas.chinadlrs.com/oauth?appid={0}&token={1}";
        
        /// <summary>
        /// 打开浏览器进行授权登录
        /// </summary>
        /// <param name="appId">appId</param>
        /// <param name="authToken">authToken</param>
        public static void OpenAuthBrowser(int appId, string authToken)
        {
            string url = string.Format(oauthUrl, appId, authToken);
            OpenURL(url);
        }
        
        /// <summary>
        /// 跳转到账号系统服务条款
        /// </summary>
        public static void ToAccRules()
        {
            OpenURL("https://chinadlrs.com/policy/?page=account");
        }

        /// <summary>
        /// 跳转到注册界面
        /// </summary>
        public static void ToRegister()
        {
            OpenURL("https://chinadlrs.com/register/");
        }
        
        private static void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
    }
}