// Decompiled with JetBrains decompiler
// Type: ReAuth.CheckerCore
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using ReAuth.Classes;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ReAuth
{
  internal class CheckerCore
  {
    private readonly Account account;

    public CheckerCore(Account account)
    {
      this.account = account;
    }

    public void OnUpdateStatus(object sender, string message)
    {
      this.account.StatusMessage = message;
      this.account.State = Account.Result.Message;
    }

    private void OnError(string error)
    {
      this.account.ErrorMessage = error;
      this.account.State = Account.Result.Error;
    }

    public async Task Check()
    {
      int maxAttempts = 15;
      int attempt = 0;
      while (this.account.State == Account.Result.Unchecked)
      {
        if (attempt > maxAttempts)
          break;
        try
        {
          await this.Login(attempt);
        }
        catch
        {
          if (attempt >= maxAttempts)
          {
            this.account.ErrorMessage = string.Format("failed to connect after {0} attempts", (object) maxAttempts);
            this.account.State = Account.Result.Error;
          }
        }
        ++attempt;
      }
    }

    public async Task Login(int attempt)
    {
      try
      {
        RegionData regiondata = AuthClass.ReadSystemRegionData(Path.Combine(Utils.MyLocation, "system.yaml"), this.account.Region.ToUpper());
        if (!regiondata.SuccessRead)
          throw new InvalidDataException("Invalid Region.");
        RiotAuthToken login = AuthClass.GetLoginToken(this.account.Username, this.account.Password, regiondata, Settings.Config.Rso);
        if (login.Result == RiotAuthResult.Success)
        {
          StoreInfo storeData = CheckerCore.GetStoreData(login, regiondata);
          if (storeData.player.summonerLevel == -1)
            throw new InvalidDataException(string.Format("Attempt {0} Summoner not created", (object) attempt));
          this.account.IpBalance = storeData.player.ip;
          this.account.RpBalance = storeData.player.rp;
          this.account.AccountId = storeData.player.accountId;
          this.account.Level = storeData.player.summonerLevel;
          await Task.WhenAll(this.GetChampData(login, regiondata), this.GetEmailData(login, regiondata), this.GetLastPlayData(login, regiondata)).ConfigureAwait(false);
          await this.GetStoreDataV(login, this.account, regiondata);
          this.account.State = Account.Result.Success;
        }
        else
        {
          if (login.Result == RiotAuthResult.InvalidCredentials)
            throw new InvalidDataException(string.Format("Attempt {0} | InvalidCredentials | proxy {1}", (object) attempt, (object) login.Dsid));
          if (login.Result == RiotAuthResult.TooManyReq)
            throw new InvalidDataException(string.Format("Attempt {0} | TooManyRequests | proxy {1}", (object) attempt, (object) login.Dsid));
          if (login.Result == RiotAuthResult.ConProblem)
            throw new InvalidDataException(string.Format("Attempt {0} | ConnectionProblem | proxy {1}", (object) attempt, (object) login.Dsid));
          if (login.Result == RiotAuthResult.BadProxy)
            throw new InvalidDataException(string.Format("Attempt {0} | BadProxy | proxy {1}", (object) attempt, (object) login.Dsid));
          if (login.Result == RiotAuthResult.UnknownReason)
            throw new InvalidDataException(string.Format("Attempt {0} | Unknown Reason | proxy {1}", (object) attempt, (object) login.Dsid));
        }
        regiondata = (RegionData) null;
        login = (RiotAuthToken) null;
      }
      catch (Exception ex)
      {
        this.HandleException(ex);
      }
    }

    public async Task GetStoreDataV(
      RiotAuthToken login,
      Account account,
      RegionData regionData)
    {
      UpdateStoreDataV(AuthClass.GetStoreInfoV1(login, account, regionData));

      void UpdateStoreDataV(string StoreData)
      {
      }
    }

    public async Task GetLastPlayData(RiotAuthToken login, RegionData regionData)
    {
      await UpdateLastPlayData(AuthClass.GetLastPlayInfo(login, regionData));

      async Task UpdateLastPlayData(long LastPlay)
      {
        this.account.LastPlay = Utils.UnixTimeStampToDateTime(LastPlay).ToString("MM.dd.yyyy");
        this.account.LastPlayLong = LastPlay;
      }
    }

    public async Task GetEmailData(RiotAuthToken login, RegionData regionData)
    {
      await UpdateEmailData(AuthClass.GetEmailInfo(login, regionData));

      async Task UpdateEmailData(EmailInfo EmailData)
      {
        this.account.EmailStatus = EmailData.emailVerified;
      }
    }

    public static StoreInfo GetStoreData(RiotAuthToken login, RegionData regionData)
    {
      return AuthClass.GetStoreIpRpLvl(login, regionData);
    }

    public async Task GetChampData(RiotAuthToken login, RegionData regionData)
    {
      await UpdateChampData(AuthClass.GetChampionJwt(login, regionData));

      async Task UpdateChampData(ChampionJwt ChampData)
      {
        this.account.ChampionCount = ChampData.Items.Champion.Length;
        this.account.SkinCount = ChampData.Items.ChampionSkin.Length;
      }
    }

    private void HandleException(Exception e)
    {
      if (e is InvalidDataException)
      {
        this.account.ErrorMessage = e.Message;
        if (e.Message.Contains("TooManyRequests") || e.Message.Contains("ConnectionProblem") || e.Message.Contains("BadProxy"))
        {
          this.account.UncheckedMessage = "Unchecked (rechecking) | " + e.Message;
          this.account.State = Account.Result.Unchecked;
        }
        else
          this.account.State = Account.Result.Error;
      }
      else
      {
        this.account.ErrorMessage = "Exception found: " + e.Message;
        Utils.ExportException(e);
        throw e;
      }
    }
  }
}
