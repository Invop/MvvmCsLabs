using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab2.Models;

namespace Lab2.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private string _firstName = string.Empty;

    [ObservableProperty] private string _lastName = string.Empty;

    [ObservableProperty] private string _email = string.Empty;

    [ObservableProperty] private DateTime? _birthDate;

    [ObservableProperty] private Person? _person;

    [ObservableProperty] private bool _canProceed;
    [ObservableProperty] private bool _isProcessing;

    partial void OnFirstNameChanged(string value)
    {
        UpdateCanProceed();
    }

    partial void OnLastNameChanged(string value)
    {
        UpdateCanProceed();
    }

    partial void OnEmailChanged(string value)
    {
        UpdateCanProceed();
    }

    partial void OnBirthDateChanged(DateTime? value)
    {
        UpdateCanProceed();
    }

    private void UpdateCanProceed()
    {
        CanProceed = !string.IsNullOrWhiteSpace(FirstName) &&
                     !string.IsNullOrWhiteSpace(LastName) &&
                     !string.IsNullOrWhiteSpace(Email) &&
                     BirthDate.HasValue;
    }

    [RelayCommand]
    private async Task ProceedAsync()
    {
        try
        {
            IsProcessing = true;
            if (!Shared.RegexUtilities.IsValidEmail(Email))
            {
                MessageBox.Show("Невірний формат електронної пошти", "Помилка");
                return;
            }

            if (!BirthDate.HasValue)
                return;

            await ValidateBirthDateAsync(BirthDate.Value);
            Person = new Person(FirstName, LastName, Email, BirthDate.Value);
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Помилка");
        }
        finally
        {
            IsProcessing = false;
        }
    }

    private static Task ValidateBirthDateAsync(DateTime birthDate)
    {
        return Task.Run(() =>
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age))
                age--;

            switch (age)
            {
                case < 0:
                    throw new ArgumentException("Дата народження не може бути в майбутньому");
                case > 135:
                    throw new ArgumentException("Вік не може бути більше 135 років");
            }
        });
    }
}