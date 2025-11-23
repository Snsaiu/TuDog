namespace Demo.Models;

public abstract class FindFatherBase
{
    public string Name { get; set; }
}

public class Children : FindFatherBase
{
    public string Age { get; set; }
}

public class Dog : FindFatherBase
{
    public string Color { get; set; }
}