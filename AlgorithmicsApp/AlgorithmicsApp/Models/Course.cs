using SQLite;

namespace AlgorithmicsApp.Models
{
    public class Course
    {
        
        public string Name { get; set; }
        
        public string Image { get; set; }
        [PrimaryKey]
        public int Id { get; set; }
    }
}
