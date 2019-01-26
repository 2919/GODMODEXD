// Decompiled with JetBrains decompiler
// Type: ReAuth.RiotAuthToken
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using Newtonsoft.Json;
using System.Net;

namespace ReAuth
{
  public class RiotAuthToken
  {
    public RiotAuthToken(
      RiotAuthResult result,
      string accessTokenJson,
      string dsid,
      RegionData regionData)
    {
      this.Result = result;
      this.Dsid = dsid;
      this.RegionData = regionData;
      if (result != RiotAuthResult.Success)
        return;
      this.AccessTokenJson = (AccessTokenJson) JsonConvert.DeserializeObject<AccessTokenJson>(accessTokenJson);
    }

    public RiotAuthToken(
      RiotAuthResult result,
      string accessTokenJson,
      string dsid,
      RegionData regionData,
      WebProxy proxy)
    {
      this.Result = result;
      this.Dsid = dsid;
      this.RegionData = regionData;
      this.Proxy = proxy;
      if (result != RiotAuthResult.Success)
        return;
      this.AccessTokenJson = (AccessTokenJson) JsonConvert.DeserializeObject<AccessTokenJson>(accessTokenJson);
    }

    public WebProxy Proxy { get; }

    public RiotAuthResult Result { get; }

    public string Dsid { get; }

    public AccessTokenJson AccessTokenJson { get; }

    public RegionData RegionData { get; }
  }
}
