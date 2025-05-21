using Project.Entities;
using System.Linq;

namespace Project.Repository.Contracts
{
    public interface IAnnouncementRepository : IRepositoryBase<Anouncement>
    {
        IQueryable<Anouncement> GetAllAnnouncements(bool trackChanges);
        IQueryable<Anouncement> GetAnnouncementById(int id, bool trackChanges);
        void CreateAnnouncement(Anouncement announcement);
        void UpdateAnnouncement(Anouncement announcement);
        void DeleteAnnouncement(Anouncement announcement);
    }
}
