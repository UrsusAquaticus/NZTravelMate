using SQLite;

namespace NZTravelMate.Models
{
    public class Currency
    {
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }
        [PrimaryKey, MaxLength(3), NotNull]
        public string Code { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        public double Rate { get; set; }
    }
}