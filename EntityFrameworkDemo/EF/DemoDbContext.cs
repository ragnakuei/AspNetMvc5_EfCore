using EntityFrameworkDemo.Models.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDemo.EF
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
        }

        public DbSet<CompCv>                    CompCv                    { get; set; }
        public DbSet<CompCvCertificate>         CompCvCertificate         { get; set; }
        public DbSet<CompCvEducation>           CompCvEducation           { get; set; }
        public DbSet<CompCvLanguageRequirement> CompCvLanguageRequirement { get; set; }
        public DbSet<Country>                   Country                   { get; set; }
        public DbSet<CountryLanguage>           CountryLanguage           { get; set; }
        public DbSet<County>                    County                    { get; set; }

        public DbSet<CountyLanguage> CountyLanguage { get; set; }
        //public DbSet<ActionReceipt>             ActionReceipt             { get; set; }
        //public DbSet<ActionReceiptNoteLanguage> ActionReceiptNoteLanguage { get; set; }
    }
}