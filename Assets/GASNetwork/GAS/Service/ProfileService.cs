using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GAS.Common;
using GAS.Config;
using GAS.Enum;
using GAS.Models.Profile;
using GAS.Network;

namespace GAS.Service
{
    /// <summary>
    /// 用户信息服务
    /// 获取指定应用下用户的详细信息，包括用户基础信息、地理位置和用户组等
    /// </summary>
    public class ProfileService
    {
        private readonly GASHttpClient _http = new GASHttpClient();

        /// <summary>
        /// 获取用户信息（新版OAuth方式）
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="accessToken">新版本访问令牌</param>
        /// <returns>ProfileResp</returns>
        public async UniTask<ProfileResp> GetProfileAsync(string email, string accessToken)
        {
            var sendReq = new ProfileReq
            {
                AppId = GASConfigManager.AppId,
                Email = email,
                AccessToken = accessToken
            };
            
            var resp = await _http.PostAsync<ProfileResp>(GASApiRoute.Endpoints.Profile, sendReq);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }
        
        /// <summary>
        /// 获取用户信息（旧版方式，已弃用）
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="userToken">老版本用户令牌</param>
        /// <returns>ProfileResp</returns>
        [Obsolete("This method is obsolete. Use GetProfileAsync instead.")]
        public async UniTask<ProfileResp> GetProfileAsyncOld(string email, string userToken)
        {
            var sendReq = new ProfileReq
            {
                AppId = GASConfigManager.AppId,
                Email = email,
                UserToken = userToken
            };
            
            var resp = await _http.PostAsync<ProfileResp>(GASApiRoute.Endpoints.Profile, sendReq);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }

        /// <summary>
        /// 检查用户是否在白名单中（内部测试人员）
        /// </summary>
        /// <param name="userGroups">用户组别字符串（逗号分隔）</param>
        /// <returns>true 表示在白名单中</returns>
        public bool IsWhitelistUser(string userGroups)
        {
            return ParseUserGroups(userGroups).Contains(0);
        }

        /// <summary>
        /// 检查用户是否被加入黑名单
        /// </summary>
        /// <param name="userGroups">用户组别字符串（逗号分隔）</param>
        /// <returns>true 表示被加入黑名单</returns>
        public bool IsBlacklistUser(string userGroups)
        {
            return ParseUserGroups(userGroups).Contains(1);
        }

        /// <summary>
        /// 检查用户是否属于指定的自定义用户组
        /// </summary>
        /// <param name="userGroups">用户组别字符串（逗号分隔）</param>
        /// <param name="groupId">自定义组别ID</param>
        /// <returns>true 表示属于该组别</returns>
        public bool IsInUserGroup(string userGroups, int groupId)
        {
            if (groupId < 0)
                throw new ArgumentException("User group ID must be >= 0");

            return ParseUserGroups(userGroups).Contains(groupId);
        }

        /// <summary>
        /// 获取用户的所有用户组ID（解析user_group字符串）
        /// </summary>
        /// <param name="userGroups">用户组别字符串（逗号分隔），例："2,4"</param>
        /// <returns>用户组ID列表</returns>
        public List<int> ParseUserGroups(string userGroups)
        {
            var groups = new List<int>();

            if (string.IsNullOrEmpty(userGroups))
                return groups;

            try
            {
                var parts = userGroups.Split(',');
                foreach (var part in parts)
                {
                    if (int.TryParse(part.Trim(), out var groupId))
                    {
                        groups.Add(groupId);
                    }
                }
            }
            catch
            {
                // 解析失败，返回空列表
            }

            return groups;
        }

        /// <summary>
        /// 获取用户组别的描述文本
        /// </summary>
        /// <param name="groupId">用户组ID</param>
        /// <returns>组别描述</returns>
        public string GetUserGroupDescription(int groupId)
        {
            if (GASConfigManager.Lang == GASLang.en)
            {
                return groupId switch
                {
                    0 => "Whitelisted User (Internal Tester)",
                    1 => "Blacklisted User",
                    _ => $"Custom User Group {groupId}"
                };
            }

            return groupId switch
            {
                0 => "白名单用户（内部测试人员）",
                1 => "黑名单用户",
                _ => $"自定义用户组 {groupId}"
            };
        }

        /// <summary>
        /// 获取用户所有组别的描述（逗号分隔）
        /// </summary>
        /// <param name="userGroups">用户组别字符串（逗号分隔）</param>
        /// <returns>组别描述列表</returns>
        public List<string> GetUserGroupDescriptions(string userGroups)
        {
            return ParseUserGroups(userGroups)
                .Select(GetUserGroupDescription)
                .ToList();
        }

        /// <summary>
        /// 获取用户所有组别的描述（以换行符分隔的字符串）
        /// </summary>
        /// <param name="userGroups">用户组别字符串（逗号分隔）</param>
        /// <returns>组别描述（多行字符串）</returns>
        public string GetUserGroupDescriptionsString(string userGroups)
        {
            var descriptions = GetUserGroupDescriptions(userGroups);
            if (descriptions.Count > 0)
            {
                return string.Join("\n", descriptions);
            }

            return GASConfigManager.Lang == GASLang.en ? "No Special Group" : "无特殊分组";
        }
    }
}
