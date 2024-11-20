namespace NewProject.ResquestBodyAccount
{
    public class ResquestBodyLogin
    {
       
            public string user { get; set; }
            public string passWord { get; set; }
        
    }
    public class ResquestCreatUser
    {

        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

    }
}
