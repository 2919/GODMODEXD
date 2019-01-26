// Decompiled with JetBrains decompiler
// Type: ReAuth.Data
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using Newtonsoft.Json;

namespace ReAuth
{
  public class Data
  {
    [JsonProperty("puuid")]
    public string Puuid { get; set; }

    [JsonProperty("accountId")]
    public long AccountId { get; set; }

    [JsonProperty("expires")]
    public string Expires { get; set; }

    [JsonProperty("items")]
    public Items Items { get; set; }
  }
}
