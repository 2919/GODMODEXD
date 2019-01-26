// Decompiled with JetBrains decompiler
// Type: ReAuth.Champion
// Assembly: ReAuth, Version=1.0.6953.41762, Culture=neutral, PublicKeyToken=null
// MVID: 30CD6CC8-2A35-4496-A933-8D554CB42274
// Assembly location: C:\Users\shawtware\Desktop\ReAuth.exe

using Newtonsoft.Json;

namespace ReAuth
{
  public class Champion
  {
    [JsonProperty("itemId")]
    public long ItemId { get; set; }

    [JsonProperty("inventoryType")]
    public string InventoryType { get; set; }

    [JsonProperty("expirationDate")]
    public object ExpirationDate { get; set; }

    [JsonProperty("purchaseDate")]
    public string PurchaseDate { get; set; }

    [JsonProperty("quantity")]
    public long Quantity { get; set; }

    [JsonProperty("ownedQuantity")]
    public long OwnedQuantity { get; set; }

    [JsonProperty("usedInGameDate")]
    public string UsedInGameDate { get; set; }

    [JsonProperty("entitlementId")]
    public object EntitlementId { get; set; }

    [JsonProperty("entitlementTypeId")]
    public string EntitlementTypeId { get; set; }

    [JsonProperty("instanceId")]
    public object InstanceId { get; set; }

    [JsonProperty("instanceTypeId")]
    public string InstanceTypeId { get; set; }

    [JsonProperty("payload")]
    public object Payload { get; set; }

    [JsonProperty("f2p")]
    public bool F2P { get; set; }

    [JsonProperty("rental")]
    public bool Rental { get; set; }

    [JsonProperty("loyalty")]
    public bool Loyalty { get; set; }

    [JsonProperty("wins")]
    public object Wins { get; set; }
  }
}
