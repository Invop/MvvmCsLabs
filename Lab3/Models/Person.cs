using Shared;

namespace Lab3.Models;

public class Person
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string _email;
    private readonly DateTime _birthDate;

    private readonly bool _isAdult;
    private readonly string _sunSign;
    private readonly string _chineseSign;
    private readonly bool _isBirthday;

    public string FirstName => _firstName;
    public string LastName => _lastName;
    public string Email => _email;
    public DateTime BirthDate => _birthDate;

    public bool IsAdult => _isAdult;
    public string SunSign => _sunSign;
    public string ChineseSign => _chineseSign;
    public bool IsBirthday => _isBirthday;

    public Person(string firstName, string lastName, string email, DateTime birthDate)
    {
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _birthDate = birthDate;

        _isAdult = CalculateIsAdult();
        _sunSign = Zodiac.GetWesternZodiac(birthDate);
        _chineseSign = Zodiac.GetChineseZodiac(birthDate);
        _isBirthday = CalculateIsBirthday();
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