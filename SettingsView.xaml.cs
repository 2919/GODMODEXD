// Decompiled with JetBrains decompiler
// Type: ReAuth.SettingsView
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using MahApps.Metro.Controls;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace ReAuth
{
  public partial class SettingsView : UserControl, IComponentConnector
  {
    internal ToggleSwitch CheckUpdates;
    internal Label WorkersLabel;
    internal Slider WorkersCount;
    private bool _contentLoaded;

    public SettingsView()
    {
      this.InitializeComponent();
      this.WorkersCount.Value = (double) Settings.Config.Workers;
      this.WorkersLabel.Content = (object) string.Format("Workers count: {0}", (object) Settings.Config.Workers);
    }

    private void WorkersOnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      this.WorkersLabel.Content = (object) string.Format("Workers count: {0}", (object) this.WorkersCount.Value);
      Settings.Config.Workers = (int) this.WorkersCount.Value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/ReAuth;component/settingsview.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.CheckUpdates = (ToggleSwitch) target;
          break;
        case 2:
          this.WorkersLabel = (Label) target;
          break;
        case 3:
          this.WorkersCount = (Slider) target;
          this.WorkersCount.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.WorkersOnValueChanged);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
