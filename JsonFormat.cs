// Decompiled with JetBrains decompiler
// Type: ReAuth.JsonFormat
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using Newtonsoft.Json;
using ReAuth.Classes;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ReAuth
{
  internal class JsonFormat
  {
    public string Version { get; set; }

    public List<Account> Accounts { get; set; }

    public JsonFormat(List<Account> accounts)
    {
      this.Accounts = accounts;
      this.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }

    public static JsonFormat Import(string file)
    {
      using (StreamReader streamReader = new StreamReader(file))
        return (JsonFormat) JsonConvert.DeserializeObject<JsonFormat>(streamReader.ReadToEnd());
    }

    public static void Export(string file, List<Account> accounts)
    {
      using (StreamWriter streamWriter = new StreamWriter(file))
        streamWriter.WriteLine(JsonConvert.SerializeObject((object) new JsonFormat(accounts)));
    }
  }
}
