// Decompiled with JetBrains decompiler
// Type: ReAuth.Settings
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using Newtonsoft.Json;
using System.IO;

namespace ReAuth
{
  internal class Settings
  {
    public static Settings Config = new Settings()
    {
      ShowPasswords = true,
      SelectedRegion = Region.NA,
      Rso = AuthClass.GetOpenIdConfig("https://auth.riotgames.com/.well-known/openid-configuration")
    };
    public RiotAuthOpenIdConfiguration Rso;

    static Settings()
    {
      Settings.Save();
      if (File.Exists("settings.json"))
        StaticHelper.SettingsSave = (SettingsSave) JsonConvert.DeserializeObject<SettingsSave>(File.ReadAllText("settings.json"));
      else
        StaticHelper.SettingsSave = new SettingsSave()
        {
          InvService = false,
          Proxy = false,
          ProxyChange = 2,
          ProxyList = string.Empty,
          Store = false
        };
      LoadClientVersion.GetLatestRad();
    }

    public bool ShowPasswords { get; set; }

    public Region SelectedRegion { get; set; }

    public string ClientVersion { get; set; }

    public string DataDragonVersion { get; set; }

    public string DefaultCustomExportFilename { get; set; }

    public int Workers { get; set; } = 3;

    public double MinSecondsDelay { get; set; }

    public double MaxSecondsDelay { get; set; } = 1.5;

    public static void Save()
    {
    }
  }
}
