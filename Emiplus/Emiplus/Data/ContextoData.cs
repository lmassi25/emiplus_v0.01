namespace Emiplus.Data
{
    using Microsoft.EntityFrameworkCore;
    using Emiplus.Model;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Diagnostics;
    using System;

    public class ContextoData : DbContext
    {
        private const string _path = @"C:\emiplus_v0.01\EMIPLUS.FDB";
        private const string _user = "sysdba";
        private const string _pass = "masterkey";
        private const string _db = "sysdba";
        private const string _host = "localhost";

        public DbSet<Item> Itens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseFirebird($"character set=NONE;initial catalog={_path};user id={_user};data source={_host};user id={_db};Password={_pass};Pooling=true;Dialect=3");
        }

    }
}
