using Newtonsoft.Json;

namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class ErrorDetailDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}