using Microsoft.EntityFrameworkCore.Storage;
using e_Ticaret.Domain.Entities;
using e_Ticaret.Domain.Interfaces;
using e_Ticaret.Infrastructure.Data;
using e_Ticaret.Infrastructure.Repositories;

namespace e_Ticaret.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EticaretDbContext _context;
        private IDbContextTransaction? _transaction;
        
        // Lazy initialization için private fields
        private IDocumentRepository? _documents;
        private IUserRepository? _users;
        private readonly Dictionary<Type, object> _repositories = new();
        
        public UnitOfWork(EticaretDbContext context)
        {
            _context = context;
        }
        
        // Lazy loading properties
        public IDocumentRepository Documents
        {
            get
            {
                _documents ??= new DocumentRepository(_context);
                return _documents;
            }
        }
        
        public IUserRepository Users
        {
            get
            {
                _users ??= new UserRepository(_context);
                return _users;
            }
        }
        
        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T);
            
            if (_repositories.ContainsKey(type))
            {
                return (IRepository<T>)_repositories[type];
            }
            
            var repositoryInstance = new GenericRepository<T>(_context);
            _repositories.Add(type, repositoryInstance);
            
            return repositoryInstance;
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
        
        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }
        
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
