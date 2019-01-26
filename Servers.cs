// Decompiled with JetBrains decompiler
// Type: ReAuth.Servers
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

namespace ReAuth
{
  public class Servers
  {
    public AccountRecovery AccountRecovery { get; set; }

    public Chat Chat { get; set; }

    public string DiscoverousServiceLocation { get; set; }

    public EmailVerification EmailVerification { get; set; }

    public Entitlements Entitlements { get; set; }

    public Lcds Lcds { get; set; }

    public LicenseAgrerementUrls LicenseAgrerementUrls { get; set; }

    public Payments Payments { get; set; }

    public PreloginConfig PreloginConfig { get; set; }

    public Rms Rms { get; set; }

    public ServiceStatus ServiceStatus { get; set; }

    public Store Store { get; set; }

    public Voice Voice { get; set; }
  }
}
