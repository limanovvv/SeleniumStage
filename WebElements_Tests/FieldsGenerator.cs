



namespace WebElements_Tests
{
    /// <summary>
    /// класс для генерации тестовых данных
    /// </summary>
	public static class FieldsGenerator
	{
        private static Random rand = new Random();

        private static string[] firstNames = { "Иван", "Петр", "Сергей", "Алексей", "Михаил", "Валерий", "Станислав" };
        private static string[] lastNames = { "Иванов", "Петров", "Сидоров", "Кузнецов", "Попов" };
        private static string[] middleNames = { "Иванович", "Петрович", "Сергеевич", "Алексеевич", "Михайлович" };

        public static string GenerateFirstName()
        {
            return firstNames[rand.Next(firstNames.Length)];
        }

        public static string GenerateLastName()
        {
            return lastNames[rand.Next(lastNames.Length)];
        }

        public static string GenerateMiddleName()
        {
            return middleNames[rand.Next(middleNames.Length)];
        }

        public static string GenerateDateOfBirth()
        {
            int year = rand.Next(1924, 2007); // для второго аргумента + 1
            int month = rand.Next(1, 13); // также для второго аргумента + 1
            int date = rand.Next(1, 29); // до 29 чтобы избежать проблем с датой
            return $"{date.ToString("D2", System.Globalization.CultureInfo.CurrentCulture)}.{month.ToString("D2", System.Globalization.CultureInfo.CurrentCulture)}.{year}";
        }

        public static string GenerateRandomPassportNumber()
        {
            return $"{rand.Next(1000, 9999)} {rand.Next(100000, 999999)}";
        }

        public static string GenerateRandomPhoneNumber()
        {
            return $"{rand.Next(900, 999)}{rand.Next(1000000, 9999999)}";
        }


    }
}

