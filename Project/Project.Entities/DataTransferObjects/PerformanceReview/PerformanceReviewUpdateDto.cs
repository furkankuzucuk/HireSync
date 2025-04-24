namespace Project.Entities.DataTransferObjects.PerformanceReview
{
    public record PerformanceReviewUpdateDto
    {
        public byte PerformanceRate { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
