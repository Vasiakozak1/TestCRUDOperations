namespace TestCRUDOperations.Web.DTOs
{
    public class ProjectDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int StartDay { get; set; }
        public int StartMonth { get; set; }
        public int StartYear { get; set; }

        public int EndDay { get; set; }
        public int EndMonth { get; set; }
        public int EndYear { get; set; }
    }
}