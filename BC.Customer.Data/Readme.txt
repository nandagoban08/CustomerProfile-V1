
// generate dbcontext command  
Scaffold-DbContext "Server=igisystem.database.windows.net,1433;Database=bookcabin;User ID=bcadmin;Password=Pass#word1@;Trusted_Connection=False;" Microsoft.EntityFrameworkCore.SqlServer -contextDir CustomerDbContext -context CustomerEntities -schema dbo -OutputDir CustomerDataModels -Force