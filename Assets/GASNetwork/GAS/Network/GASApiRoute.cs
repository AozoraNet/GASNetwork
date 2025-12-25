namespace GAS.Network
{
    public class GASApiRoute
    {
        /// <summary>
        /// GAS API Base URL.
        /// GAS 接口地址
        /// </summary>
        private static readonly string BaseUrl = "https://api.chinadlrs.com/developer";

        public static class Endpoints
        {
            /// <summary>
            /// Oauth authorized login.
            /// 授权登录
            /// </summary>
            public static readonly string Oauth = BaseUrl + "/oauth.php";
            
            /// <summary>
            /// Auto Login.
            /// 自动登录
            /// </summary>
            public static readonly string AutoLogin = BaseUrl + "/auto-login.php";
            
            /// <summary>
            /// User profile.
            /// 用户信息
            /// </summary>
            public static readonly string Profile = BaseUrl + "/profile.php";
            
            /// <summary>
            /// Cloud Archive.
            /// 云存档
            /// </summary>
            public static readonly string Archive = BaseUrl + "/archive.php";
            
            /// <summary>
            /// Version.
            /// 版本管理
            /// </summary>
            public static readonly string Version = BaseUrl + "/version.php";
            
            /// <summary>
            /// Redeem Code.
            /// 兑换码
            /// </summary>
            public static readonly string Redeem = BaseUrl + "/redeem.php";
            
            /// <summary>
            /// Config.
            /// 配置获取
            /// </summary>
            public static readonly string Config = BaseUrl + "/config.php";
        }
    }
}