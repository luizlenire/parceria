using AppCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ParceriaAPI.SeveralFunctions;
using System.Data;

namespace AppCore.Database
{
    /* --> † 24/09/2020 - Luiz Lenire <-- */

    public class Context : DbContext
    {
        #region --> Public properties. <--   

        public DbSet<Parceria> Parceria { get; set; }

        public DbSet<ParceriaLike> ParceriaLike { get; set; }

        public DbSet<ParceriaLog> ParceriaLog { get; set; }

        public DbSet<vParceria> vParceria { get; set; }

        public DbSet<vParceriaLike> vParceriaLike { get; set; }

        #endregion --> Public properties. <--           

        #region --> Constructors. <--

        public Context() { }

        public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions) { }

        #endregion --> Constructors. <--

        #region --> Protected methods. <--

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionBuilder)
        {
            if (!dbContextOptionBuilder.IsConfigured)
            {
                dbContextOptionBuilder.UseSqlServer(GlobalAtributtes.Connectionstring, opt => opt.CommandTimeout(GlobalAtributtes.Timeout));
            }
        }

        #endregion --> Protected methods. <-- 

        #region --> Public methods. <--     

        public dynamic Get(object id, object obj)
        {
            using IDbContextTransaction idbContextTransaction = Database.BeginTransaction(IsolationLevel.ReadUncommitted);
            obj = Find(obj.GetType(), id);
            idbContextTransaction.Commit();

            return obj;
        }

        public void Post(object obj)
        {
            Add(obj);
            SaveChanges();
        }

        public void Put(object obj)
        {
            Update(obj);
            SaveChanges();
        }

        public void Delete(object obj)
        {
            Remove(obj);
            SaveChanges();
        }

        #endregion --> Public methods. <--
    }
}
