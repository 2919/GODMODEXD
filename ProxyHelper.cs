// Decompiled with JetBrains decompiler
// Type: ReAuth.ProxyHelper
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using System.Linq;

namespace ReAuth
{
  public static class ProxyHelper
  {
    private static int CurrentPosition = 1;
    private static int Amount = 0;

    public static ProxyData GetProxy(bool type)
    {
      if (!StaticHelper.ProxyList.Any<ProxyData>())
        return (ProxyData) null;
      ProxyData proxyData = new ProxyData();
      proxyData.Host = StaticHelper.ProxyList[ProxyHelper.CurrentPosition - 1].Host;
      proxyData.Port = StaticHelper.ProxyList[ProxyHelper.CurrentPosition - 1].Port;
      if (ProxyHelper.CurrentPosition == StaticHelper.ProxyList.Count)
        ProxyHelper.CurrentPosition = 1;
      ++ProxyHelper.CurrentPosition;
      return proxyData;
    }
  }
}
