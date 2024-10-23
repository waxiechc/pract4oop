using System;
using System.IO;
using System.Xml.Serialization;

public class Company
{
    public string Name { get; set; } = "Невизначено";
    public string Industry { get; set; } = "Невідомо";
    public int NumberOfEmployees { get; set; } = 0;
    public string Country { get; set; } = "Невідомо";

    public Company() { }
    public Company(string name, string industry, int numberOfEmployees, string country)
    {
        Name = name;
        Industry = industry;
        NumberOfEmployees = numberOfEmployees;
        Country = country;
    }
}

public class Person
{
    public string Name { get; set; } = "Невизначено";
    public int Age { get; set; } = 1;
    public string Gender { get; set; } = "Невідомо";
    public string Position { get; set; } = "Співробітник";
    public Company Company { get; set; } = new Company();
    public string Email { get; set; } = "noemail@unknown.com";

    public Person() { }

    public Person(string name, int age, string gender, string position, Company company, string email)
    {
        Name = name;
        Age = age;
        Gender = gender;
        Position = position;
        Company = company;
        Email = email;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Серіалізація
        var microsoft = new Company("Microsoft", "Software", 163000, "USA");
        var google = new Company("Google", "Technology", 135301, "USA");

        Person[] people = new Person[]
        {
            new Person("Tom", 37, "Male", "Software Engineer", microsoft, "tom@microsoft.com"),
            new Person("Bob", 41, "Male", "Product Manager", google, "bob@google.com"),
            new Person("Alice", 30, "Female", "Designer", microsoft, "alice@microsoft.com")
        };

        XmlSerializer formatter = new XmlSerializer(typeof(Person[]));

        using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, people);
        }

        Console.WriteLine("Об'єкти були серiалiзованi у people.xml");

        // Десеріалізація
        using (FileStream fs = new FileStream("people.xml", FileMode.Open))
        {
            Person[]? newPeople = formatter.Deserialize(fs) as Person[];

            if (newPeople != null)
            {
                Console.WriteLine("\nДесерiалiзованi данi:");
                foreach (Person person in newPeople)
                {
                    Console.WriteLine($"Name: {person.Name}, Age: {person.Age}, Gender: {person.Gender}, Position: {person.Position}");
                    Console.WriteLine($"Company: {person.Company.Name}, Industry: {person.Company.Industry}, Employees: {person.Company.NumberOfEmployees}, Country: {person.Company.Country}");
                    Console.WriteLine($"Email: {person.Email}\n");
                }
            }
        }
    }
}
