namespace Project.Entities.DataTransferObjects.Candidate
{
    public record CandidateAuthenticationDto
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
