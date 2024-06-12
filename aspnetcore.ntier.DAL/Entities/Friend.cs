
namespace aspnetcore.ntier.DAL.Entities
{

    public enum Gender
    {
        Male,
        Female,
        Other,
        Unknown
    }

    public class Friend
    {
        public int Id { get; set; }
        public int? Age {  get; set; }
        public string? Email { get; set; }
        public string Gender { get; set; } = "unknown";
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string DateCreated { get; set; }

        public string Picture { get; set; }

        public Friend()
        {

            DateCreated = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ssZ");
        }


    }
}
