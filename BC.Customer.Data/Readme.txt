
// generate dbcontext command  
Scaffold-DbContext "" Microsoft.EntityFrameworkCore.SqlServer -contextDir CustomerDbContext -context CustomerEntities -schema dbo -OutputDir CustomerDataModels -Force

// place you connection string Scaffold-DbContext "sql database connection string"
