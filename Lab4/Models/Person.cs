using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Shared;

namespace Lab4.Models;

public class Person
{
    [Key] public int Id { get; init; }
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string _email;
    private readonly DateTime _birthDate;

    public string FirstName => _firstName;
    public string LastName => _lastName;
    public string Email => _email;
    public DateTime BirthDate => _birthDate;

    public bool IsAdult => CalculateIsAdult();
    public string SunSign => Zodiac.GetWesternZodiac(_birthDate);
    public string ChineseSign => Zodiac.GetChineseZodiac(_birthDate);
    public bool IsBirthday => CalculateIsBirthday();

    public Person()
    {
    }

    public Person(string firstName, string lastName, string email, DateTime birthDate)
    {
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _birthDate = birthDate;
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
        var age = today.Year - _birthDate.Year;
        if (_birthDate.Date > today.AddYears(-age))
            age--;
        return age >= 18;
    }

    private bool CalculateIsBirthday()
    {
        var today = DateTime.Today;
        return _birthDate.Month == today.Month && _birthDate.Day == today.Day;
    }
}