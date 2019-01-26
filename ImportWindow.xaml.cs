// Decompiled with JetBrains decompiler
// Type: ReAuth.ImportWindow
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using MahApps.Metro.Controls;
using ReAuth.Classes;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace ReAuth
{
  public partial class ImportWindow : MetroWindow, IComponentConnector
  {
    private readonly List<string[]> _accounts;
    internal DataGrid AccountsGrid;
    internal ComboBox RegionBox;
    private bool _contentLoaded;

    public ImportWindow(List<string[]> accounts)
    {
      this.\u002Ector();
      this.InitializeComponent();
      this._accounts = accounts;
      this.AccountsGrid.ItemsSource = (IEnumerable) this._accounts;
      this.RegionBox.ItemsSource = (IEnumerable) Enum.GetValues(typeof (Region)).Cast<Region>();
      this.RegionBox.SelectedItem = (object) Settings.Config.SelectedRegion;
    }

    private void OnChangeRegion(object sender, SelectionChangedEventArgs e)
    {
      Settings.Config.SelectedRegion = (Region) this.RegionBox.SelectedIndex;
    }

    private void BtnImportClick(object sender, RoutedEventArgs e)
    {
      foreach (string[] strArray in this._accounts.Where<string[]>((Func<string[], bool>) (a => Checker.Accounts.All<Account>((Func<Account, bool>) (aa => !string.Equals(aa.Username, a[0], StringComparison.CurrentCultureIgnoreCase))))))
      {
        try
        {
          string str = Settings.Config.SelectedRegion.ToString();
          Checker.Accounts.Add(new Account()
          {
            Username = strArray[0],
            Password = strArray[1],
            State = Account.Result.Unchecked,
            Region = str
          });
        }
        catch
        {
        }
      }
      ((Window) this).Close();
    }

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/ReAuth;component/importwindow.xaml", UriKind.Relative));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.AccountsGrid = (DataGrid) target;
          break;
        case 2:
          this.RegionBox = (ComboBox) target;
          this.RegionBox.SelectionChanged += new SelectionChangedEventHandler(this.OnChangeRegion);
          break;
        case 3:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.BtnImportClick);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
