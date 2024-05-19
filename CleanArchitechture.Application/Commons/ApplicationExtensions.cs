namespace CleanArchitechture.Application.Commons
{
    public static partial class ApplicationExtensions
    {
        private static readonly Random _random = new();
        private static readonly int[] _UnicodeCharactersList = Enumerable.Range(48, 10).Concat(Enumerable.Range(65, 26)).ToArray();

        /* -- Function extension -- */
        /// <summary>
        /// Generate mật khẩu
        /// </summary>
        /// <returns></returns>
        public static string PasswordGenerate()
        {
            int length = 10;
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            var random = new Random();
            string password = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return password;
        }

        /// <summary>
        /// Hash mật khẩu
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Hash(this string text)
        {
            return Crypt.HashPassword(text, 10, false);
        }

        /// <summary>
        /// Xác thực mật khẩu
        /// </summary>
        /// <param name="text"></param>
        /// <param name="comparedText"></param>
        /// <returns></returns>
        public static bool Verify(this string text, string comparedText)
        {
            return Crypt.Verify(text, comparedText, false);
        }

        /// <summary>
        /// Generate id người dùng
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GenerateUserId(this DateTime time, string prefix)
        {
            string charSet = $"ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz0123456789";
            int charSetLength = charSet.Length;

            int secondIndex = time.Second % charSetLength;
            char thirdChar = charSet[secondIndex];

            int yIndex = time.Year % charSetLength;
            char yChar = charSet[yIndex];

            int minuteIndex = time.Minute % charSetLength;
            char secondChar = charSet[minuteIndex];

            int MIndex = time.Month % charSetLength;
            char MChar = charSet[MIndex];

            int dIndex = time.Day % charSetLength;
            char dChar = charSet[dIndex];

            int hourIndex = time.Hour % charSetLength;
            char firstChar = charSet[hourIndex];

            return $"{prefix}-{GenarateRandomString(2)}{yChar}{MChar}{dChar}{firstChar}{secondChar}{thirdChar}";
        }

        public static string GenarateRandomString(int maxSize = 12)
        {
            // Step 1: Random number
            var randomNumberWithDate = new Random((int)DateTime.Now.Ticks).Next(1, 1000).ToString();

            // Step 2: Random difference sequence
            var differenceString = string.Empty;
            var difference = maxSize - randomNumberWithDate.Length;
            for (var i = 0; i < difference; i++)
            {
                var randomCharacter = _UnicodeCharactersList[_random.Next(1, _UnicodeCharactersList.Length)];
                differenceString += Convert.ToChar(randomCharacter);
            }

            // Step 3: Insert the string in Step 1 anywhere from the string in Step 2
            var index = new Random().Next(0, differenceString.Length);
            var result = differenceString.Insert(index, randomNumberWithDate);

            return result;
        }

        /* -- Const extension -- */
        public static DateTime TIME_NOW = DateTime.UtcNow;
        public const int IMAGE_LIMIT = 2 * 1024 * 1024;
    }
}
