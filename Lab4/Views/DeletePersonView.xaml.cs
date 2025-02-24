using System.ComponentModel;
using System.Windows;
using Lab4.ViewModels.Base;

namespace Lab4.Views;

public partial class DeletePersonView : Window
{
    public DeletePersonView()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.OldValue is DialogViewModelBase oldViewModel) oldViewModel.PropertyChanged -= OnViewModelPropertyChanged;

        if (e.NewValue is DialogViewModelBase newViewModel) newViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(DialogViewModelBase.DialogResult)
            && sender is DialogViewModelBase viewModel)
            DialogResult = viewModel.DialogResult;
    }
}