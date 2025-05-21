using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.Anouncement;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Project.Services.Concretes
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public AnnouncementService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<AnouncementDto>> GetAllAnnouncements(bool trackChanges)
        {
            var announcements = await repositoryManager.AnnouncementRepository.GetAllAnnouncements(trackChanges)
                .Where(a => a.IsActive)  // Aktif duyuruları getir
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
            return mapper.Map<IEnumerable<AnouncementDto>>(announcements);
        }

        public async Task<AnouncementDto> GetAnnouncementById(int id, bool trackChanges)
        {
            var entity = await repositoryManager.AnnouncementRepository.GetAnnouncementById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<Anouncement>(id);

            return mapper.Map<AnouncementDto>(entity);
        }

        public async Task<AnouncementDto> CreateAnnouncement(AnouncementInsertDto announcementDto)
        {
            var entity = mapper.Map<Anouncement>(announcementDto);
            repositoryManager.AnnouncementRepository.CreateAnnouncement(entity);
            await repositoryManager.Save();
            return mapper.Map<AnouncementDto>(entity);
        }

        public async Task UpdateAnnouncement(int id, AnouncementUpdateDto announcementDto, bool trackChanges)
        {
            var entity = await repositoryManager.AnnouncementRepository.GetAnnouncementById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<Anouncement>(id);

            mapper.Map(announcementDto, entity);
            await repositoryManager.Save();
        }

        public async Task DeleteAnnouncement(int id, bool trackChanges)
        {
            var entity = await repositoryManager.AnnouncementRepository.GetAnnouncementById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<Anouncement>(id);

            repositoryManager.AnnouncementRepository.DeleteAnnouncement(entity);
            await repositoryManager.Save();
        }
    }
}
