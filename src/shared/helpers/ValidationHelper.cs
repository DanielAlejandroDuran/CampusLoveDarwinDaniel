namespace CampusLoveDarwinDaniel.Shared.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidAge(int age)
        {
            return age >= 18 && age <= 100;
        }

        public static bool IsValidGender(string gender)
        {
            var validGenders = new[] { "Masculino", "Femenino", "Otro" };
            return validGenders.Contains(gender);
        }
    }
}
