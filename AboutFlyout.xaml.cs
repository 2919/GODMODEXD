// Decompiled with JetBrains decompiler
// Type: ReAuth.AboutFlyout
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace ReAuth
{
  public partial class AboutFlyout : UserControl, IComponentConnector
  {
    private bool _contentLoaded;

    public AboutFlyout()
    {
      this.InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      Process.Start("https://github.com/fiftythreecorp/LeagueOfLegendsAccountChecker");
    }

    private void Update_Click(object sender, RoutedEventArgs e)
    {
      Process.Start("https://github.com/fiftythreecorp/LeagueOfLegendsAccountChecker/releases");
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      Process.Start("https://github.com/fiftythreecorp/LeagueOfLegendsAccountChecker");
    }

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/ReAuth;component/aboutflyout.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      if (connectionId != 1)
      {
        if (connectionId == 2)
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Update_Click);
        else
          this._contentLoaded = true;
      }
      else
        ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click);
    }
  }
}
