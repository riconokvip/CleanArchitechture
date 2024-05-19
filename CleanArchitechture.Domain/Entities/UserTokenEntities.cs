namespace CleanArchitechture.Domain.Entities
{
    /// <summary>
    /// Thực thể phiên đăng nhập của người dùng trong hệ thống
    /// </summary>
    [Table("UserTokens")]
    public class UserTokenEntities : BaseEntity<string>
    {
        /// <summary>
        /// Id người dùng
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Id máy khách
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Access token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Thời gian hết hạn access token
        /// </summary>
        public DateTime AccessTokenExpiresAt { get; set; }

        /// <summary>
        /// Refresh token
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// Thời gian hết hạn refresh token
        /// </summary>
        public DateTime RefreshTokenExpiresAt { get; set; }

        /// <summary>
        /// Trạng thái thu hồi
        /// </summary>
        public bool IsRevoked { get; set; }
    }
}
