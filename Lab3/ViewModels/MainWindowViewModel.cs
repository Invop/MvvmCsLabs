using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab3.Exceptions;
using Lab3.Models;

namespace Lab3.ViewModels;

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
            if (!Shared.RegexUtilities.IsValidEmail(Email)) throw new InvalidEmailException();

            if (!BirthDate.HasValue)
                return;

            await ValidateBirthDateAsync(BirthDate.Value);
            Person = new Person(FirstName, LastName, Email, BirthDate.Value);
        }
        catch (FutureBirthDateException ex)
        {
            MessageBox.Show(ex.Message, "Помилка дати", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (PastBirthDateException ex)
        {
            MessageBox.Show(ex.Message, "Помилка дати", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (InvalidEmailException ex)
        {
            MessageBox.Show(ex.Message, "Помилка email", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    throw new FutureBirthDateException();
                case > 135:
                    throw new PastBirthDateException();
            }
        });
    }
}