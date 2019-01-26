// Decompiled with JetBrains decompiler
// Type: ReAuth.LoadClientVersion
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace ReAuth
{
  internal class LoadClientVersion
  {
    public static void GetLatestRad()
    {
      if (System.IO.File.Exists(Path.Combine(Utils.MyLocation, "system.yaml")))
        System.IO.File.Delete(Path.Combine(Utils.MyLocation, "system.yaml"));
      using (WebClient webClient = new WebClient())
      {
        string str = ((IEnumerable<string>) webClient.DownloadString("http://l3cdn.riotgames.com/releases/live/solutions/league_client_sln/releases/releaselisting_NA").Split(new string[1]
        {
          Environment.NewLine
        }, StringSplitOptions.None)).First<string>();
        string[] array = webClient.DownloadString("http://l3cdn.riotgames.com/releases/live/solutions/league_client_sln/releases/" + str + "/solutionmanifest").Split(new string[1]
        {
          Environment.NewLine
        }, StringSplitOptions.None);
        int num = Array.IndexOf<string>(array, "league_client");
        webClient.DownloadFile("http://l3cdn.riotgames.com/releases/live/projects/league_client/releases/" + array[num + 1] + "/files/system.yaml.compressed", Path.Combine(Utils.MyLocation, "system.yaml.compressed"));
        Utils.DecompressFile(Path.Combine(Utils.MyLocation, "system.yaml.compressed"), Path.Combine(Utils.MyLocation, "system.yaml"));
        System.IO.File.Delete(Path.Combine(Utils.MyLocation, "system.yaml.compressed"));
      }
    }

    public static void GetLatestRadPbe()
    {
      if (System.IO.File.Exists(Path.Combine(Utils.MyLocation, "pbe.system.yaml")))
        System.IO.File.Delete(Path.Combine(Utils.MyLocation, "pbe.system.yaml"));
      using (WebClient webClient = new WebClient())
      {
        string str = ((IEnumerable<string>) webClient.DownloadString("http://l3cdn.riotgames.com/releases/pbe/solutions/league_client_sln/releases/releaselisting_PBE").Split(new string[1]
        {
          Environment.NewLine
        }, StringSplitOptions.None)).First<string>();
        string[] array = webClient.DownloadString("http://l3cdn.riotgames.com/releases/pbe/solutions/league_client_sln/releases/" + str + "/solutionmanifest").Split(new string[1]
        {
          Environment.NewLine
        }, StringSplitOptions.None);
        int num = Array.IndexOf<string>(array, "league_client");
        webClient.DownloadFile("http://l3cdn.riotgames.com/releases/pbe/projects/league_client/releases/" + array[num + 1] + "/files/system.yaml.compressed", Path.Combine(Utils.MyLocation, "pbe.system.yaml.compressed"));
        Utils.DecompressFile(Path.Combine(Utils.MyLocation, "pbe.system.yaml.compressed"), Path.Combine(Utils.MyLocation, "pbe.system.yaml"));
        System.IO.File.Delete(Path.Combine(Utils.MyLocation, "pbe.system.yaml.compressed"));
      }
    }
  }
}
