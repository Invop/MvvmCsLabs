using Bogus;
using Person = Lab4.Models.Person;

namespace Lab4.Data;

public static class Seeder
{
    public static void Seed(AppDbContext context, int count = 50)
    {
        if (context.People.Any()) return;
        var faker = new Faker<Person>()
            .CustomInstantiator(f => new Person(
                f.Name.FirstName(),
                f.Name.LastName(),
                f.Internet.Email(),
                f.Date.Past(80, DateTime.Now.AddYears(-18)) // Generate dates for people 18-80 years old
            ));

        var people = faker.Generate(count);

        context.People.AddRange(people);
        context.SaveChanges();
    }
}