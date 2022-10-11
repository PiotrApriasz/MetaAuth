namespace MetaAuth.Core.Entities.User;

public class RegisteredApp
{
    public string UserId { get; set; }
    public string WebAppAddress { get; set; }
    public DateTime RegisterTime { get; set; }
    public List<string> RequiredUserData { get; set; }
}