namespace SearchRanking.Data.Models
{
    public class SearchHistory
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
        public string Url { get; set; }
        public string Rankings { get; set; }
        public DateTime SearchDate { get; set; }
    }
}
