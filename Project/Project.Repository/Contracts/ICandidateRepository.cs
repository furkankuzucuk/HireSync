using Project.Entities;

namespace Project.Repository.Contracts
{
    public interface ICandidateRepository : IRepositoryBase<Candidate>
    {
        IQueryable<Candidate> GetCandidateById(int id, bool trackChanges);
        void CreateCandidate(Candidate candidate);
        void UpdateCandidate(Candidate candidate);
    }
}
