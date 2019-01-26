// Decompiled with JetBrains decompiler
// Type: ReAuth.StaticHelper
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;

namespace ReAuth
{
  public static class StaticHelper
  {
    public static bool StopChecking = false;
    public static List<ProxyData> ProxyList = new List<ProxyData>();
    public static SettingsSave SettingsSave = new SettingsSave();

    public static MainWindow Main { get; set; }

    public static ProxyFlyout ProxyFlyout { get; set; }

    public static string Latest { get; set; }

    public static List<string> ProxyLocationList { get; set; }

    public static void StopProxyChecking()
    {
      if (StaticHelper.ProxyList.Count > 0)
      {
        StaticHelper.ProxyFlyout.LabelProxyStatus.Content = (object) "Status: Checked";
        StaticHelper.ProxyFlyout.ButtonProxyExport.IsEnabled = true;
      }
      else
      {
        StaticHelper.ProxyFlyout.LabelProxyStatus.Content = (object) "Waiting for proxy";
        StaticHelper.ProxyFlyout.ButtonProxyExport.IsEnabled = true;
      }
      StaticHelper.ProxyFlyout.ButtonProxyStopChecking.IsEnabled = false;
    }

    public static void LoadProxyFromFile(string location)
    {
      StaticHelper.StopChecking = false;
      StaticHelper.ProxyFlyout.LabelProxyStatus.Content = (object) "Status: Checking proxy";
      StaticHelper.ProxyFlyout.ButtonProxyStopChecking.IsEnabled = true;
      StaticHelper.ProxyFlyout.ButtonProxyExport.IsEnabled = false;
      new Thread((ThreadStart) (() =>
      {
        if (StaticHelper.ProxyLocationList == null)
          StaticHelper.ProxyLocationList = new List<string>();
        StaticHelper.ProxyLocationList.Add(location);
        Parallel.ForEach<string>(((IEnumerable<string>) System.IO.File.ReadAllLines(location)).Where<string>((Func<string, bool>) (x =>
        {
          if (x.Contains(":"))
            return !string.IsNullOrWhiteSpace(x);
          return false;
        })), (Action<string, ParallelLoopState>) (async (account, state) =>
        {
          if (StaticHelper.StopChecking)
            state.Stop();
          string[] strArray = account.Split(':');
          try
          {
            ProxyData proxy = new ProxyData()
            {
              Host = strArray[0],
              Port = int.Parse(strArray[1])
            };
            if (StaticHelper.ProxyList.Contains(proxy) || !StaticHelper.VerifyRiot(proxy.Host, proxy.Port))
              return;
            await ((DispatcherObject) StaticHelper.Main).Dispatcher.BeginInvoke(DispatcherPriority.Send, (Delegate) (() =>
            {
              StaticHelper.ProxyList.Add(proxy);
              StaticHelper.ProxyFlyout.ProxyGrid.ItemsSource = (IEnumerable) StaticHelper.ProxyList;
              StaticHelper.ProxyFlyout.ProxyGrid.Items.Refresh();
              CollectionViewSource.GetDefaultView((object) StaticHelper.ProxyFlyout.ProxyGrid.ItemsSource).Refresh();
              StaticHelper.Main.CountWorkingProxy();
            }));
          }
          catch
          {
          }
        }));
        StaticHelper.SettingsSave.ProxyList = JsonConvert.SerializeObject((object) StaticHelper.ProxyLocationList);
      })).Start();
    }

    public static bool VerifyRiot(string host, int port)
    {
      bool returnVal = false;
      Thread thread = new Thread((ThreadStart) (() =>
      {
        try
        {
          HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create("https://auth.riotgames.com/.well-known/openid-configuration");
          httpWebRequest.Method = "GET";
          httpWebRequest.Proxy = (IWebProxy) new WebProxy(host, port);
          Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
          if (responseStream == null)
            throw new InvalidOperationException();
          using (StreamReader streamReader = new StreamReader(responseStream))
          {
            streamReader.ReadToEnd();
            returnVal = true;
          }
        }
        catch
        {
          returnVal = false;
        }
      }));
      thread.Start();
      thread.Join();
      return returnVal;
    }
  }
}
