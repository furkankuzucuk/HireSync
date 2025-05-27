using Project.Entities.DataTransferObjects.Announcement;

namespace Project.Services.Contracts;

public interface IAnnouncementService
{
    Task<IEnumerable<AnnouncementDto>> GetAllAnnouncements(bool trackChanges);
    Task<AnnouncementDto> GetAnnouncementById(int id, bool trackChanges);
    Task<AnnouncementDto> CreateAnnouncement(AnnouncementInsertDto dto);
    Task UpdateAnnouncement(int id, AnnouncementUpdateDto dto, bool trackChanges);
    Task DeleteAnnouncement(int id, bool trackChanges);
}
