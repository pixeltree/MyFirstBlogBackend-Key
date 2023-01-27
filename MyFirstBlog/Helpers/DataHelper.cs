namespace MyFirstBlog.Helpers;

using Microsoft.EntityFrameworkCore;

public static class DataHelper
{
    
    public static async Task ManageDataAsync(IServiceProvider svcProvider)
    {
        //Service: An instance of db context
        var dbContextSvc = svcProvider.GetRequiredService<DataContext>();
        
        //Migration: This is the programmatic equivalent to Update-Database
        await dbContextSvc.Database.MigrateAsync();
    }
    
}
