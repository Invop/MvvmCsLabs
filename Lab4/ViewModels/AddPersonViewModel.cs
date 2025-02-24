using CommunityToolkit.Mvvm.ComponentModel;
using Lab4.Models;
using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.Input;
using Lab4.Attributes;
using Lab4.Data;
using Lab4.ViewModels.Base;

namespace Lab4.ViewModels;

public partial class AddPersonViewModel : DialogViewModelBase
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "First name is required")]
    [MinLength(2, ErrorMessage = "First name must be at least 2 characters long")]
    private string _firstName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Last name is required")]
    [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long")]
    private string _lastName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Email is required")]
    [CustomValidation(typeof(Shared.RegexUtilities), "ValidateEmail")]
    private string _email = string.Empty;

    [ObservableProperty] [NotifyDataErrorInfo] [Required(ErrorMessage = "Birth date is required")] [BirthDateValidation]
    private DateTime _birthDate = DateTime.Now;

    private readonly AppDbContext _dbContext;

    public AddPersonViewModel(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Save()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            ShowErrors();
            return;
        }

        var person = new Person(FirstName, LastName, Email, BirthDate);

        _dbContext.People.Add(person);
        _dbContext.SaveChanges();

        base.Save();
    }
}