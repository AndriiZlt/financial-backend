

namespace aspnetcore.ntier.DTO.DTOs;

    public enum Gender
    {
        Male,
        Female,
        Other,
        Unknown
    }

public class FriendDTO
    {
        public int Id { get; set; }
        public int? Age {  get; set; }
        public string? Email { get; set; }

    public string Gender { get; set; }
/*    public Gender? Gender { get; set; }*/
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? DateCreated { get; set; }

    public string Picture { get; set; }

}

