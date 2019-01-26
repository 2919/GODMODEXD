// Decompiled with JetBrains decompiler
// Type: ReAuth.AuthDataGrid
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

namespace ReAuth
{
  public class AuthDataGrid
  {
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;

    public string Verified { get; set; } = string.Empty;

    public string HiddenPassword
    {
      get
      {
        return new string('*', this.Password.Length);
      }
      set
      {
        this.Password = value;
      }
    }

    public string Runes { get; set; }

    public string RP { get; set; }

    public string IP { get; set; }

    public string Champs { get; set; }
  }
}
