using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Lab4.ViewModels.Base;

public partial class DialogViewModelBase : ObservableValidator
{
    [ObservableProperty] private bool? _dialogResult;

    [RelayCommand]
    public virtual void Close()
    {
        DialogResult = false;
    }

    [RelayCommand]
    public virtual void Save()
    {
        ValidateAllProperties();
        if (!HasErrors) DialogResult = true;
    }

    [RelayCommand]
    public void ShowErrors()
    {
        var message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
        MessageBox.Show(message, "Errors");
    }
}