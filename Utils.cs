// Decompiled with JetBrains decompiler
// Type: ReAuth.Utils
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using Microsoft.Win32;
using ReAuth.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using zlib;

namespace ReAuth
{
  internal class Utils
  {
    public static object GetPropertyValue(object src, string propName)
    {
      if (propName.Contains("."))
      {
        string[] strArray = propName.Split(new char[1]
        {
          '.'
        }, 2);
        return Utils.GetPropertyValue(Utils.GetPropertyValue(src, strArray[0]), strArray[1]);
      }
      PropertyInfo property = src.GetType().GetProperty(propName);
      if ((object) property == null)
        return (object) null;
      return property.GetValue(src, (object[]) null);
    }

    private static int FindDataGridRecordWithinRange(
      DataGrid dataGrid,
      int min,
      int max,
      Func<object, bool> itemCompareMethod)
    {
      for (int index = min; index <= max; ++index)
      {
        if (dataGrid.SelectedIndex != index && itemCompareMethod(dataGrid.Items[index]))
          return index;
      }
      return -1;
    }

    public static void UseDefaultExtAsFilterIndex(FileDialog dialog)
    {
      string str = "*." + dialog.DefaultExt;
      string[] strArray = dialog.Filter.Split('|');
      for (int index = 1; index < strArray.Length; index += 2)
      {
        if (strArray[index] == str)
        {
          dialog.FilterIndex = 1 + (index - 1) / 2;
          break;
        }
      }
    }

    public static IEnumerable<DependencyObject> GetChildObjects(
      DependencyObject parent)
    {
      if (parent != null)
      {
        if (parent is ContentElement || parent is FrameworkElement)
        {
          foreach (object child in LogicalTreeHelper.GetChildren(parent))
          {
            if (child is DependencyObject)
              yield return (DependencyObject) child;
          }
        }
        else
        {
          for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); ++i)
            yield return VisualTreeHelper.GetChild(parent, i);
        }
      }
    }

    public static IEnumerable<T> FindChildren<T>(DependencyObject parent) where T : DependencyObject
    {
      if (parent != null)
      {
        foreach (DependencyObject childObject in Utils.GetChildObjects(parent))
        {
          DependencyObject child = childObject;
          T obj = child as T;
          if ((object) obj != null)
            yield return obj;
          foreach (T child1 in Utils.FindChildren<T>(child))
            yield return child1;
          child = (DependencyObject) null;
        }
      }
    }

    public static List<string[]> GetLogins(string file)
    {
      List<string[]> strArrayList = new List<string[]>();
      StreamReader streamReader = new StreamReader(file);
      while (!streamReader.EndOfStream)
      {
        string str = streamReader.ReadLine();
        if (!string.IsNullOrEmpty(str) && !str.StartsWith("#"))
          strArrayList.Add(str.Split(':'));
      }
      return strArrayList;
    }

    public static void DataGridSearchByLetterKey(
      DataGrid dataGrid,
      Key key,
      Func<object, bool> itemCompareMethod)
    {
      if (dataGrid.Items.Count == 0 || Keyboard.Modifiers.HasFlag((Enum) ModifierKeys.Control) || (key < Key.A || key > Key.Z))
        return;
      int selectedIndex = dataGrid.SelectedIndex;
      int recordWithinRange = Utils.FindDataGridRecordWithinRange(dataGrid, selectedIndex, dataGrid.Items.Count - 1, itemCompareMethod);
      if (recordWithinRange == -1)
        recordWithinRange = Utils.FindDataGridRecordWithinRange(dataGrid, 0, selectedIndex - 1, itemCompareMethod);
      if (recordWithinRange <= -1)
        return;
      dataGrid.ScrollIntoView(dataGrid.Items[recordWithinRange]);
      dataGrid.SelectedIndex = recordWithinRange;
    }

    public static void AccountsDataDataGridSearchByLetterKey(object sender, KeyEventArgs e)
    {
      DataGrid dataGrid = sender as DataGrid;
      Func<object, bool> func = (Func<object, bool>) (item =>
      {
        Account account = item as Account;
        return account != null && account.Username.StartsWith(e.Key.ToString(), true, CultureInfo.CurrentCulture);
      });
      int key = (int) e.Key;
      Func<object, bool> itemCompareMethod = func;
      Utils.DataGridSearchByLetterKey(dataGrid, (Key) key, itemCompareMethod);
    }

    public static void ExportLogins(string file, List<Account> accounts, bool exportErrors)
    {
      using (StreamWriter streamWriter = new StreamWriter(file))
      {
        if (!exportErrors)
          accounts = accounts.Where<Account>((Func<Account, bool>) (a => a.State == Account.Result.Success)).ToList<Account>();
        foreach (Account account in accounts)
          streamWriter.WriteLine("{0}:{1}", (object) account.Username, (object) account.Password);
      }
    }

    public static void ExportProxy(string file, List<ProxyData> proxies)
    {
      using (StreamWriter streamWriter = new StreamWriter(file))
      {
        foreach (ProxyData proxy in proxies)
          streamWriter.WriteLine("{0}:{1}", (object) proxy.Host, (object) proxy.Port);
      }
    }

    public static void AccountsDataGrid_RightClickCommand(object sender, DataGrid accountsDataGrid)
    {
      if (accountsDataGrid.SelectedItems.Count == 0)
        return;
      string[] strArray = ((FrameworkElement) sender).Tag.ToString().Split(',');
      StringBuilder stringBuilder = new StringBuilder();
      foreach (Account selectedItem in (IEnumerable) accountsDataGrid.SelectedItems)
      {
        for (int index = 0; index <= strArray.Length - 1; ++index)
        {
          stringBuilder.Append(Utils.GetPropertyValue((object) selectedItem, strArray[index]));
          if (index < strArray.Length - 1)
            stringBuilder.Append(":");
        }
        if (accountsDataGrid.SelectedItems.Count > 1)
          stringBuilder.Append(Environment.NewLine);
      }
      Clipboard.SetDataObject((object) stringBuilder.ToString());
    }

    public static void DecompressFile(string inFile, string outFile)
    {
      FileStream fileStream = new FileStream(outFile, FileMode.Create);
      ZInputStream zinputStream = new ZInputStream((Stream) File.Open(inFile, FileMode.Open, FileAccess.Read));
      int num1;
      while (-1 != (num1 = ((BinaryReader) zinputStream).Read()))
      {
        byte num2 = (byte) num1;
        fileStream.WriteByte(num2);
      }
      ((BinaryReader) zinputStream).Close();
      fileStream.Close();
    }

    public static string MyLocation
    {
      get
      {
        return Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)?.Replace("file:\\", "");
      }
    }

    public static void ExportException(Exception e)
    {
      string str = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
      if (!Directory.Exists(str))
        Directory.CreateDirectory(str);
      string path2 = string.Format("crash_{0:dd-MM-yyyy_HH-mm-ss}.txt", (object) DateTime.Now);
      using (StreamWriter streamWriter = new StreamWriter(Path.Combine(str, path2)))
        streamWriter.WriteLine(e.ToString());
    }

    public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
      DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
      dateTime = dateTime.AddMilliseconds((double) unixTimeStamp);
      return dateTime;
    }
  }
}
