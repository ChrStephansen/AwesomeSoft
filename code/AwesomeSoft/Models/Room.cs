namespace AwesomeSoft.Models
{
    class Room
    {
        public Room(uint no, string description)
        {
            No = no;
            Description = description;
        }

        public uint No { get; }
        public string Description { get; }
    }
}
