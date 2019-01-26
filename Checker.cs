// Decompiled with JetBrains decompiler
// Type: ReAuth.Checker
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using ReAuth.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReAuth
{
  internal class Checker
  {
    private static readonly Random random = new Random();

    public static bool IsChecking { get; private set; }

    public static ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();

    public static CancellationTokenSource CancellationTokenSource { get; private set; }

    public static async void Start()
    {
      if (Checker.IsChecking)
        return;
      Checker.CancellationTokenSource = new CancellationTokenSource();
      Checker.IsChecking = true;
      MainWindow.Instance.UpdateControls();
      await Task.Factory.StartNew<Task>(new Func<Task>(Checker.DoWork), TaskCreationOptions.LongRunning).Unwrap().ConfigureAwait(false);
      Checker.IsChecking = false;
      Checker.CancellationTokenSource = (CancellationTokenSource) null;
      MainWindow.Instance.UpdateControls();
    }

    public static void Stop()
    {
      if (!Checker.IsChecking)
        return;
      Checker.CancellationTokenSource.Cancel();
      MainWindow.Instance.UpdateControls();
    }

    private static async Task DoWork()
    {
      using (SemaphoreSlim throttler = new SemaphoreSlim(Settings.Config.Workers))
      {
        List<Task> tasks = new List<Task>();
        int index = 0;
        foreach (Account account in Checker.Accounts.Where<Account>((Func<Account, bool>) (account => account.State == Account.Result.Unchecked)))
        {
          Account item = account;
          if (!Checker.CancellationTokenSource.IsCancellationRequested)
          {
            await throttler.WaitAsync().ConfigureAwait(false);
            tasks.Add((Task) Task.Run((Func<Task>) (async () =>
            {
              await new CheckerCore(item).Check().ConfigureAwait(false);
              MainWindow.Instance.UpdateControls();
            })).ContinueWith<int>((Func<Task, int>) (t => throttler.Release())));
            ++index;
          }
          else
            break;
        }
        await Task.WhenAll((IEnumerable<Task>) tasks).ConfigureAwait(false);
        tasks = (List<Task>) null;
      }
    }

    public static Task RandomDelay()
    {
      double minSecondsDelay = Settings.Config.MinSecondsDelay;
      double maxSecondsDelay = Settings.Config.MaxSecondsDelay;
      if (minSecondsDelay >= 0.0 && maxSecondsDelay > 0.0 && maxSecondsDelay > minSecondsDelay)
        return Task.Delay(TimeSpan.FromMilliseconds(Math.Floor((Checker.random.NextDouble() * (maxSecondsDelay - minSecondsDelay) + minSecondsDelay) * 1000.0)));
      return (Task) Task.FromResult<bool>(true);
    }
  }
}
