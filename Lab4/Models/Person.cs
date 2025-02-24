using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Shared;

namespace Lab4.Models;

public class Person
{
    [Key] public int Id { get; init; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsAdult => CalculateIsAdult();
    public string SunSign => Zodiac.GetWesternZodiac(BirthDate);
    public string ChineseSign => Zodiac.GetChineseZodiac(BirthDate);
    public bool IsBirthday => CalculateIsBirthday();

    public Person()
    {
    }

    public Person(string firstName, string lastName, string email, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        BirthDate = birthDate;
    }

    public Person(string firstName, string lastName, string email)
        : this(firstName, lastName, email, DateTime.Today)
    {
    }

    public Person(string firstName, string lastName, DateTime birthDate)
        : this(firstName, lastName, string.Empty, birthDate)
    {
    }

    private bool CalculateIsAdult()
    {
        var today = DateTime.Today;
        var age = today.Year - BirthDate.Year;
        if (BirthDate.Date > today.AddYears(-age))
            age--;
        return age >= 18;
    }

    private bool CalculateIsBirthday()
    {
        var today = DateTime.Today;
        return BirthDate.Month == today.Month && BirthDate.Day == today.Day;
    }
}