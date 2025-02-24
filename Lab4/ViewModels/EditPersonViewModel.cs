using CommunityToolkit.Mvvm.ComponentModel;
using Lab4.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Lab4.Attributes;
using Lab4.Data;
using Lab4.ViewModels.Base;
using Microsoft.EntityFrameworkCore;

namespace Lab4.ViewModels;

public partial class EditPersonViewModel : DialogViewModelBase
{
    private Person? _person;
    private readonly AppDbContext _dbContext;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "First name is required")]
    [MinLength(2, ErrorMessage = "First name must be at least 2 characters long")]
    private string _firstName;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Last name is required")]
    [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long")]
    private string _lastName;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Email is required")]
    [CustomValidation(typeof(Shared.RegexUtilities), "ValidateEmail")]
    private string _email;

    [ObservableProperty] [NotifyDataErrorInfo] [Required(ErrorMessage = "Birth date is required")] [BirthDateValidation]
    private DateTime _birthDate;

    public EditPersonViewModel(Person person, AppDbContext dbContext)
    {
        _person = person;
        _dbContext = dbContext;

        FirstName = person.FirstName;
        LastName = person.LastName;
        Email = person.Email;
        BirthDate = person.BirthDate;
    }

    public override void Save()
    {
        if (HasErrors)
        {
            ShowErrors();
            return;
        }

        if (_person != null)
        {
            _person.FirstName = FirstName;
            _person.LastName = LastName;
            _person.Email = Email;
            _person.BirthDate = BirthDate;

            _dbContext.Update(_person);
        }

        _dbContext.SaveChanges();


        base.Save();
    }
}