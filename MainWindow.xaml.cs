// Decompiled with JetBrains decompiler
// Type: ReAuth.MainWindow
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using ReAuth.Classes;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;

namespace ReAuth
{
  public partial class MainWindow : MetroWindow, IComponentConnector
  {
    public static readonly string version = "v1.5";
    private static readonly ICollectionView CheckedAccountsView = new CollectionViewSource()
    {
      Source = ((object) Checker.Accounts)
    }.View;
    internal static Predicate<object> CheckedAccountsViewDefaultFilter = (Predicate<object>) (item => ((Account) item).State == Account.Result.Success);
    public bool IsFilterActive;
    public static MainWindow Instance;
    internal Button Help;
    internal Button About;
    internal Button Settings;
    internal Flyout HelpFlyout;
    internal Flyout SettingFlyout;
    internal SettingsView SettingsView;
    internal Flyout ProxyFlyout;
    internal ReAuth.ProxyFlyout ProxyView;
    internal Flyout AboutFlyout;
    internal ReAuth.AboutFlyout AboutView;
    internal DataGrid AccountGrid;
    internal DataGridTextColumn IPHeader;
    internal DataGridTextColumn RPHeader;
    internal DataGridTextColumn ChampsHeader;
    internal DataGridTextColumn RunesHeader;
    internal DataGridTextColumn LastPlay;
    internal Button FilterWindow;
    internal Button AddProxy;
    internal Button VerifyButton;
    internal Button ImportButton;
    internal Button ExportButton;
    internal Label CheckedLabel;
    internal Label StatusLabel;
    internal Label ProxyLabel;
    private bool _contentLoaded;

    static MainWindow()
    {
      MainWindow.CheckedAccountsView.Filter = MainWindow.CheckedAccountsViewDefaultFilter;
    }

    public MainWindow()
    {
      base.\u002Ector();
      this.InitializeComponent();
      MainWindow.Instance = this;
      StaticHelper.Main = this;
      this.AccountGrid.ItemsSource = (IEnumerable) MainWindow.CheckedAccountsView;
      this.AccountGrid.PreviewKeyDown += new KeyEventHandler(Utils.AccountsDataDataGridSearchByLetterKey);
      this.AccountGrid.Items.IsLiveSorting = new bool?(true);
      this.AccountGrid.Loaded += (RoutedEventHandler) ((_param1, _param2) =>
      {
        foreach (DataGridColumn column in (Collection<DataGridColumn>) this.AccountGrid.Columns)
        {
          column.MinWidth = column.ActualWidth;
          column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
        }
      });
      Checker.Accounts.CollectionChanged += (NotifyCollectionChangedEventHandler) ((_param1, _param2) => this.UpdateControls());
    }

    private void GitHubClick(object sender, RoutedEventArgs e)
    {
      Process.Start("https://github.com/fiftythreecorp/LeagueOfLegendsAccountChecker");
    }

    private void FilterClick(object sender, RoutedEventArgs e)
    {
      if (this.IsFilterActive)
        this.ClearFilter();
      else
        ((Window) new SearchWindow()).ShowDialog();
    }

    public void ClearFilter()
    {
      if (!this.IsFilterActive)
        return;
      CollectionView defaultView = (CollectionView) CollectionViewSource.GetDefaultView((object) this.AccountGrid.ItemsSource);
      if (defaultView != null)
        defaultView.Filter = MainWindow.CheckedAccountsViewDefaultFilter;
      this.FilterWindow.Content = (object) "Filter";
      this.IsFilterActive = false;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      if (Checker.IsChecking)
      {
        Checker.Stop();
        this.UpdateControls();
      }
      else if (StaticHelper.ProxyList.Count > 0)
        Checker.Start();
      else
        DialogManager.ShowMessageAsync((MetroWindow) this, "Proxy Error", "You have not added proxy.", (MessageDialogStyle) 0, (MetroDialogSettings) null);
    }

    public void CountWorkingProxy()
    {
      this.ProxyLabel.Content = (object) ("Working proxy: " + (object) StaticHelper.ProxyList.Count);
    }

    public void UpdateControls()
    {
      if (!((DispatcherObject) this).Dispatcher.CheckAccess())
      {
        ((DispatcherObject) this).Dispatcher.Invoke(new Action(this.UpdateControls));
      }
      else
      {
        int num = Checker.Accounts.Count<Account>((Func<Account, bool>) (a => (uint) a.State > 0U));
        this.VerifyButton.Content = Checker.IsChecking ? (object) "Stop" : (object) "Start";
        this.ImportButton.IsEnabled = !Checker.IsChecking;
        this.ExportButton.IsEnabled = num > 0;
        this.FilterWindow.IsEnabled = num > 0;
        if (Checker.CancellationTokenSource != null && Checker.CancellationTokenSource.IsCancellationRequested)
        {
          this.VerifyButton.IsEnabled = false;
          this.StatusLabel.Content = (object) "Status: Stopping...";
        }
        else
        {
          this.VerifyButton.IsEnabled = num < Checker.Accounts.Count;
          if (Checker.IsChecking)
            this.StatusLabel.Content = (object) "Status: Checking...";
          else if (num > 0 && num == Checker.Accounts.Count)
            this.StatusLabel.Content = (object) "Status: Finished!";
          else
            this.StatusLabel.Content = (object) "Status: Stopped!";
        }
        this.CheckedLabel.Content = (object) string.Format("Checked: {0}/{1}", (object) num, (object) Checker.Accounts.Count);
        CollectionViewSource.GetDefaultView((object) this.AccountGrid.ItemsSource).Refresh();
        if (AccountsWindow.Instance == null)
          return;
        AccountsWindow.Instance.UpdateControls();
      }
    }

    private void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
    {
      if (AccountsWindow.Instance == null)
      {
        AccountsWindow.Instance = new AccountsWindow();
        ((Window) AccountsWindow.Instance).Show();
      }
      else if (((Window) AccountsWindow.Instance).WindowState == WindowState.Minimized)
        ((Window) AccountsWindow.Instance).WindowState = WindowState.Normal;
      else
        ((Window) AccountsWindow.Instance).Activate();
    }

    private void BtnExportToFileClick(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      saveFileDialog1.FileName = "output";
      saveFileDialog1.Filter = "JavaScript Object Notation (*.json)|*.json";
      SaveFileDialog saveFileDialog2 = saveFileDialog1;
      bool? nullable = saveFileDialog2.ShowDialog();
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      List<Account> list = Checker.Accounts.Where<Account>((Func<Account, bool>) (a => a.State == Account.Result.Success)).ToList<Account>();
      JsonFormat.Export(saveFileDialog2.FileName, list);
      DialogManager.ShowMessageAsync((MetroWindow) this, "Export", string.Format("Exported {0} accounts.", (object) list.Count), (MessageDialogStyle) 0, (MetroDialogSettings) null);
    }

    private void CmExportJson(object sender, RoutedEventArgs e)
    {
      if (this.AccountGrid.SelectedItems.Count == 0)
        return;
      SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      saveFileDialog1.FileName = "output";
      saveFileDialog1.Filter = "JavaScript Object Notation (*.json)|*.json";
      SaveFileDialog saveFileDialog2 = saveFileDialog1;
      List<Account> list = this.AccountGrid.SelectedItems.Cast<Account>().ToList<Account>();
      bool? nullable = saveFileDialog2.ShowDialog();
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      JsonFormat.Export(saveFileDialog2.FileName, list);
      DialogManager.ShowMessageAsync((MetroWindow) this, "Export", string.Format("Exported {0} accounts.", (object) list.Count), (MessageDialogStyle) 0, (MetroDialogSettings) null);
    }

    private async void BtnImportClick(object sender, RoutedEventArgs e)
    {
      MainWindow mainWindow = this;
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      openFileDialog1.Filter = "JavaScript Object Notation (*.json)|*.json";
      OpenFileDialog openFileDialog2 = openFileDialog1;
      bool? nullable = openFileDialog2.ShowDialog();
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      string fileName = openFileDialog2.FileName;
      if (!File.Exists(fileName))
        return;
      JsonFormat jsonformat = JsonFormat.Import(fileName);
      if (jsonformat == null)
      {
        MessageDialogResult messageDialogResult1 = await DialogManager.ShowMessageAsync((MetroWindow) mainWindow, "Error", "Unable to load JSON file, it might be corrupted or from an old version.", (MessageDialogStyle) 0, (MetroDialogSettings) null);
      }
      else
      {
        Version version1 = Version.Parse(jsonformat.Version);
        Version version2 = Assembly.GetExecutingAssembly().GetName().Version;
        if (version1 != version2)
        {
          TaskAwaiter<MessageDialogResult> awaiter = DialogManager.ShowMessageAsync((MetroWindow) mainWindow, "Warning", string.Format("The file you are importing is from a {0} version. Some functions might not might not work, do you still want to load it?", version1 < version2 ? (object) "old" : (object) "new"), (MessageDialogStyle) 1, (MetroDialogSettings) null).GetAwaiter();
          if (!awaiter.IsCompleted)
          {
            int num;
            // ISSUE: explicit reference operation
            // ISSUE: reference to a compiler-generated field
            (^this).\u003C\u003E1__state = num = 1;
            TaskAwaiter<MessageDialogResult> taskAwaiter = awaiter;
            // ISSUE: explicit reference operation
            // ISSUE: reference to a compiler-generated field
            (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<MessageDialogResult>, MainWindow.\u003CBtnImportClick\u003Ed__16>(ref awaiter, this);
            return;
          }
          if (awaiter.GetResult() == null)
            return;
        }
        int num1 = 0;
        foreach (Account account1 in jsonformat.Accounts)
        {
          Account account = account1;
          if (Checker.Accounts.All<Account>((Func<Account, bool>) (a => !string.Equals(a.Username, account.Username, StringComparison.CurrentCultureIgnoreCase))))
          {
            Checker.Accounts.Add(account);
            ++num1;
          }
        }
        if (num1 > 0)
        {
          mainWindow.AccountGrid.Focus();
        }
        else
        {
          MessageDialogResult messageDialogResult2 = await DialogManager.ShowMessageAsync((MetroWindow) mainWindow, "Import", "No new accounts found.", (MessageDialogStyle) 0, (MetroDialogSettings) null);
        }
      }
    }

    private void Help_OnClick(object sender, RoutedEventArgs e)
    {
      if (!this.HelpFlyout.get_IsOpen())
      {
        this.SettingFlyout.set_IsOpen(false);
        this.ProxyFlyout.set_IsOpen(false);
        this.AboutFlyout.set_IsOpen(false);
      }
      this.HelpFlyout.set_IsOpen(!this.HelpFlyout.get_IsOpen());
    }

    private void Settings_OnClick(object sender, RoutedEventArgs e)
    {
      if (!this.SettingFlyout.get_IsOpen())
      {
        this.HelpFlyout.set_IsOpen(false);
        this.ProxyFlyout.set_IsOpen(false);
        this.AboutFlyout.set_IsOpen(false);
      }
      this.SettingFlyout.set_IsOpen(!this.SettingFlyout.get_IsOpen());
    }

    private void Proxy_OnClick(object sender, RoutedEventArgs e)
    {
      if (!this.ProxyFlyout.get_IsOpen())
      {
        this.SettingFlyout.set_IsOpen(false);
        this.HelpFlyout.set_IsOpen(false);
        this.AboutFlyout.set_IsOpen(false);
      }
      this.ProxyFlyout.set_IsOpen(!this.ProxyFlyout.get_IsOpen());
    }

    private void About_OnClick(object sender, RoutedEventArgs e)
    {
      if (!this.AboutFlyout.get_IsOpen())
      {
        this.SettingFlyout.set_IsOpen(false);
        this.HelpFlyout.set_IsOpen(false);
        this.ProxyFlyout.set_IsOpen(false);
      }
      this.AboutFlyout.set_IsOpen(!this.AboutFlyout.get_IsOpen());
    }

    private void MainWindow_OnClosed(object sender, EventArgs e)
    {
      Environment.Exit(0);
    }

    private void AccountGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    private void AccountsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    private void AccountsDataGrid_Cm(object sender, RoutedEventArgs e)
    {
      Utils.AccountsDataGrid_RightClickCommand(sender, this.AccountGrid);
    }

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/ReAuth;component/mainwindow.xaml", UriKind.Relative));
    }

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    internal Delegate _CreateDelegate(Type delegateType, string handler)
    {
      return Delegate.CreateDelegate(delegateType, (object) this, handler);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          ((Window) target).Closed += new EventHandler(this.MainWindow_OnClosed);
          break;
        case 2:
          this.Help = (Button) target;
          this.Help.Click += new RoutedEventHandler(this.Help_OnClick);
          break;
        case 3:
          this.About = (Button) target;
          this.About.Click += new RoutedEventHandler(this.About_OnClick);
          break;
        case 4:
          this.Settings = (Button) target;
          this.Settings.Click += new RoutedEventHandler(this.Settings_OnClick);
          break;
        case 5:
          this.HelpFlyout = (Flyout) target;
          break;
        case 6:
          this.SettingFlyout = (Flyout) target;
          break;
        case 7:
          this.SettingsView = (SettingsView) target;
          break;
        case 8:
          this.ProxyFlyout = (Flyout) target;
          break;
        case 9:
          this.ProxyView = (ReAuth.ProxyFlyout) target;
          break;
        case 10:
          this.AboutFlyout = (Flyout) target;
          break;
        case 11:
          this.AboutView = (ReAuth.AboutFlyout) target;
          break;
        case 12:
          this.AccountGrid = (DataGrid) target;
          this.AccountGrid.SelectionChanged += new SelectionChangedEventHandler(this.AccountsDataGrid_SelectionChanged);
          break;
        case 13:
          this.IPHeader = (DataGridTextColumn) target;
          break;
        case 14:
          this.RPHeader = (DataGridTextColumn) target;
          break;
        case 15:
          this.ChampsHeader = (DataGridTextColumn) target;
          break;
        case 16:
          this.RunesHeader = (DataGridTextColumn) target;
          break;
        case 17:
          this.LastPlay = (DataGridTextColumn) target;
          break;
        case 18:
          ((MenuItem) target).Click += new RoutedEventHandler(this.AccountsDataGrid_Cm);
          break;
        case 19:
          ((MenuItem) target).Click += new RoutedEventHandler(this.AccountsDataGrid_Cm);
          break;
        case 20:
          ((MenuItem) target).Click += new RoutedEventHandler(this.AccountsDataGrid_Cm);
          break;
        case 21:
          ((MenuItem) target).Click += new RoutedEventHandler(this.AccountsDataGrid_Cm);
          break;
        case 22:
          ((MenuItem) target).Click += new RoutedEventHandler(this.CmExportJson);
          break;
        case 23:
          this.FilterWindow = (Button) target;
          this.FilterWindow.Click += new RoutedEventHandler(this.FilterClick);
          break;
        case 24:
          this.AddProxy = (Button) target;
          this.AddProxy.Click += new RoutedEventHandler(this.Proxy_OnClick);
          break;
        case 25:
          this.VerifyButton = (Button) target;
          this.VerifyButton.Click += new RoutedEventHandler(this.ButtonBase_OnClick);
          break;
        case 26:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.ButtonBase_OnClick2);
          break;
        case 27:
          this.ImportButton = (Button) target;
          this.ImportButton.Click += new RoutedEventHandler(this.BtnImportClick);
          break;
        case 28:
          this.ExportButton = (Button) target;
          this.ExportButton.Click += new RoutedEventHandler(this.BtnExportToFileClick);
          break;
        case 29:
          this.CheckedLabel = (Label) target;
          break;
        case 30:
          this.StatusLabel = (Label) target;
          break;
        case 31:
          this.ProxyLabel = (Label) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
