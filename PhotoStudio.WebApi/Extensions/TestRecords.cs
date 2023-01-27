namespace PhotoStudio.WebApi.Extensions
{
    public record Person(string FirstName, string LastName);

    public record User(string FirstName, string LastName)
    {
        public int Id { get; init; }
        public DateTime CreatedDate { get; init; } = DateTime.UtcNow;
        public string Phone { get; set; } = "+44 ";
        public bool VerifiedEmail { get; set; } = false;
    }

    public record Animal {

        public string Run { get; init; } = default!;

        public string Swimm { get; init; } = default!;

    }
    public class ciudad {

        public string algo() {

            Person ss = new Person("habana", "vedado");
            return ss.FirstName;

            User user = new User("guillermo", "Mesa") { Id = 1, CreatedDate = DateTime.Now };
            
        }

    }


   

}
