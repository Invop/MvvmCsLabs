using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab3.Exceptions;
using Lab3.Models;

namespace Lab3.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private string _firstName = "John";
    [ObservableProperty] private string _lastName = "Doe";
    [ObservableProperty] private string _email = "example@example.com";
    [ObservableProperty] private DateTime? _birthDate = DateTime.Today;
    [ObservableProperty] private Person? _person;
    [ObservableProperty] private bool _isProcessing;

    private bool CanExecute()
    {
        return !string.IsNullOrWhiteSpace(FirstName) &&
               !string.IsNullOrWhiteSpace(LastName) &&
               !string.IsNullOrWhiteSpace(Email) &&
               BirthDate.HasValue;
    }

    [RelayCommand(CanExecute = "CanExecute")]
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