namespace AnnuaireEntrepriseCESI.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string MobilePhone { get; set; } = null!;

    public int ServiceId { get; set; }

    public int SiteId { get; set; }

    public Site Site { get; set; }  
    public Service Service { get; set; }
}
