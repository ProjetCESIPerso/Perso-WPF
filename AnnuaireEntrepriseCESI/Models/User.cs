using System.Text.Json.Serialization;

namespace AnnuaireEntrepriseCESI.Models;

public partial class User
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("surname")]
    public string Surname { get; set; } = null!;

    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = null!;

    [JsonPropertyName("mobilePhone")]
    public string MobilePhone { get; set; } = null!;

    [JsonPropertyName("serviceId")]
    public int ServiceId { get; set; }

    [JsonPropertyName("siteId")]
    public int SiteId { get; set; }

    [JsonPropertyName("site")]
    public Site Site { get; set; }

    [JsonPropertyName("service")]
    public Service Service { get; set; }
}
