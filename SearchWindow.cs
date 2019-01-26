// Decompiled with JetBrains decompiler
// Type: ReAuth.SearchWindow
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using MahApps.Metro.Controls;
using ReAuth.Classes;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace ReAuth
{
  public class SearchWindow : MetroWindow, IComponentConnector
  {
    internal CheckBox checkBoxMinimumLevel;
    internal NumericUpDown numericUpDownMinimumLevel;
    internal CheckBox checkBoxMinimumChamps;
    internal NumericUpDown numericUpDownMinimumChamps;
    internal CheckBox checkBoxMinimumSkins;
    internal NumericUpDown numericUpDownMinimumSkins;
    internal CheckBox checkBoxMinimumRP;
    internal NumericUpDown numericUpDownMinimumRP;
    internal CheckBox checkBoxMinimumIP;
    internal NumericUpDown numericUpDownMinimumIP;
    internal CheckBox checkBoxNotValidated;
    internal Button FilterButton;
    private bool _contentLoaded;

    public SearchWindow()
    {
      base.\u002Ector();
      this.InitializeComponent();
    }

    private void ApplyFilter(object sender, RoutedEventArgs e)
    {
      CollectionView defaultView = (CollectionView) CollectionViewSource.GetDefaultView((object) MainWindow.Instance.AccountGrid.ItemsSource);
      defaultView.Filter = new Predicate<object>(this.AccountsFilter);
      MainWindow.Instance.IsFilterActive = true;
      MainWindow.Instance.FilterWindow.Content = (object) "Clear Filter";
      // ISSUE: explicit non-virtual call
      __nonvirtual (((Window) this).Title) = string.Format("Filter - Results: {0}", (object) defaultView.Count);
    }

    private bool AccountsFilter(object item)
    {
      Account account = item as Account;
      List<bool> source = new List<bool>();
      if (account != null && account.State == Account.Result.Success)
      {
        bool? isChecked = this.checkBoxMinimumLevel.IsChecked;
        bool flag1 = true;
        if (isChecked.GetValueOrDefault() == flag1 & isChecked.HasValue)
        {
          List<bool> boolList = source;
          double level = (double) account.Level;
          double? nullable = this.numericUpDownMinimumLevel.get_Value();
          double valueOrDefault = nullable.GetValueOrDefault();
          int num = level >= valueOrDefault & nullable.HasValue ? 1 : 0;
          boolList.Add(num != 0);
        }
        isChecked = this.checkBoxMinimumChamps.IsChecked;
        bool flag2 = true;
        if (isChecked.GetValueOrDefault() == flag2 & isChecked.HasValue)
        {
          List<bool> boolList = source;
          double championCount = (double) account.ChampionCount;
          double? nullable = this.numericUpDownMinimumChamps.get_Value();
          double valueOrDefault = nullable.GetValueOrDefault();
          int num = championCount >= valueOrDefault & nullable.HasValue ? 1 : 0;
          boolList.Add(num != 0);
        }
        isChecked = this.checkBoxMinimumSkins.IsChecked;
        bool flag3 = true;
        if (isChecked.GetValueOrDefault() == flag3 & isChecked.HasValue)
        {
          List<bool> boolList = source;
          double skinCount = (double) account.SkinCount;
          double? nullable = this.numericUpDownMinimumSkins.get_Value();
          double valueOrDefault = nullable.GetValueOrDefault();
          int num = skinCount >= valueOrDefault & nullable.HasValue ? 1 : 0;
          boolList.Add(num != 0);
        }
        isChecked = this.checkBoxMinimumRP.IsChecked;
        bool flag4 = true;
        if (isChecked.GetValueOrDefault() == flag4 & isChecked.HasValue)
        {
          List<bool> boolList = source;
          double rpBalance = (double) account.RpBalance;
          double? nullable = this.numericUpDownMinimumRP.get_Value();
          double valueOrDefault = nullable.GetValueOrDefault();
          int num = rpBalance >= valueOrDefault & nullable.HasValue ? 1 : 0;
          boolList.Add(num != 0);
        }
        isChecked = this.checkBoxMinimumIP.IsChecked;
        bool flag5 = true;
        if (isChecked.GetValueOrDefault() == flag5 & isChecked.HasValue)
        {
          List<bool> boolList = source;
          double ipBalance = (double) account.IpBalance;
          double? nullable = this.numericUpDownMinimumIP.get_Value();
          double valueOrDefault = nullable.GetValueOrDefault();
          int num = ipBalance >= valueOrDefault & nullable.HasValue ? 1 : 0;
          boolList.Add(num != 0);
        }
        isChecked = this.checkBoxNotValidated.IsChecked;
        bool flag6 = true;
        if (isChecked.GetValueOrDefault() == flag6 & isChecked.HasValue)
          source.Add(account.EmailStatus == "false");
      }
      if (source.Count != 0)
        return source.All<bool>((Func<bool, bool>) (b => b));
      return false;
    }

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/ReAuth;component/filterwindow.xaml", UriKind.Relative));
    }

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.checkBoxMinimumLevel = (CheckBox) target;
          break;
        case 2:
          this.numericUpDownMinimumLevel = (NumericUpDown) target;
          break;
        case 3:
          this.checkBoxMinimumChamps = (CheckBox) target;
          break;
        case 4:
          this.numericUpDownMinimumChamps = (NumericUpDown) target;
          break;
        case 5:
          this.checkBoxMinimumSkins = (CheckBox) target;
          break;
        case 6:
          this.numericUpDownMinimumSkins = (NumericUpDown) target;
          break;
        case 7:
          this.checkBoxMinimumRP = (CheckBox) target;
          break;
        case 8:
          this.numericUpDownMinimumRP = (NumericUpDown) target;
          break;
        case 9:
          this.checkBoxMinimumIP = (CheckBox) target;
          break;
        case 10:
          this.numericUpDownMinimumIP = (NumericUpDown) target;
          break;
        case 11:
          this.checkBoxNotValidated = (CheckBox) target;
          break;
        case 12:
          this.FilterButton = (Button) target;
          this.FilterButton.Click += new RoutedEventHandler(this.ApplyFilter);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
