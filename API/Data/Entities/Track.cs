namespace api.Data.Entities
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int UniquePitBoxes { get; set; }
        public int PrivateServerSlots { get; set; }
    }
}