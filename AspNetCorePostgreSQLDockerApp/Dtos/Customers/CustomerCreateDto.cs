namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class CustomerCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string Gender { get; set; }
    }
}