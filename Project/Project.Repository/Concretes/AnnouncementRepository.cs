using Project.Entities;
using Project.Repository.Contracts;
using System.Linq;

namespace Project.Repository.Concretes
{
    public class AnnouncementRepository : RepositoryBase<Anouncement>, IAnnouncementRepository
    {
        public AnnouncementRepository(RepositoryContext context) : base(context) { }

        public IQueryable<Anouncement> GetAllAnnouncements(bool trackChanges) =>
            FindAll(trackChanges);

        public IQueryable<Anouncement> GetAnnouncementById(int id, bool trackChanges) =>
            FindByCondition(a => a.AnouncementId == id, trackChanges);

        public void CreateAnnouncement(Anouncement anouncement) =>
            Create(anouncement);

        public void UpdateAnnouncement(Anouncement anouncement) =>
            Update(anouncement);

        public void DeleteAnnouncement(Anouncement anouncement) =>
            Delete(anouncement);
    }
}
