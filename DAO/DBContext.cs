using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Reflection;
using Model;
using System.Reflection.Metadata.Ecma335;
using System.Linq.Expressions;

namespace DAO
{
    public class DBContext : DbContext
    {
        // Singleton instance
        private static DBContext _instance;
        public static DBContext Instance => _instance ??= new DBContext();
        //Delegate CRUD
        public delegate void AddEntity<T>(T entity) where T : class;
        public delegate void RemoveEntity<T>(T entity) where T : class;
        public delegate T? FindEntity<T>(Expression<Func<T, bool>> predicate) where T : class;
        public delegate List<T> GetAllEntity<T>(params Expression<Func<T, object>>[]? includes) where T : class;
        //Delegate Log Error Event
        public delegate void LogErrorEventHandler(string message, Exception exception);
        //Event
        public Action<object>? OnEntityAdded;
        public Action<object>? OnEntityModified;
        public Action<object>? OnEntityDeleted;
        public event LogErrorEventHandler? LogError;
        // Static field lưu chuỗi kết nối sau khi đọc từ appsettings.json
        private readonly string ConnectionString;
        // Default admin account (from appsettings.json)
        public string AdminEmail { get; private set; }
        public string AdminPassword { get; private set; }
        private string LoadAppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // For console or desktop apps
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            AdminEmail = config["DefaultAccount:Email"];
            AdminPassword = config["DefaultAccount:Password"];

            return config.GetConnectionString("MyDb")
            ?? throw new Exception("Không tìm thấy connection string 'MyDb' trong appsettings.json.");
        }
        public DBContext()
        {
            // Load data from appsettings.json
            ConnectionString = LoadAppSettings();
        }
        //Thiết lập kết nối CSDL tại đây
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
        // Ghi đè SaveChanges để bắt các sự kiện
        public override int SaveChanges(){
            try
            {
                var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added
                         || e.State == EntityState.Modified
                         || e.State == EntityState.Deleted)
                .ToList();

                foreach (var entry in entries)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            OnEntityAdded?.Invoke(entry.Entity);
                            break;
                        case EntityState.Modified:
                            OnEntityModified?.Invoke(entry.Entity);
                            break;
                        case EntityState.Deleted:
                            OnEntityDeleted?.Invoke(entry.Entity);
                            break;
                    }
                }
                return base.SaveChanges();
            }
            catch (DbUpdateException ex) {
                //Call event error
                foreach (var entityEntry in ex.Entries)
                {
                    LogError?.Invoke($"Error while saving entity of type {entityEntry.Entity.GetType().Name}", ex);
                }
                throw;
            }
        }
        // Generic query method
        public GetAllEntity<T> GetAll<T>() where T : class
        {
            return (includes) =>
            {
                IQueryable<T> query = Set<T>();
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                return query.ToList();
            };
        }
        // Generic add/find/delete
        public AddEntity<T> Add<T>() where T : class{
            return entity =>{
                Set<T>().Add(entity);
                SaveChanges();
            };
        }
        public RemoveEntity<T> Delete<T>() where T: class {
            return entity =>{
                Set<T>().Remove(entity);
                SaveChanges();
            };
        }
        public FindEntity<T> Search<T>() where T : class{
            return predicate =>{
                return Set<T>().FirstOrDefault(predicate);
            };
        }
    }
}
