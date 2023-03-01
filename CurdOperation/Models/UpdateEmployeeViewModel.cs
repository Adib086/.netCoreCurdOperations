namespace CurdOperation.Models
{
    public class UpdateEmployeeViewModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string department { get; set; }
        public string email { get; set; }
        public int salary { get; set; }
        public DateTime dateOfBirth { get; set; }
    }
}
