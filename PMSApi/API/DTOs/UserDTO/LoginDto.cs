namespace API.DTOs
{
    /// <summary>
    /// Data transfer object for user login information.
    /// </summary>
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
