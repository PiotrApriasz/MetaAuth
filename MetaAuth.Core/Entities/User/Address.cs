namespace MetaAuth.Core.Entities.User;

public class Address
{
    public string Town { get; set; }
    public string Street { get; set; }
    public string HomeNumber { get; set; }
    public string? FlatNumber { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
}