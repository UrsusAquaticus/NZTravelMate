using SQLite;

namespace NZTravelMate.Models
{
    public class AppState
    {
        [PrimaryKey]
        public int Id { get; set; }

        //State of the picker's index
        public int FirstIndex { get; set; }

        public int SecondIndex { get; set; }

        public string LastUpdated { get; set; }
    }
}