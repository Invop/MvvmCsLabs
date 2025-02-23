using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared;

namespace Lab1.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private DateTime? _birthDate;

    [ObservableProperty]
    private string _age = string.Empty;

    [ObservableProperty]
    private string _westernZodiac = string.Empty;

    [ObservableProperty]
    private string _chineseZodiac = string.Empty;

    [RelayCommand]
    private void Calculate()
    {
        if (!BirthDate.HasValue)
        {
            MessageBox.Show("Будь ласка, виберіть дату народження", "Помилка");
            return;
        }

        var today = DateTime.Today;
        var age = today.Year - BirthDate.Value.Year;
        
        if (BirthDate.Value.Date > today.AddYears(-age))
            age--;

        if (age < 0)
        {
            MessageBox.Show("Дата народження не може бути в майбутньому", "Помилка");
            return;
        }
        if (age > 135)
        {
            MessageBox.Show("Вік не може бути більше 135 років", "Помилка");
            return;
        }

        Age = $"Вік: {age} років";

        if (BirthDate.Value.Month == today.Month && BirthDate.Value.Day == today.Day)
        {
            MessageBox.Show("З днем народження!", "Вітаємо");
        }

        WesternZodiac = $"Західний знак: {Zodiac.GetWesternZodiac(BirthDate)}";
        ChineseZodiac = $"Китайський знак: {Zodiac.GetChineseZodiac(BirthDate)}";
    }
}