// Decompiled with JetBrains decompiler
// Type: ReAuth.Classes.Account
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using Newtonsoft.Json;
using System;

namespace ReAuth.Classes
{
  public class Account : BaseViewModel
  {
    private string _username;
    private string _password;
    private Account.Result _state;
    private string _region;

    public long AccountId { get; set; }

    public string Username
    {
      get
      {
        return this._username;
      }
      set
      {
        this.SetAndNotify<string>(ref this._username, value, nameof (Username));
      }
    }

    public string Password
    {
      get
      {
        return this._password;
      }
      set
      {
        this.SetAndNotify<string>(ref this._password, value, nameof (Password));
      }
    }

    public int SummonerId { get; set; }

    public string SummonerName { get; set; }

    public int Level { get; set; }

    public string EmailStatus { get; set; }

    public int RpBalance { get; set; }

    public int IpBalance { get; set; }

    public string PreviousSeasonRank { get; set; }

    public string SoloQRank { get; set; }

    public string LastPlay { get; set; }

    public long LastPlayLong { get; set; }

    public DateTime CheckedTime { get; set; }

    public int ChampionCount { get; set; }

    public int SkinCount { get; set; }

    public string ErrorMessage { get; set; }

    public Account.Result State
    {
      get
      {
        return this._state;
      }
      set
      {
        this.SetAndNotify<Account.Result>(ref this._state, value, nameof (State));
        this.RaisePropertyChanged("StateDisplay");
      }
    }

    public string Region
    {
      get
      {
        return this._region;
      }
      set
      {
        this.SetAndNotify<string>(ref this._region, value, this.Region);
      }
    }

    [JsonIgnore]
    public string StatusMessage { get; set; }

    [JsonIgnore]
    public string UncheckedMessage { get; set; } = "Unchecked";

    [JsonIgnore]
    public string StateDisplay
    {
      get
      {
        switch (this.State)
        {
          case Account.Result.Unchecked:
            return this.UncheckedMessage;
          case Account.Result.Success:
            return "Successfully Checked";
          case Account.Result.Error:
            return this.ErrorMessage;
          case Account.Result.Message:
            return this.StatusMessage;
          default:
            return string.Empty;
        }
      }
    }

    public enum Result
    {
      Unchecked,
      Success,
      Error,
      Message,
    }
  }
}
