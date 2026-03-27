namespace WebAppECommerce.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }

        public bool? AgeLevel()
        {
            return Age > 18;
        }

        public int? AgeLevelLevel()
        {
            if ((bool)AgeLevel()!)
                return Age;
            return null;
        }
    }
}
