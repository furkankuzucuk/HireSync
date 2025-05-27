using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes;

public class AnnouncementRepository : RepositoryBase<Announcement>, IAnnouncementRepository
{
    public AnnouncementRepository(RepositoryContext context) : base(context) { }

    public IQueryable<Announcement> GetAllAnnouncements(bool trackChanges) =>
        FindAll(trackChanges);

    public IQueryable<Announcement> GetAnnouncementById(int id, bool trackChanges) =>
        FindByCondition(a => a.AnnouncementId == id, trackChanges);

    public void CreateAnnouncement(Announcement announcement) => Create(announcement);
    public void UpdateAnnouncement(Announcement announcement) => Update(announcement);
    public void DeleteAnnouncement(Announcement announcement) => Delete(announcement);
}
