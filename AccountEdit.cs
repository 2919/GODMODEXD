// Decompiled with JetBrains decompiler
// Type: ReAuth.AccountEdit
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
  public class AccountEdit : MetroWindow, IComponentConnector
  {
    private readonly List<Account> _accounts;
    internal Label UsernameLabel;
    internal Label PasswordLabel;
    internal TextBox UsernameBox;
    internal PasswordBox PasswordBox;
    internal TextBox PasswordBoxText;
    internal ComboBox RegionBox;
    internal Label ResultLabel;
    private bool _contentLoaded;

    public AccountEdit(List<Account> accounts = null)
    {
      this.\u002Ector();
      this.InitializeComponent();
      this.RegionBox.ItemsSource = (IEnumerable) Enum.GetValues(typeof (Region)).Cast<Region>();
      this.RegionBox.SelectedItem = (object) Settings.Config.SelectedRegion;
      this._accounts = accounts;
      if (Settings.Config.ShowPasswords)
      {
        this.PasswordBoxText.Text = this.PasswordBox.Password;
        this.PasswordBoxText.Visibility = Visibility.Visible;
        this.PasswordBox.Visibility = Visibility.Hidden;
      }
      else
      {
        this.PasswordBox.Password = this.PasswordBoxText.Text;
        this.PasswordBoxText.Visibility = Visibility.Hidden;
        this.PasswordBox.Visibility = Visibility.Visible;
      }
      if (this._accounts == null)
        return;
      if (this._accounts.Count == 1)
      {
        this.UsernameBox.Text = this._accounts[0].Username;
        this.PasswordBox.Password = this._accounts[0].Password;
        this.PasswordBoxText.Text = this._accounts[0].Password;
        this.RegionBox.SelectedItem = (object) this._accounts[0].Region;
      }
      else
      {
        this.UsernameLabel.Visibility = Visibility.Collapsed;
        this.UsernameBox.Visibility = Visibility.Collapsed;
        this.PasswordLabel.Visibility = Visibility.Collapsed;
        this.PasswordBox.Visibility = Visibility.Collapsed;
        this.PasswordBoxText.Visibility = Visibility.Collapsed;
      }
    }

    private void BtnSaveClick(object sender, RoutedEventArgs e)
    {
      if (Checker.IsChecking)
      {
        this.ResultLabel.Content = (object) "Stop the checker before saving.";
      }
      else
      {
        List<Account> accounts = this._accounts;
        // ISSUE: explicit non-virtual call
        if ((accounts != null ? (__nonvirtual (accounts.Count) > 1 ? 1 : 0) : 0) != 0)
        {
          foreach (Account account1 in this._accounts)
          {
            Account acc = account1;
            Account account2 = Checker.Accounts.FirstOrDefault<Account>((Func<Account, bool>) (a => a == acc));
            string str = this.RegionBox.SelectedItem.ToString();
            if (account2 != null && account2.Region != str)
            {
              account2.Region = str;
              account2.State = Account.Result.Unchecked;
            }
          }
          ((Window) this).Close();
        }
        string str1 = Settings.Config.ShowPasswords ? this.PasswordBoxText.Text : this.PasswordBox.Password;
        if (string.IsNullOrEmpty(this.UsernameBox.Text) || string.IsNullOrWhiteSpace(str1))
          this.ResultLabel.Content = (object) "Insert a username and password!";
        else if (this._accounts == null)
        {
          if (Checker.Accounts.Any<Account>((Func<Account, bool>) (a => a.Username.ToLower() == this.UsernameBox.Text.ToLower())))
          {
            this.ResultLabel.Content = (object) "Username already exists!";
          }
          else
          {
            Checker.Accounts.Add(new Account()
            {
              Username = this.UsernameBox.Text,
              Password = str1,
              Region = this.RegionBox.SelectedItem.ToString()
            });
            this.ResultLabel.Content = (object) "Account successfuly added!";
            this.UsernameBox.Text = string.Empty;
            this.PasswordBoxText.Text = string.Empty;
            this.PasswordBox.Password = string.Empty;
          }
        }
        else if (this._accounts[0].Username != this.UsernameBox.Text && Checker.Accounts.Any<Account>((Func<Account, bool>) (a => a.Username.ToLower() == this.UsernameBox.Text.ToLower())))
        {
          this.ResultLabel.Content = (object) "Username already exists!";
        }
        else
        {
          Account account = Checker.Accounts.FirstOrDefault<Account>((Func<Account, bool>) (a => a == this._accounts[0]));
          if (account == null)
            return;
          account.Username = this.UsernameBox.Text;
          account.Password = str1;
          account.Region = this.RegionBox.SelectedItem.ToString();
          account.State = Account.Result.Unchecked;
          this._accounts[0] = account;
          this.ResultLabel.Content = (object) "Account successfuly edited!";
        }
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/ReAuth;component/accounteditwindow.xaml", UriKind.Relative));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.UsernameLabel = (Label) target;
          break;
        case 2:
          this.PasswordLabel = (Label) target;
          break;
        case 3:
          this.UsernameBox = (TextBox) target;
          break;
        case 4:
          this.PasswordBox = (PasswordBox) target;
          break;
        case 5:
          this.PasswordBoxText = (TextBox) target;
          break;
        case 6:
          this.RegionBox = (ComboBox) target;
          break;
        case 7:
          this.ResultLabel = (Label) target;
          break;
        case 8:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.BtnSaveClick);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
