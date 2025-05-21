using Project.Entities.DataTransferObjects.Anouncement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Services.Contracts
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<AnouncementDto>> GetAllAnnouncements(bool trackChanges);
        Task<AnouncementDto> GetAnnouncementById(int id, bool trackChanges);
        Task<AnouncementDto> CreateAnnouncement(AnouncementInsertDto announcementDto);
        Task UpdateAnnouncement(int id, AnouncementUpdateDto announcementDto, bool trackChanges);
        Task DeleteAnnouncement(int id, bool trackChanges);
    }
}
