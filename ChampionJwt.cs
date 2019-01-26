// Decompiled with JetBrains decompiler
// Type: ReAuth.ChampionJwt
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using Newtonsoft.Json;

namespace ReAuth
{
  public class ChampionJwt
  {
    [JsonProperty("sub")]
    public string Sub { get; set; }

    [JsonProperty("exp")]
    public long Exp { get; set; }

    [JsonProperty("items")]
    public ItemsChamp Items { get; set; }

    [JsonProperty("iat")]
    public long Iat { get; set; }
  }
}
