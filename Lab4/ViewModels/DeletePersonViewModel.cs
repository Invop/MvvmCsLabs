using CommunityToolkit.Mvvm.ComponentModel;
using Lab4.Data;
using Lab4.Models;
using Lab4.ViewModels.Base;

namespace Lab4.ViewModels;

public partial class DeletePersonViewModel : DialogViewModelBase
{
    private readonly AppDbContext _dbContext;

    [ObservableProperty] private Person? _personToDelete;

    public DeletePersonViewModel(Person? person, AppDbContext dbContext)
    {
        PersonToDelete = person;
        _dbContext = dbContext;
    }

    public override void Save()
    {
        _dbContext.People.Remove(PersonToDelete);
        _dbContext.SaveChanges();
        base.Save();
    }
}