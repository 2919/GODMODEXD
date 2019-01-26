// Decompiled with JetBrains decompiler
// Type: ReAuth.SpellBookPage
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using Newtonsoft.Json;

namespace ReAuth
{
  public class SpellBookPage
  {
    [JsonProperty("itemId")]
    public long ItemId { get; set; }

    [JsonProperty("inventoryType")]
    public string InventoryType { get; set; }

    [JsonProperty("ownedQuantity")]
    public long Quantity { get; set; }

    [JsonProperty("f2p")]
    public bool F2P { get; set; }

    [JsonProperty("rental")]
    public bool Rental { get; set; }

    [JsonProperty("lsb")]
    public bool Lsb { get; set; }
  }
}
