# GAS Network

[中文](README_ZH.md) | [English](README.md)

GASNetwork is a Unity package for interacting with the DLRS GAS (Game Account System) API. It provides user authentication, cloud saves, version management, and related features.

## Contents

- [Requirements](#requirements)
- [Quick Start](#quick-start)
- [Configuration](#configuration)
- [Error Handling](#error-handling)
- [Acknowledgements](#acknowledgements)

## Requirements

### Required Dependencies

- **UniTask**: For asynchronous operations ([GitHub](https://github.com/Cysharp/UniTask))
- **Newtonsoft.Json**: The Unity-specific JSON serialization package ([GitHub](https://github.com/applejag/Newtonsoft.Json-for-Unity) | [Unity](http://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@3.0/manual/index.html))

### Install UniTask

If UniTask is not already installed in the project, install it using one of the following methods:

1. Unity Package Manager
2. Download it from [UniTask GitHub](https://github.com/Cysharp/UniTask)

### Install Newtonsoft.Json

The Unity-specific version is required for `IL2CPP` support.

1. Open Unity Package Manager.
2. Select **Add package by name...**, enter `com.unity.nuget.newtonsoft-json`, and use version `3.2.2`.

## Quick Start

### 1. Create a configuration asset

`GASConfig` is already included under `Resources`, so you do not need to create it again. To create a new one in the Unity Editor:

1. Right-click `Assets/GASNetwork/Resources/`.
2. Select `Create > GAS > Config`.
3. Create a `GASConfig` asset.
4. Set the following values in the Inspector:
   - **App Id**: Your application ID.
   - **App Token**: Your application token.

### 2. Set the API language (optional)

```csharp
using GAS.Config;
using GAS.Enum;

// Use Chinese.
GASConfigManager.Lang = GASLang.zh;

// Use English.
GASConfigManager.Lang = GASLang.en;
```

### 3. Basic usage example

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
        // Read the app ID and app token.
        int appId = GASConfigManager.AppId;
        string appToken = GASConfigManager.AppToken;
        
        // Start the OAuth login flow...
    }
}
```

## Configuration

### GASConfig

The configuration asset is located at `Resources/GASConfig.asset` and contains the following fields:

- **App Id** (`int`): The application ID obtained from the GAS platform.
- **App Token** (`string`): The application token obtained from the GAS platform.

### Language

The API supports two languages:

- `GASLang.zh` - Chinese (default)
- `GASLang.en` - English

You can switch language at runtime with `GASConfigManager.Lang`:

```csharp
GASConfigManager.Lang = GASLang.en; // Switch to English.
```

## Error Handling

### Exception Types

The package defines three exception types:

1. **GASException** - General exception.
   ```csharp
   catch (GASException ex)
   {
       Debug.LogError($"Error code: {ex.Code}, message: {ex.Message}");
   }
   ```

2. **GASNetworkException** - Network exception.
   ```csharp
   catch (GASNetworkException ex)
   {
       Debug.LogError($"Network error: {ex.Code}, {ex.Message}");
       Debug.LogError($"Raw response: {ex.RawText}");
   }
   ```

3. **GASParseException** - JSON parsing exception.
   ```csharp
   catch (GASParseException ex)
   {
       Debug.LogError($"Parse error: {ex.Message}");
   }
   ```

### Response Validation

All service methods automatically validate the response status code. If `code != 200`, a `GASException` is thrown.

### Logging

The package automatically logs all HTTP requests and responses, including:

- Request method, URL, and body.
- Response status code, body, and duration.

Logs are written to the Unity Console through `GASResponseLogger`.

## Acknowledgements

- 幻影の刄
- DL RS 同人群官方网站
- All developers in the DL RS fan community

## License

This package is licensed under the MIT License.
