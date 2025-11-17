namespace e_Ticaret.Domain.Enums
{
    public enum DocumentStatus
    {
        Draft = 1,      // Taslak
        Pending = 2,    // Beklemede
        Approved = 3,   // Onaylandı
        Rejected = 4,   // Reddedildi
        Published = 5,  // Yayınlandı
        Archived = 6    // Arşivlendi
    }
}
