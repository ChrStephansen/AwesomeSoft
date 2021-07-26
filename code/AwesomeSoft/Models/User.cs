namespace AwesomeSoft.Models
{
    public class User
    {
        
        public User(uint no, string userName = "")
        {
            No = no;
            UserName = userName;
        }

        public string UserName { get; }
        public uint No { get; }
    }
}
