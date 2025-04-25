using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes
{
    public class CandidateRepository : RepositoryBase<Candidate>, ICandidateRepository
    {
        public CandidateRepository(RepositoryContext context) : base(context) { }

        public void CreateCandidate(Candidate candidate)
        {
            Create(candidate);
        }

        public IQueryable<Candidate> GetCandidateById(int id, bool trackChanges)
        {
            return FindByCondition(d => d.CandidateId == id, trackChanges);
        }

        public void UpdateCandidate(Candidate candidate)
        {
            Update(candidate);
        }
    }
}
