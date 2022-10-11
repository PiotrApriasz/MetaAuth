using Newtonsoft.Json;

namespace MetaAuth.Core.Entities.User;

public class MetaAuthUserData
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<RegisteredApp> RegisteredApps { get; set; }
    public DateTime? BirthDate { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
    public IdCard IdCard { get; set; }
    public long IssueTime { get; set; }
    public string Base64PhotoCode { get; set; }

    public MetaAuthUserData()
    {
        Address = new Address();
        IdCard = new IdCard();
        RegisteredApps = new List<RegisteredApp>();
    }
}