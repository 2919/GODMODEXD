// Decompiled with JetBrains decompiler
// Type: ReAuth.Dat
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using Newtonsoft.Json;

namespace ReAuth
{
  public class Dat
  {
    [JsonProperty("r")]
    public string R { get; set; }

    [JsonProperty("c")]
    public string C { get; set; }

    [JsonProperty("u")]
    public long U { get; set; }
  }
}
