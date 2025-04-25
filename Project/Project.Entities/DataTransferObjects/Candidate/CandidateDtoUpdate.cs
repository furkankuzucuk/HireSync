namespace Project.Entities.DataTransferObjects.Candidate
{
    public record CandidateDtoUpdate
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Mail { get; set; }
    }
}
