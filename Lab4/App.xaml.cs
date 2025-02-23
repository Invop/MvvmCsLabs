using System.Windows;
using Lab4.Data;

namespace Lab4;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        using var db = new AppDbContext();
        db.Database.EnsureCreated();
        Seeder.Seed(db);
    }
}