using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.Announcement;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes;

public class AnnouncementService : IAnnouncementService
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public AnnouncementService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AnnouncementDto>> GetAllAnnouncements(bool trackChanges)
    {
        var announcements = await repositoryManager.AnnouncementRepository.GetAllAnnouncements(trackChanges)
            .OrderByDescending(a => a.CreatedDate)
            .ToListAsync();

        return mapper.Map<IEnumerable<AnnouncementDto>>(announcements);
    }

    public async Task<AnnouncementDto> GetAnnouncementById(int id, bool trackChanges)
    {
        var entity = await repositoryManager.AnnouncementRepository.GetAnnouncementById(id, trackChanges).FirstOrDefaultAsync();
        if (entity == null)
            throw new EntityNotFoundException<Announcement>(id);

        return mapper.Map<AnnouncementDto>(entity);
    }

    public async Task<AnnouncementDto> CreateAnnouncement(AnnouncementInsertDto dto)
    {
        var entity = mapper.Map<Announcement>(dto);
        repositoryManager.AnnouncementRepository.CreateAnnouncement(entity);
        await repositoryManager.Save();
        return mapper.Map<AnnouncementDto>(entity);
    }

    public async Task UpdateAnnouncement(int id, AnnouncementUpdateDto dto, bool trackChanges)
    {
        var entity = await repositoryManager.AnnouncementRepository.GetAnnouncementById(id, trackChanges).FirstOrDefaultAsync();
        if (entity == null)
            throw new EntityNotFoundException<Announcement>(id);

        mapper.Map(dto, entity);
        await repositoryManager.Save();
    }

    public async Task DeleteAnnouncement(int id, bool trackChanges)
    {
        var entity = await repositoryManager.AnnouncementRepository.GetAnnouncementById(id, trackChanges).FirstOrDefaultAsync();
        if (entity == null)
            throw new EntityNotFoundException<Announcement>(id);

        repositoryManager.AnnouncementRepository.DeleteAnnouncement(entity);
        await repositoryManager.Save();
    }
}
