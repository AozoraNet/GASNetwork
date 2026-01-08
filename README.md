# GAS Network

GASNetwork 用于与 DLRS GAS (Game Account System) API 进行交互，提供用户认证、云存档、版本管理等功能。

## 目录

- [环境要求](#环境要求)
- [快速开始](#快速开始)
- [配置说明](#配置说明)
- [错误处理](#错误处理)
- [鸣谢](#鸣谢)

## 环境要求

### 必需依赖

- **UniTask**: 用于异步操作（[GitHub](https://github.com/Cysharp/UniTask)）
- **Newtonsoft.Json**: JSON 序列化库 Unity 专用版（[Github](https://github.com/applejag/Newtonsoft.Json-for-Unity) | [Unity](http://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@3.0/manual/index.html)）

### 安装 UniTask

如果项目中尚未安装 UniTask，可以通过以下方式安装：

1. 使用 Unity Package Manager
2. 或从 [UniTask GitHub](https://github.com/Cysharp/UniTask) 下载

### 安装 Newtonsoft.Json

⚠️ 需要安装 Unity 专用版以支持 `IL2CPP` 运行

步骤：  
1. 使用 Unity Package Manager
2. Add package by name... -> 输入包名`com.unity.nuget.newtonsoft-json`，版本`3.2.2`


## 快速开始

### 1. 创建配置文件（已包含在Resources，无需重复创建）

在 Unity 编辑器中：

1. 右键点击 `Assets/GASNetwork/Resources/` 目录
2. 选择 `Create > GAS > Config`
3. 创建 `GASConfig` 资源文件
4. 在 Inspector 中设置：
   - **App Id**: 你的应用 ID
   - **App Token**: 你的应用令牌

### 2. 设置接口语言（可选）

```csharp
using GAS.Config;
using GAS.Enum;

// 设置为中文
GASConfigManager.Lang = GASLang.zh;

// 设置为英文
GASConfigManager.Lang = GASLang.en;
```

### 3. 基本使用示例

```csharp
using Cysharp.Threading.Tasks;
using GAS.Service;
using GAS.Config;

public class MyGameManager : MonoBehaviour
{
    private OAuthService _oauth = new OAuthService();
    private ProfileService _profile = new ProfileService();
    
    async void Start()
    {
        // 获取 appId 和 appToken
        int appId = GASConfigManager.AppId;
        string appToken = GASConfigManager.AppToken;
        
        // 执行 OAuth 登录流程...
    }
}
```

## 配置说明

### GASConfig 配置

配置文件位于 `Resources/GASConfig.asset`，包含以下字段：

- **App Id** (`int`): 应用 ID，从 GAS 平台获取
- **App Token** (`string`): 应用令牌，从 GAS 平台获取

### 语言配置

Api 接口支持两种语言：

- `GASLang.zh` - 中文（默认）
- `GASLang.en` - 英文

可以通过 `GASConfigManager.Lang` 动态切换：

```csharp
GASConfigManager.Lang = GASLang.en; // 切换到英文
```

## 错误处理

### 异常类型

系统定义了三种异常类型：

1. **GASException** - 通用异常
   ```csharp
   catch (GASException ex)
   {
       Debug.LogError($"错误代码: {ex.Code}, 消息: {ex.Message}");
   }
   ```

2. **GASNetworkException** - 网络异常
   ```csharp
   catch (GASNetworkException ex)
   {
       Debug.LogError($"网络错误: {ex.Code}, {ex.Message}");
       Debug.LogError($"原始响应: {ex.RawText}");
   }
   ```

3. **GASParseException** - JSON 解析异常
   ```csharp
   catch (GASParseException ex)
   {
       Debug.LogError($"解析错误: {ex.Message}");
   }
   ```

### 响应检查

所有服务方法都会自动检查响应状态码，如果 `code != 200`，会抛出 `GASException`。

### 日志记录

系统会自动记录所有 HTTP 请求和响应，包括：
- 请求方法、URL、请求体
- 响应状态码、响应体、耗时

日志通过 `GASResponseLogger` 输出到 Unity Console。

## 鸣谢

幻影の刄  
DL RS 同人群官方网站  
DL RS 同人社区全体开发者

## 开源协议

This package is licensed under The MIT License (MIT)

---

此文档使用 Cursor 辅助生成  

Author: BluesDawn  
Date: 2025-11-29  
Edit: 2026-01-08