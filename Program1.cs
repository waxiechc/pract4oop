using System;
using System.IO;
using System.Xml.Serialization;

public class Company
{
    public string Name { get; set; } = "Невизначений";

    public Company() { }
    public Company(string name)
    {
        Name = name;
    }
}

public class Person
{
    public string Name { get; set; } = "Невизначений";
    public int Age { get; set; } = 1;
    public Company Company { get; set; } = new Company();

    public Person() { }

    public Person(string name, int age, Company company)
    {
        Name = name;
        Age = age;
        Company = company;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Серіалізація
        var microsoft = new Company("Microsoft");
        var google = new Company("Google");

        Person[] people = new Person[]
        {
            new Person("Tom", 37, microsoft),
            new Person("Bob", 41, google)
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
                    Console.WriteLine($"Name: {person.Name}, Age: {person.Age}, Company: {person.Company.Name}");
                }
            }
        }
    }
}