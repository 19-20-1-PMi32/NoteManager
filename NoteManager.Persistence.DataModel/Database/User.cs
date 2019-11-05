using System.Collections.Generic;

namespace NoteManager.Persistence.DataModel.Database
{
    public class User
    {
        public User()
        {
            Pictures = new HashSet<Picture>();
            Videos = new HashSet<Video>();
            Plans = new HashSet<Plan>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
        public IEnumerable<Video> Videos { get; set; }
        public IEnumerable<Plan> Plans { get; set; }

    }
}
