namespace Core.Dto
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public string UserFullName { get; set; }
        public string UserId { get; set; }  

        public DateTime AccessTokenExpiration { get; set; }
    }
}
