// Decompiled with JetBrains decompiler
// Type: ReAuth.StringExtensions
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using System;

namespace ReAuth
{
  public static class StringExtensions
  {
    public static bool Contains(this string source, string toCheck, bool bCaseInsensitive)
    {
      return source.IndexOf(toCheck, bCaseInsensitive ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture) >= 0;
    }
  }
}
