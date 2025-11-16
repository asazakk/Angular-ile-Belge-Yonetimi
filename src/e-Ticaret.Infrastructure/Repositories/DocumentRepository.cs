using Microsoft.EntityFrameworkCore;
using e_Ticaret.Domain.Entities;
using e_Ticaret.Domain.Enums;
using e_Ticaret.Domain.Interfaces;
using e_Ticaret.Infrastructure.Data;

namespace e_Ticaret.Infrastructure.Repositories
{
    public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(EticaretDbContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<Document>> GetByStatusAsync(DocumentStatus status)
        {
            return await _dbSet
                .Include(d => d.CreatedByUser)
                .Include(d => d.UpdatedByUser)
                .Where(d => d.Status == status && !d.IsDeleted)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Document>> GetByTypeAsync(DocumentType documentType)
        {
            return await _dbSet
                .Include(d => d.CreatedByUser)
                .Include(d => d.UpdatedByUser)
                .Where(d => d.DocumentType == documentType && !d.IsDeleted)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Document>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(d => d.CreatedByUser)
                .Include(d => d.UpdatedByUser)
                .Where(d => (d.CreatedByUserId == userId || d.UpdatedByUserId == userId) && !d.IsDeleted)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Document>> GetRecentDocumentsAsync(int count)
        {
            return await _dbSet
                .Include(d => d.CreatedByUser)
                .Include(d => d.UpdatedByUser)
                .Where(d => !d.IsDeleted)
                .OrderByDescending(d => d.CreatedAt)
                .Take(count)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Document>> SearchDocumentsAsync(string searchTerm)
        {
            var lowerSearchTerm = searchTerm.ToLower();
            
            return await _dbSet
                .Include(d => d.CreatedByUser)
                .Include(d => d.UpdatedByUser)
                .Where(d => !d.IsDeleted && (
                    d.Title.ToLower().Contains(lowerSearchTerm) ||
                    (d.Description != null && d.Description.ToLower().Contains(lowerSearchTerm))
                ))
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }
        
        public async Task<int> GetDocumentCountByStatusAsync(DocumentStatus status)
        {
            return await _dbSet
                .CountAsync(d => d.Status == status && !d.IsDeleted);
        }
        
        // Override GetByIdAsync to include related entities
        public override async Task<Document?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(d => d.CreatedByUser)
                .Include(d => d.UpdatedByUser)
                .Include(d => d.DocumentActions)
                    .ThenInclude(da => da.User)
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        }
        
        // Override GetAllAsync to include related entities
        public override async Task<IEnumerable<Document>> GetAllAsync()
        {
            return await _dbSet
                .Include(d => d.CreatedByUser)
                .Include(d => d.UpdatedByUser)
                .Where(d => !d.IsDeleted)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }
    }
}
