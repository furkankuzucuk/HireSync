using Project.Entities;

namespace Project.Repository.Contracts;

public interface IAnnouncementRepository : IRepositoryBase<Announcement>
{
    IQueryable<Announcement> GetAllAnnouncements(bool trackChanges);
    IQueryable<Announcement> GetAnnouncementById(int id, bool trackChanges);
    void CreateAnnouncement(Announcement announcement);
    void UpdateAnnouncement(Announcement announcement);
    void DeleteAnnouncement(Announcement announcement);
}
