using System;
using Cysharp.Threading.Tasks;
using GAS.Common;
using GAS.Config;
using GAS.Enum;
using GAS.Service;
using GAS.Handler;
using UnityEngine;
using UnityEngine.UI;

namespace GAS.Demo
{
    /// <summary>
    /// GAS DEMO
    /// 使用前需要引入UniTask、Newtonsoft.Json
    /// </summary>
    public class GASDemo : MonoBehaviour
    {
        [Header("UI")]
        public Button btnOAuth;
        public Button btnProfile;
        public Button btnSaveArchive;
        public Button btnReadArchive;
        public Button btnVersion;
        public Button btnRedeem;
        public Button btnLogout;
        
        public Text log;

        [Header("Text")]
        public InputField versionSequence;
        public InputField redeemCode;
        
        [Header("Lang")]
        public Button btnSwitchToZH;
        public Button btnSwitchToEN;

        // Service
        private OAuthService _oauth = new OAuthService();
        private ProfileService _profile = new ProfileService();
        private ArchiveService _archive = new ArchiveService();
        private VersionService _version = new VersionService();
        private RedeemService _redeem = new RedeemService();

        // Runtime tokens
        private string _authToken;
        private string _accessToken;
        private string _email;
        
        private int appId;
        private string appToken;

        void Awake()
        {
            Application.targetFrameRate = 60;
        }

        void Start()
        {
            // 需要在 Resources 里创建一个 GASConfig 配置文件（右键菜单）
            // appId 和 appToken 从此配置文件处读取
            appId = GASConfigManager.AppId;
            appToken = GASConfigManager.AppToken;
            
            btnOAuth.onClick.AddListener(() => RunOAuth());
            btnProfile.onClick.AddListener(() => RunProfile());
            btnSaveArchive.onClick.AddListener(() => RunSaveArchive());
            btnReadArchive.onClick.AddListener(() => RunReadArchive());
            btnVersion.onClick.AddListener(() => RunVersion());
            btnRedeem.onClick.AddListener(() => RunRedeem());
            btnLogout.onClick.AddListener(() => RunLogout());
            
            btnSwitchToZH.onClick.AddListener(() => SetLanguage(GASLang.zh));
            btnSwitchToEN.onClick.AddListener(() => SetLanguage(GASLang.en));
            
            btnLogout.interactable = false;
        }

        private void Log(string msg, int color = 2)
        {
            SnackBar.Instance.Show(msg, color);
            Debug.Log(msg);
            log.text += msg + "\n";
        }
        
        private void SetLanguage(GASLang lang)
        {
            GASConfigManager.Lang = lang;
            Log("切换接口语言：" + lang);
        }

        // --------------------------------------------------------------------
        // 1. OAuth 流程：获取 auth_token → 打开浏览器 → 等待用户授权 → 获取 access_token
        // --------------------------------------------------------------------
        private async void RunOAuth()
        {
            if (appToken == "" || appId == 0)
            {
                Log("请先设置 appId 和 appToken");
                return;
            }
            Log("请求 auth_token ...");
            
            btnOAuth.interactable = false;

            try
            {
                var resp = await _oauth.GetAuthTokenAsync();
                _authToken = resp.Data.AuthToken;
            }
            catch (GASException ex)
            {
                Log(ex.Message, 1);
                btnOAuth.interactable = true;
                return;
            }

            Log("auth_token = " + _authToken);

            // ---------------------------
            // (2) 调起浏览器 type=2 授权
            // ---------------------------
            GASBrowserHandler.OpenAuthBrowser(appId, _authToken);
            
            Log("已打开浏览器进行授权，请授权后回到游戏");
            // 开始轮询
            OnAuthCallbackPolling(_authToken);
        }

        // --------------------------------------------------------------------
        // 轮询 10 次，获取 access_token
        // --------------------------------------------------------------------
        private async void OnAuthCallbackPolling(string authTokenFromBrowser)
        {
            const int maxRetry = 10;
            const int intervalMs = 1000;

            for (int i = 1; i <= maxRetry; i++)
            {
                Log($"OAuth 回调轮询第 {i}/{maxRetry} 次...");

                try
                {
                    var resp = await _oauth.ExchangeAuthTokenAsync(authTokenFromBrowser);

                    if (resp != null && resp.Code == 200)
                    {
                        _accessToken = resp.Data.AccessToken;
                        _email = resp.Data.Email;

                        btnLogout.interactable = true;
                        Log(resp.Msg +
                            "\naccess_token = " + _accessToken +
                            "\nemail = " + _email);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Log("回调检查失败：" + ex.Message);
                }

                await UniTask.Delay(intervalMs);
            }

            Log("轮询结束：未获得授权，请确认是否已在浏览器完成授权。");
            btnOAuth.interactable = true;
        }

        // --------------------------------------------------------------------
        // 2. 获取 Profile
        // --------------------------------------------------------------------
        private async void RunProfile()
        {
            if (_accessToken == null)
            {
                Log("请先完成 OAuth");
                return;
            }

            var resp = await _profile.GetProfileAsync(_email, _accessToken);
            Log("Profile = " + resp.Data.Nickname);
        }

        // --------------------------------------------------------------------
        // 3. 保存存档
        // --------------------------------------------------------------------
        private async void RunSaveArchive()
        {
            if (_accessToken == null)
            {
                Log("请先完成 OAuth");
                return;
            }

            string content = "{\"level\":1, \"percent\":100, \"diamond\":10}";
            var archiveSaveResp = await _archive.SaveAsync(_email, _accessToken, Application.version, content);

            Log(archiveSaveResp.Msg);
        }

        // --------------------------------------------------------------------
        // 4. 读取存档
        // --------------------------------------------------------------------
        private async void RunReadArchive()
        {
            if (_accessToken == null)
            {
                Log("请先完成 OAuth");
                return;
            }
            
            var resp = await _archive.ReadAsync(_email, _accessToken);
            string decrypted = _archive.DecryptArchiveContent(resp.Data.ContentEncrypted);
            
            Log("存档内容 = " + decrypted);
        }

        // --------------------------------------------------------------------
        // 5. Version 版本查询
        // --------------------------------------------------------------------
        private async void RunVersion()
        {
            if (_accessToken == null)
            {
                Log("请先完成 OAuth");
                return;
            }

            var resp = await _version.GetVersionAsync(versionSequence.text);

            Log("最新版本：" + string.Join(",", resp.Data.Versions));
        }

        // --------------------------------------------------------------------
        // 6. Redeem 兑换码
        // --------------------------------------------------------------------
        private async void RunRedeem()
        {
            if (_accessToken == null)
            {
                Log("请先完成 OAuth");
                return;
            }

            var resp = await _redeem.RedeemWithAccountAsync(_email, _accessToken, redeemCode.text);

            string decrypted = _redeem.DecryptRedeemContent(resp.Data.ContentEncrypted);
            Log("兑换结果：" + decrypted);
        }
        
        // --------------------------------------------------------------------
        // 7. Logout 退出登录
        // --------------------------------------------------------------------
        private async void RunLogout()
        {
            if (_accessToken == null)
            {
                Log("请先完成 OAuth");
                return;
            }
            
            var resp = await _oauth.LogoutAsync(_email, _accessToken);
            
            btnLogout.interactable = false;
            btnOAuth.interactable = true;
            Log("已注销登录状态：" + resp.Code);
        }
    }
}