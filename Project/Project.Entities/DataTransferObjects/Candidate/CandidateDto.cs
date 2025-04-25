namespace Project.Entities.DataTransferObjects.Candidate
{
    public record CandidateDto
    {
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Mail { get; set; }
    }
}
