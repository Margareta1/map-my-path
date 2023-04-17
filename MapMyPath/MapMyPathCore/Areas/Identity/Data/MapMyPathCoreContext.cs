using MapMyPathCore.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MapMyPathCore.Data;

public class MapMyPathCoreContext : IdentityDbContext<MapMyPathCoreUser>
{
    private static readonly string SERVER = "Server=tcp:mapmypathoicar.database.windows.net,1433;Initial Catalog=MapMyPath;Persist Security Info=False;User ID=PPPK10@racunarstvo.hr;Password=8&Np=D#xn;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Authentication=\"Active Directory Password\";";
    public MapMyPathCoreContext(DbContextOptions<MapMyPathCoreContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(SERVER);
    }
}
