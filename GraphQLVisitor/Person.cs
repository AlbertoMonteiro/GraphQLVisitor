using System;

public class Person
{
    public string Identity { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime BirthDate { get; set; }
    public Product[] FavotireProducts { get; set; }
    public AuditData AuditData { get; set; }
}
