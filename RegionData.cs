// Decompiled with JetBrains decompiler
// Type: ReAuth.RegionData
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using System.Collections.Generic;

namespace ReAuth
{
  public class RegionData
  {
    public List<Locales> AvailableLocales { get; set; }

    public Locales DefaultLocale { get; set; }

    public RSO Rso { get; set; }

    public RsoPlatformId PlatformId { get; set; }

    public Servers Servers { get; set; }

    public string WebRegion { get; set; }

    public bool SuccessRead { get; set; }

    public string StoreUrlV { get; set; }
  }
}
