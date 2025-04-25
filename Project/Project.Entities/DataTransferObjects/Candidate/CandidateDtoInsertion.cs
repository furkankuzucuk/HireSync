namespace Project.Entities.DataTransferObjects.Candidate
{
    public record CandidateDtoInsertion
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
