using GAS.Common;
using GAS.Enum;
using UnityEngine;

namespace GAS.Config
{
    public static class GASConfigManager
    {
        private static GASConfig _config;
        
        private static GASLang? _lang;

        public static GASConfig Config
        {
            get
            {
                if (_config == null)
                {
                    _config = Resources.Load<GASConfig>("GASConfig");
                    if (_config == null)
                    {
                        throw new GASException("GASConfig not found! Please create a GASConfig asset under Resources/");
                    }
                }
                return _config;
            }
        }

        public static int AppId => Config.appId;
        public static string AppToken => Config.appToken;

        /// <summary>
        /// 接口语言
        /// </summary>
        public static GASLang Lang
        {
            get
            {
                if (_lang == null)
                {
                    // 默认中文
                    _lang = GASLang.zh;
                }

                return _lang.Value;
            }
            set
            {
                _lang = value;
            }
        }
        
        public static string LangString => Lang.ToString();
    }
}