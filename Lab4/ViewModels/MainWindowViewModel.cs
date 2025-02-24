using CommunityToolkit.Mvvm.ComponentModel;
using Lab4.Data;
using Lab4.Models;
using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Lab4.Views;

namespace Lab4.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    public ObservableCollection<Person?> People { get; set; }
    public ObservableCollection<Person?> FilteredPeople { get; set; }
    [ObservableProperty] private Person? _selectedPerson;
    [ObservableProperty] private string _nameFilter = string.Empty;
    [ObservableProperty] private string _lastNameFilter = string.Empty;
    [ObservableProperty] private string _emailFilter = string.Empty;
    [ObservableProperty] private int? _yearOfBirthFilter = null;

    private readonly AppDbContext _db = new();

    public MainWindowViewModel()
    {
        People = new ObservableCollection<Person?>(GetUsers());
        FilteredPeople = new ObservableCollection<Person?>(People);
        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName is nameof(NameFilter) or nameof(LastNameFilter)
                or nameof(EmailFilter) or nameof(YearOfBirthFilter))
                ApplyFilters();
        };
    }

    private void ApplyFilters()
    {
        var filteredResults = People.Where(person =>
            (string.IsNullOrEmpty(NameFilter) ||
             person.FirstName.Contains(NameFilter, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(LastNameFilter) ||
             person.LastName.Contains(LastNameFilter, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(EmailFilter) ||
             person.Email.Contains(EmailFilter, StringComparison.OrdinalIgnoreCase)) &&
            (!YearOfBirthFilter.HasValue || person.BirthDate.Year == YearOfBirthFilter)
        ).ToList();

        FilteredPeople.Clear();
        foreach (var person in filteredResults) FilteredPeople.Add(person);
    }

    public List<Person?> GetUsers()
    {
        return _db.People.ToList();
    }

    [RelayCommand]
    private void Add()
    {
        var viewModel = new AddPersonViewModel(_db);
        var window = new AddPersonView { DataContext = viewModel };
        window.Owner = Application.Current.MainWindow;

        if (window.ShowDialog() == true)
        {
            People = new ObservableCollection<Person?>(GetUsers());
            ApplyFilters();
        }
    }

    [RelayCommand]
    private void Edit()
    {
        if (SelectedPerson == null) return;

        var viewModel = new EditPersonViewModel(SelectedPerson, _db);
        var window = new EditPersonView { DataContext = viewModel };
        window.Owner = Application.Current.MainWindow;

        if (window.ShowDialog() == true)
        {
            People = new ObservableCollection<Person?>(GetUsers());
            ApplyFilters();
        }
    }

    [RelayCommand]
    private void Delete()
    {
        if (SelectedPerson == null) return;
        var viewModel = new DeletePersonViewModel(SelectedPerson, _db);
        var window = new DeletePersonView { DataContext = viewModel };
        window.Owner = Application.Current.MainWindow;
        var result = window.ShowDialog();

        if (result == true)
        {
            People = new ObservableCollection<Person?>(GetUsers());
            ApplyFilters();
        }
    }
}