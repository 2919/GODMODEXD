// Decompiled with JetBrains decompiler
// Type: ReAuth.AccountsWindow
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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;

namespace ReAuth
{
  public partial class AccountsWindow : MetroWindow, IComponentConnector
  {
    public static AccountsWindow Instance;
    internal DataGrid AccountsDataGrid;
    internal ContextMenu ContextMenuAccounts;
    internal Button ButtonAdd;
    internal Button ButtonClear;
    internal Button ButtonImport;
    private bool _contentLoaded;

    public AccountsWindow()
    {
      base.\u002Ector();
      this.InitializeComponent();
      AccountsWindow.Instance = this;
      ((Window) this).Closed += (EventHandler) ((o, a) => AccountsWindow.Instance = (AccountsWindow) null);
      this.AccountsDataGrid.PreviewKeyDown += new KeyEventHandler(Utils.AccountsDataDataGridSearchByLetterKey);
      this.AccountsDataGrid.ItemsSource = (IEnumerable) Checker.Accounts;
      this.UpdateControls();
    }

    public void UpdateControls()
    {
      if (!((DispatcherObject) this).Dispatcher.CheckAccess())
      {
        ((DispatcherObject) this).Dispatcher.Invoke(new Action(this.UpdateControls));
      }
      else
      {
        this.ButtonClear.IsEnabled = !Checker.IsChecking && Checker.Accounts.Any<Account>();
        this.ButtonImport.IsEnabled = !Checker.IsChecking;
        foreach (MenuItem menuItem in (IEnumerable) this.ContextMenuAccounts.Items)
        {
          if (!menuItem.Header.ToString().Contains("Copy"))
            menuItem.IsEnabled = !Checker.IsChecking;
        }
      }
    }

    private void BtnAddAccountsClick(object sender, RoutedEventArgs e)
    {
      ((Window) new AccountEdit((List<Account>) null)).ShowDialog();
    }

    private void CmEditClick(object sender, RoutedEventArgs e)
    {
      if (this.AccountsDataGrid.SelectedItems.Count == 0)
        return;
      ((Window) new AccountEdit(this.AccountsDataGrid.SelectedItems.Cast<Account>().ToList<Account>())).ShowDialog();
    }

    private void BtnImportClick(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      openFileDialog1.Filter = "Text File (*.txt)|*.txt";
      OpenFileDialog openFileDialog2 = openFileDialog1;
      bool? nullable = openFileDialog2.ShowDialog();
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue) || !File.Exists(openFileDialog2.FileName))
        return;
      List<string[]> logins = Utils.GetLogins(openFileDialog2.FileName);
      if (!logins.Any<string[]>())
        return;
      ((Window) new ImportWindow(logins)).ShowDialog();
    }

    private void CmCopyComboClick(object sender, RoutedEventArgs e)
    {
      Utils.AccountsDataGrid_RightClickCommand(sender, this.AccountsDataGrid);
    }

    private async void CmMakeUncheckedClick(object sender, RoutedEventArgs e)
    {
      AccountsWindow accountsWindow = this;
      if (accountsWindow.AccountsDataGrid.SelectedItems.Count == 0)
        return;
      bool uncheckSuccess = false;
      bool toAll = false;
      MetroDialogSettings metroDialogSettings = new MetroDialogSettings();
      metroDialogSettings.set_AffirmativeButtonText("No");
      metroDialogSettings.set_NegativeButtonText("Yes");
      metroDialogSettings.set_FirstAuxiliaryButtonText("No to All");
      metroDialogSettings.set_SecondAuxiliaryButtonText("Yes to All");
      MetroDialogSettings settings = metroDialogSettings;
      foreach (Account selectedItem in (IEnumerable) accountsWindow.AccountsDataGrid.SelectedItems)
      {
        Account account = selectedItem;
        TaskAwaiter<MessageDialogResult> taskAwaiter;
        int num;
        TaskAwaiter<MessageDialogResult> awaiter;
        if (accountsWindow.AccountsDataGrid.SelectedItems.Count > 1)
        {
          if (account.State == Account.Result.Success)
          {
            if (!toAll)
            {
              awaiter = DialogManager.ShowMessageAsync((MetroWindow) accountsWindow, "Make Unchecked", string.Format("This account ({0}) was successfully checked, are you sure that you wanna make it unchecked?", (object) account.Username), (MessageDialogStyle) 3, settings).GetAwaiter();
              if (!awaiter.IsCompleted)
              {
                // ISSUE: explicit reference operation
                // ISSUE: reference to a compiler-generated field
                (^this).\u003C\u003E1__state = num = 0;
                taskAwaiter = awaiter;
                // ISSUE: explicit reference operation
                // ISSUE: reference to a compiler-generated field
                (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<MessageDialogResult>, AccountsWindow.\u003CCmMakeUncheckedClick\u003Ed__7>(ref awaiter, this);
                break;
              }
              switch (awaiter.GetResult() - 1)
              {
                case 0:
                  continue;
                case 1:
                  toAll = true;
                  continue;
                case 2:
                  toAll = true;
                  uncheckSuccess = true;
                  break;
              }
            }
            else if (!uncheckSuccess)
              continue;
          }
          account.State = Account.Result.Unchecked;
        }
        else
        {
          if (account.State == Account.Result.Success)
          {
            awaiter = DialogManager.ShowMessageAsync((MetroWindow) accountsWindow, "Make Unchecked", "This account was successfully checked, are you sure that you wanna make it unchecked?", (MessageDialogStyle) 1, (MetroDialogSettings) null).GetAwaiter();
            if (!awaiter.IsCompleted)
            {
              // ISSUE: explicit reference operation
              // ISSUE: reference to a compiler-generated field
              (^this).\u003C\u003E1__state = num = 1;
              taskAwaiter = awaiter;
              // ISSUE: explicit reference operation
              // ISSUE: reference to a compiler-generated field
              (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<MessageDialogResult>, AccountsWindow.\u003CCmMakeUncheckedClick\u003Ed__7>(ref awaiter, this);
              break;
            }
            if (awaiter.GetResult() == null)
              break;
          }
          account.State = Account.Result.Unchecked;
        }
        account = (Account) null;
      }
    }

    private async void CmRemoveClick(object sender, RoutedEventArgs e)
    {
      AccountsWindow accountsWindow = this;
      int count = accountsWindow.AccountsDataGrid.SelectedItems.Count;
      if (count == 0)
        return;
      MessageDialogResult messageDialogResult;
      if (count > 1)
        messageDialogResult = await DialogManager.ShowMessageAsync((MetroWindow) accountsWindow, "Remove", string.Format("Are you sure that you wanna remove {0} accounts?", (object) count), (MessageDialogStyle) 1, (MetroDialogSettings) null);
      else
        messageDialogResult = await DialogManager.ShowMessageAsync((MetroWindow) accountsWindow, "Remove", "Are you sure?", (MessageDialogStyle) 1, (MetroDialogSettings) null);
      if (messageDialogResult == null)
        return;
      foreach (Account account in accountsWindow.AccountsDataGrid.SelectedItems.Cast<Account>().ToList<Account>())
        Checker.Accounts.Remove(account);
    }

    private async void BtnClearAccountsClick(object sender, RoutedEventArgs e)
    {
      TaskAwaiter<MessageDialogResult> awaiter = DialogManager.ShowMessageAsync((MetroWindow) this, "Remove", "Are you sure?", (MessageDialogStyle) 1, (MetroDialogSettings) null).GetAwaiter();
      if (!awaiter.IsCompleted)
      {
        int num;
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003C\u003E1__state = num = 0;
        TaskAwaiter<MessageDialogResult> taskAwaiter = awaiter;
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<MessageDialogResult>, AccountsWindow.\u003CBtnClearAccountsClick\u003Ed__9>(ref awaiter, this);
      }
      else
      {
        if (awaiter.GetResult() == null)
          return;
        Checker.Accounts.Clear();
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/ReAuth;component/accountswindow.xaml", UriKind.Relative));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.AccountsDataGrid = (DataGrid) target;
          break;
        case 2:
          this.ContextMenuAccounts = (ContextMenu) target;
          break;
        case 3:
          ((MenuItem) target).Click += new RoutedEventHandler(this.CmEditClick);
          break;
        case 4:
          ((MenuItem) target).Click += new RoutedEventHandler(this.CmCopyComboClick);
          break;
        case 5:
          ((MenuItem) target).Click += new RoutedEventHandler(this.CmMakeUncheckedClick);
          break;
        case 6:
          ((MenuItem) target).Click += new RoutedEventHandler(this.CmRemoveClick);
          break;
        case 7:
          this.ButtonAdd = (Button) target;
          this.ButtonAdd.Click += new RoutedEventHandler(this.BtnAddAccountsClick);
          break;
        case 8:
          this.ButtonClear = (Button) target;
          this.ButtonClear.Click += new RoutedEventHandler(this.BtnClearAccountsClick);
          break;
        case 9:
          this.ButtonImport = (Button) target;
          this.ButtonImport.Click += new RoutedEventHandler(this.BtnImportClick);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
