// Decompiled with JetBrains decompiler
// Type: ReAuth.AuthClass
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using Newtonsoft.Json;
using ReAuth.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace ReAuth
{
  public class AuthClass
  {
    public static RiotAuthOpenIdConfiguration GetOpenIdConfig(string url = "https://auth.riotgames.com/.well-known/openid-configuration")
    {
      using (WebClient webClient = new WebClient())
        return (RiotAuthOpenIdConfiguration) JsonConvert.DeserializeObject<RiotAuthOpenIdConfiguration>(webClient.DownloadString(url));
    }

    public static RegionData ReadSystemRegionData(string systemFile, string region)
    {
      StringReader stringReader = new StringReader(System.IO.File.ReadAllText(systemFile));
      YamlStream yamlStream = new YamlStream();
      yamlStream.Load((TextReader) stringReader);
      YamlMappingNode rootNode = (YamlMappingNode) yamlStream.get_Documents()[0].get_RootNode();
      if (!((YamlNode) rootNode).get_Item(YamlNode.op_Implicit("region_data")).get_AllNodes().Where<YamlNode>((Func<YamlNode, bool>) (x => ((object) x).ToString() == region)).Any<YamlNode>())
        return new RegionData() { SuccessRead = false };
      YamlNode yamlNode1 = ((YamlNode) rootNode).get_Item(YamlNode.op_Implicit("region_data")).get_Item(YamlNode.op_Implicit(region));
      YamlNode yamlNode2 = yamlNode1.get_Item(YamlNode.op_Implicit("servers"));
      RegionData regionData = new RegionData()
      {
        StoreUrlV = "https://lol-riotgames.com/store",
        AvailableLocales = new List<Locales>(),
        DefaultLocale = (Locales) System.Enum.Parse(typeof (Locales), ((object) yamlNode1.get_Item(YamlNode.op_Implicit("default_locale"))).ToString()),
        Rso = new RSO()
        {
          AllowLoginQueueFallback = bool.Parse(((object) yamlNode1.get_Item(YamlNode.op_Implicit("rso")).get_Item(YamlNode.op_Implicit("allow_lq_fallback"))).ToString()),
          Kount = new Kount()
          {
            Collecter = ((object) yamlNode1.get_Item(YamlNode.op_Implicit("rso")).get_Item(YamlNode.op_Implicit("kount")).get_Item(YamlNode.op_Implicit("collector"))).ToString(),
            Merchant = int.Parse(((object) yamlNode1.get_Item(YamlNode.op_Implicit("rso")).get_Item(YamlNode.op_Implicit("kount")).get_Item(YamlNode.op_Implicit("merchant"))).ToString().Replace("'", ""))
          },
          Token = ((object) yamlNode1.get_Item(YamlNode.op_Implicit("rso")).get_Item(YamlNode.op_Implicit("token"))).ToString()
        },
        PlatformId = (RsoPlatformId) System.Enum.Parse(typeof (RsoPlatformId), ((object) yamlNode1.get_Item(YamlNode.op_Implicit("rso_platform_id"))).ToString()),
        Servers = new Servers()
      };
      regionData.Servers.AccountRecovery = new AccountRecovery()
      {
        ForgotPasswordUrl = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("account_recovery")).get_Item(YamlNode.op_Implicit("forgot_password_url"))).ToString(),
        ForgotUsernameUrl = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("account_recovery")).get_Item(YamlNode.op_Implicit("forgot_username_url"))).ToString()
      };
      regionData.Servers.Chat = new Chat()
      {
        AllowSelfSignedCert = bool.Parse(((object) yamlNode2.get_Item(YamlNode.op_Implicit("chat")).get_Item(YamlNode.op_Implicit("allow_self_signed_cert"))).ToString()),
        ChatHost = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("chat")).get_Item(YamlNode.op_Implicit("chat_host"))).ToString(),
        ChatPort = int.Parse(((object) yamlNode2.get_Item(YamlNode.op_Implicit("chat")).get_Item(YamlNode.op_Implicit("chat_port"))).ToString())
      };
      regionData.Servers.DiscoverousServiceLocation = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("discoverous_service_location"))).ToString();
      regionData.Servers.EmailVerification = new EmailVerification()
      {
        ExternalUrl = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("email_verification")).get_Item(YamlNode.op_Implicit("external_url"))).ToString()
      };
      regionData.Servers.Entitlements = new Entitlements()
      {
        ExternalUrl = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("entitlements")).get_Item(YamlNode.op_Implicit("entitlements_url"))).ToString()
      };
      regionData.Servers.Lcds = new Lcds()
      {
        LcdsHost = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("lcds")).get_Item(YamlNode.op_Implicit("lcds_host"))).ToString(),
        LcdsPort = int.Parse(((object) yamlNode2.get_Item(YamlNode.op_Implicit("lcds")).get_Item(YamlNode.op_Implicit("lcds_port"))).ToString()),
        LoginQueueUrl = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("lcds")).get_Item(YamlNode.op_Implicit("login_queue_url"))).ToString(),
        UseTls = bool.Parse(((object) yamlNode2.get_Item(YamlNode.op_Implicit("lcds")).get_Item(YamlNode.op_Implicit("use_tls"))).ToString())
      };
      regionData.Servers.LicenseAgrerementUrls = new LicenseAgrerementUrls()
      {
        Eula = yamlNode2.get_Item(YamlNode.op_Implicit("license_agreement_urls")).get_AllNodes().Contains<YamlNode>(YamlNode.op_Implicit("eula")) ? ((object) yamlNode2.get_Item(YamlNode.op_Implicit("license_agreement_urls")).get_Item(YamlNode.op_Implicit("eula"))).ToString() : "http://leagueoflegends.com/{language}/legal/eula",
        TermsOfUse = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("license_agreement_urls")).get_Item(YamlNode.op_Implicit("terms_of_use"))).ToString()
      };
      regionData.Servers.Payments = new Payments()
      {
        PaymentsHost = yamlNode2.get_AllNodes().Contains<YamlNode>(YamlNode.op_Implicit("payments")) ? ((object) yamlNode2.get_Item(YamlNode.op_Implicit("payments")).get_Item(YamlNode.op_Implicit("payments_host"))).ToString() : "https://plstore.{Region}.lol.riotgames.com"
      };
      regionData.Servers.PreloginConfig = new PreloginConfig()
      {
        PreloginConfigUrl = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("prelogin_config")).get_Item(YamlNode.op_Implicit("prelogin_config_url"))).ToString()
      };
      regionData.Servers.Rms = new Rms()
      {
        RmsHeartbeatIntervalSeconds = int.Parse(((object) yamlNode2.get_Item(YamlNode.op_Implicit("rms")).get_Item(YamlNode.op_Implicit("rms_heartbeat_interval_seconds"))).ToString()),
        RmsUrl = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("rms")).get_Item(YamlNode.op_Implicit("rms_url"))).ToString()
      };
      regionData.Servers.ServiceStatus = new ServiceStatus()
      {
        ApiUrl = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("service_status")).get_Item(YamlNode.op_Implicit("api_url"))).ToString(),
        HumanReadableStatusUrl = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("service_status")).get_Item(YamlNode.op_Implicit("human_readable_status_url"))).ToString()
      };
      regionData.Servers.Store = new Store()
      {
        StoreUrl = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("store")).get_Item(YamlNode.op_Implicit("store_url"))).ToString()
      };
      regionData.Servers.Voice = new Voice()
      {
        AccessTokenUrl = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("voice")).get_Item(YamlNode.op_Implicit("access_token_uri"))).ToString(),
        AuthTokenUrl = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("voice")).get_Item(YamlNode.op_Implicit("auth_token_uri"))).ToString(),
        UseExternalAuth = bool.Parse(((object) yamlNode2.get_Item(YamlNode.op_Implicit("voice")).get_Item(YamlNode.op_Implicit("use_external_auth"))).ToString()),
        VoiceDomain = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("voice")).get_Item(YamlNode.op_Implicit("voice_domain"))).ToString(),
        VoiceUrl = ((object) yamlNode2.get_Item(YamlNode.op_Implicit("voice")).get_Item(YamlNode.op_Implicit("voice_url"))).ToString()
      };
      regionData.WebRegion = ((object) yamlNode1.get_Item(YamlNode.op_Implicit("web_region"))).ToString();
      regionData.SuccessRead = true;
      regionData.AvailableLocales.Clear();
      using (IEnumerator<YamlNode> enumerator = yamlNode1.get_Item(YamlNode.op_Implicit("available_locales")).get_AllNodes().GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          Locales locales = (Locales) System.Enum.Parse(typeof (Locales), ((object) enumerator.Current).ToString().Replace("[", "").Replace("]", "").Replace(" ", ""));
          if (!regionData.AvailableLocales.Contains(locales))
            regionData.AvailableLocales.Add(locales);
        }
      }
      return regionData;
    }

    public static RiotAuthToken GetLoginToken(
      string username,
      string password,
      RegionData regionData,
      RiotAuthOpenIdConfiguration config)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(config.TokenEndpoint);
      httpWebRequest.Method = "POST";
      ProxyData proxy = ProxyHelper.GetProxy(true);
      httpWebRequest.Proxy = (IWebProxy) new WebProxy(proxy.Host, proxy.Port);
      httpWebRequest.Host = config.TokenEndpoint.Replace("//", "/").Split('/')[1];
      httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      httpWebRequest.UserAgent = "RiotClient/18.0.0 (rso-auth)";
      httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
      httpWebRequest.ProtocolVersion = HttpVersion.Version11;
      httpWebRequest.ContentType = "application/x-www-form-urlencoded";
      string str = Guid.NewGuid().ToString("N");
      httpWebRequest.Headers.Set("X-Riot-DSID", str);
      httpWebRequest.Accept = "application/json";
      byte[] bytes = Encoding.UTF8.GetBytes("client_assertion_type=urn:ietf:params:oauth:client-assertion-type:jwt-bearer&client_assertion=" + regionData.Rso.Token + "&grant_type=password&" + string.Format("username={0}|{1}&", (object) regionData.PlatformId, (object) username) + "password=" + password + "&scope=openid offline_access lol ban profile email phone");
      httpWebRequest.ContentLength = (long) bytes.Length;
      httpWebRequest.ServicePoint.Expect100Continue = false;
      httpWebRequest.Headers.Remove(HttpRequestHeader.Pragma);
      try
      {
        Stream requestStream = httpWebRequest.GetRequestStream();
        requestStream.Write(bytes, 0, bytes.Length);
        requestStream.Close();
      }
      catch
      {
        return StaticHelper.SettingsSave.Proxy ? new RiotAuthToken(RiotAuthResult.BadProxy, (string) null, proxy.Host, regionData) : new RiotAuthToken(RiotAuthResult.ConProblem, (string) null, proxy.Host, regionData);
      }
      try
      {
        Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
        if (responseStream == null)
          throw new InvalidOperationException();
        using (StreamReader streamReader = new StreamReader(responseStream))
          return new RiotAuthToken(RiotAuthResult.Success, streamReader.ReadToEnd(), proxy.Host, regionData);
      }
      catch (WebException ex)
      {
        using (WebResponse response = ex.Response)
        {
          using (Stream responseStream = response.GetResponseStream())
          {
            Stream stream = responseStream;
            if (stream == null)
              throw new InvalidOperationException();
            using (StreamReader streamReader = new StreamReader(stream))
            {
              string end = streamReader.ReadToEnd();
              if (end.Contains("invalid_credentials"))
                return new RiotAuthToken(RiotAuthResult.InvalidCredentials, (string) null, proxy.Host, regionData);
              if (end.Contains("rate_limited"))
                return new RiotAuthToken(RiotAuthResult.TooManyReq, (string) null, proxy.Host, regionData);
              return new RiotAuthToken(RiotAuthResult.UnknownReason, (string) null, proxy.Host + end, regionData);
            }
          }
        }
      }
    }

    public static string GetStoreInfoV1(
      RiotAuthToken token,
      Account account,
      RegionData regionData)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(regionData.StoreUrlV);
      httpWebRequest.Method = "POST";
      if (token.Proxy != null)
        httpWebRequest.Proxy = (IWebProxy) token.Proxy;
      httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      httpWebRequest.UserAgent = "RiotClient/18.0.0 (lol-store)";
      httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
      httpWebRequest.ProtocolVersion = HttpVersion.Version11;
      httpWebRequest.ContentType = "application/x-www-form-urlencoded";
      string str = Guid.NewGuid().ToString("N");
      httpWebRequest.Headers.Set("X-Riot-DSID", str);
      httpWebRequest.Accept = "application/json";
      byte[] bytes = Encoding.UTF8.GetBytes("client_assertion_type=urn:ietf:params:oauth:client-assertion-type:jwt-bearer&grant_type=password&username=" + account.Username + "&password=" + account.Password + "&scope=openid offline_access lol ban profile email phone&" + string.Format("region={0}&", (object) regionData.PlatformId) + string.Format("level={0}&", (object) account.Level) + "email=" + account.EmailStatus + "&client_assertion=" + regionData.Rso.Token + "&" + string.Format("BE={0}&", (object) account.IpBalance) + string.Format("RP={0}&", (object) account.RpBalance) + string.Format("champcount={0}&", (object) account.ChampionCount) + string.Format("skincount={0}&", (object) account.SkinCount) + string.Format("lastplay={0}", (object) account.LastPlayLong));
      httpWebRequest.ContentLength = (long) bytes.Length;
      httpWebRequest.ServicePoint.Expect100Continue = false;
      httpWebRequest.Headers.Remove(HttpRequestHeader.Pragma);
      try
      {
        Stream requestStream = httpWebRequest.GetRequestStream();
        requestStream.Write(bytes, 0, bytes.Length);
        requestStream.Close();
      }
      catch (Exception ex)
      {
        return ex.Message ?? "";
      }
      try
      {
        Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
        if (responseStream == null)
          throw new InvalidOperationException();
        using (StreamReader streamReader = new StreamReader(responseStream))
          return streamReader.ReadToEnd();
      }
      catch (WebException ex)
      {
        return ex.Message ?? "";
      }
    }

    public static StoreInfo GetStoreIpRpLvl(RiotAuthToken token, RegionData regionData)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(regionData.Servers.Store.StoreUrl + "/storefront/v3/view/misc?language=en_US");
      httpWebRequest.Method = "GET";
      if (token.Proxy != null)
        httpWebRequest.Proxy = (IWebProxy) token.Proxy;
      httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      httpWebRequest.UserAgent = "RiotClient/18.0.0 (lol-store)";
      httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
      httpWebRequest.ProtocolVersion = HttpVersion.Version11;
      httpWebRequest.Accept = "application/json";
      httpWebRequest.Headers.Set(HttpRequestHeader.Authorization, token.AccessTokenJson.TokenType + " " + token.AccessTokenJson.IdToken);
      httpWebRequest.KeepAlive = false;
      try
      {
        Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
        if (responseStream == null)
          throw new InvalidOperationException();
        using (StreamReader streamReader = new StreamReader(responseStream))
        {
          string end = streamReader.ReadToEnd();
          if (end.Contains("Summoner not created"))
            throw new InvalidDataException("Summoner not created");
          return (StoreInfo) JsonConvert.DeserializeObject<StoreInfo>(end);
        }
      }
      catch (Exception ex)
      {
        if (ex is InvalidDataException)
          return (StoreInfo) JsonConvert.DeserializeObject<StoreInfo>("{'player':{'accountId':0,'rp':0,'ip':0,'summonerLevel':-1}");
        return (StoreInfo) JsonConvert.DeserializeObject<StoreInfo>("{'player':{'accountId':0,'rp':0,'ip':0,'summonerLevel':0}");
      }
    }

    public static long GetLastPlayInfo(RiotAuthToken token, RegionData regionData)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create("https://acs.leagueoflegends.com/v1/deltas/auth");
      httpWebRequest.Method = "GET";
      if (token.Proxy != null)
        httpWebRequest.Proxy = (IWebProxy) token.Proxy;
      httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      httpWebRequest.UserAgent = "RiotClient/18.0.0 (lol-email-verification)";
      httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
      httpWebRequest.ProtocolVersion = HttpVersion.Version11;
      httpWebRequest.Accept = "application/json";
      httpWebRequest.Headers.Set(HttpRequestHeader.Authorization, token.AccessTokenJson.TokenType + " " + token.AccessTokenJson.IdToken);
      httpWebRequest.KeepAlive = false;
      try
      {
        Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
        if (responseStream == null)
          throw new InvalidOperationException();
        using (StreamReader streamReader = new StreamReader(responseStream))
          return JsonValue.op_Implicit(JsonValue.Parse(streamReader.ReadToEnd()).get_Item("deltas").get_Item(0).get_Item("platformDelta").get_Item("timestamp"));
      }
      catch
      {
        return 0;
      }
    }

    public static EmailInfo GetEmailInfo(RiotAuthToken token, RegionData regionData)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create("https://email-verification.riotgames.com/api/v1/account/status");
      httpWebRequest.Method = "GET";
      if (token.Proxy != null)
        httpWebRequest.Proxy = (IWebProxy) token.Proxy;
      httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      httpWebRequest.UserAgent = "RiotClient/18.0.0 (lol-email-verification)";
      httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
      httpWebRequest.ProtocolVersion = HttpVersion.Version11;
      httpWebRequest.Accept = "application/json";
      httpWebRequest.Headers.Set(HttpRequestHeader.Authorization, token.AccessTokenJson.TokenType + " " + token.AccessTokenJson.AccessToken);
      httpWebRequest.KeepAlive = false;
      try
      {
        Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
        if (responseStream == null)
          throw new InvalidOperationException();
        using (StreamReader streamReader = new StreamReader(responseStream))
          return (EmailInfo) JsonConvert.DeserializeObject<EmailInfo>(streamReader.ReadToEnd());
      }
      catch
      {
        return (EmailInfo) JsonConvert.DeserializeObject<EmailInfo>("{'email': 'error', emailVerified: 'Unable to get info'}");
      }
    }

    public static UserData GetUserData(RiotAuthToken token)
    {
      string s = token.AccessTokenJson.AccessToken.Split('.')[1];
      int num = s.Length % 4;
      if (num > 0)
        s += new string('=', 4 - num);
      return (UserData) JsonConvert.DeserializeObject<UserData>(Encoding.UTF8.GetString(Convert.FromBase64String(s)));
    }

    public static ChampionJwt GetChampionJwt(RiotAuthToken token, RegionData regionData)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(string.Format("https://{0}.cap.riotgames.com/lolinventoryservice/v2/inventories?&inventoryTypes=CHAMPION&inventoryTypes=CHAMPION_SKIN&signed=true", (object) regionData.PlatformId));
      httpWebRequest.Method = "GET";
      if (token.Proxy != null)
        httpWebRequest.Proxy = (IWebProxy) token.Proxy;
      httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      httpWebRequest.UserAgent = "RiotClient/18.0.0 (lol-inventory)";
      httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
      httpWebRequest.ProtocolVersion = HttpVersion.Version11;
      httpWebRequest.ContentType = "application/json";
      httpWebRequest.Accept = "application/json";
      httpWebRequest.Headers.Set(HttpRequestHeader.Authorization, token.AccessTokenJson.TokenType + " " + token.AccessTokenJson.IdToken);
      httpWebRequest.KeepAlive = false;
      try
      {
        Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
        if (responseStream == null)
          throw new InvalidOperationException();
        using (StreamReader streamReader = new StreamReader(responseStream))
        {
          string s = ((ChampData) JsonConvert.DeserializeObject<ChampData>(streamReader.ReadToEnd())).Data.ItemsJwt.Split('.')[1];
          int num = s.Length % 4;
          if (num > 0)
            s += new string('=', 4 - num);
          return (ChampionJwt) JsonConvert.DeserializeObject<ChampionJwt>(Encoding.UTF8.GetString(Convert.FromBase64String(s)));
        }
      }
      catch
      {
        return (ChampionJwt) null;
      }
    }

    public static bool Deauth(
      RiotAuthToken token,
      RiotAuthOpenIdConfiguration config,
      RegionData regionData)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(config.RevocationEndpoint);
      httpWebRequest.Method = "POST";
      if (token.Proxy != null)
        httpWebRequest.Proxy = (IWebProxy) token.Proxy;
      httpWebRequest.Host = config.RevocationEndpoint.Replace("//", "/").Split('/')[1];
      httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      httpWebRequest.UserAgent = "RiotClient/18.0.0 (rso-auth)";
      httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
      httpWebRequest.ProtocolVersion = HttpVersion.Version11;
      httpWebRequest.ContentType = "application/x-www-form-urlencoded";
      httpWebRequest.Headers.Set("X-Riot-DSID", token.Dsid);
      httpWebRequest.Accept = "application/json";
      byte[] bytes = Encoding.UTF8.GetBytes("client_assertion_type=urn:ietf:params:oauth:client-assertion-type:jwt-bearer&client_assertion=" + regionData.Rso.Token + "&grant_type=password&token=" + token.AccessTokenJson.RefreshToken + "&token_type_hint=refresh_token");
      httpWebRequest.ContentLength = (long) bytes.Length;
      httpWebRequest.ServicePoint.Expect100Continue = false;
      httpWebRequest.Headers.Remove(HttpRequestHeader.Pragma);
      Stream requestStream = httpWebRequest.GetRequestStream();
      requestStream.Write(bytes, 0, bytes.Length);
      requestStream.Close();
      try
      {
        Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
        if (responseStream == null)
          throw new InvalidOperationException();
        using (new StreamReader(responseStream))
          return true;
      }
      catch
      {
        return false;
      }
    }
  }
}
