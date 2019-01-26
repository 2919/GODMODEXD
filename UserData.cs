// Decompiled with JetBrains decompiler
// Type: ReAuth.UserData
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using Newtonsoft.Json;

namespace ReAuth
{
  public class UserData
  {
    [JsonProperty("sub")]
    public string Sub { get; set; }

    [JsonProperty("scp")]
    public string[] Scp { get; set; }

    [JsonProperty("clm")]
    public string[] Clm { get; set; }

    [JsonProperty("dat")]
    public Dat Dat { get; set; }

    [JsonProperty("iss")]
    public string Iss { get; set; }

    [JsonProperty("exp")]
    public long Exp { get; set; }

    [JsonProperty("iat")]
    public long Iat { get; set; }

    [JsonProperty("jti")]
    public string Jti { get; set; }

    [JsonProperty("cid")]
    public string Cid { get; set; }
  }
}
