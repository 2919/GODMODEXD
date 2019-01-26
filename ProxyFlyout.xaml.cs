// Decompiled with JetBrains decompiler
// Type: ReAuth.ProxyFlyout
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace ReAuth
{
  public partial class ProxyFlyout : UserControl, IComponentConnector
  {
    internal DataGrid ProxyGrid;
    internal DataGridTextColumn Password;
    internal Button ButtonProxyAdd;
    internal Button ButtonProxyStopChecking;
    internal Button ButtonProxyExport;
    internal Label LabelProxyStatus;
    private bool _contentLoaded;

    public static MainWindow Main { get; set; }

    public ProxyFlyout()
    {
      this.InitializeComponent();
      StaticHelper.ProxyFlyout = this;
      this.ProxyGrid.ItemsSource = (IEnumerable) StaticHelper.ProxyList;
    }

    private void ProxyRotateSlider_OnValueChanged(
      object sender,
      RoutedPropertyChangedEventArgs<double> e)
    {
      StaticHelper.SettingsSave.ProxyChange = 1;
    }

    private void ProxyStopCheckingClick(object sender, RoutedEventArgs e)
    {
      StaticHelper.StopChecking = true;
      StaticHelper.StopProxyChecking();
    }

    private void AddProxyClick(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      openFileDialog1.DefaultExt = ".txt";
      openFileDialog1.Filter = "Check list|*.txt";
      openFileDialog1.Multiselect = false;
      OpenFileDialog openFileDialog2 = openFileDialog1;
      bool? nullable = openFileDialog2.ShowDialog();
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      StaticHelper.LoadProxyFromFile(openFileDialog2.FileName);
    }

    private async void ProxyWorkingExportClick(object sender, RoutedEventArgs e)
    {
      List<ProxyData> proxyList = StaticHelper.ProxyList;
      if (proxyList.Count == 0)
        return;
      SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      saveFileDialog1.FileName = "outputProxy";
      saveFileDialog1.Filter = "Text file (*.txt)|*.txt";
      SaveFileDialog saveFileDialog2 = saveFileDialog1;
      bool? nullable = saveFileDialog2.ShowDialog();
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        return;
      MetroDialogSettings metroDialogSettings = new MetroDialogSettings();
      metroDialogSettings.set_AffirmativeButtonText("Yes");
      metroDialogSettings.set_NegativeButtonText("No");
      metroDialogSettings.set_FirstAuxiliaryButtonText("Cancel");
      Utils.ExportProxy(saveFileDialog2.FileName, proxyList);
      this.LabelProxyStatus.Content = (object) "Working proxy have been exported!";
    }

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/ReAuth;component/proxyflyout.xaml", UriKind.Relative));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.ProxyGrid = (DataGrid) target;
          break;
        case 2:
          this.Password = (DataGridTextColumn) target;
          break;
        case 3:
          this.ButtonProxyAdd = (Button) target;
          this.ButtonProxyAdd.Click += new RoutedEventHandler(this.AddProxyClick);
          break;
        case 4:
          this.ButtonProxyStopChecking = (Button) target;
          this.ButtonProxyStopChecking.Click += new RoutedEventHandler(this.ProxyStopCheckingClick);
          break;
        case 5:
          this.ButtonProxyExport = (Button) target;
          this.ButtonProxyExport.Click += new RoutedEventHandler(this.ProxyWorkingExportClick);
          break;
        case 6:
          this.LabelProxyStatus = (Label) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
