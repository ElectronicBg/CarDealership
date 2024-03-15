using System.ComponentModel.DataAnnotations;

namespace CarDealership.Helpers
{
    public static class EnumHelper
    {
        public static string GetDisplayName(Enum value)
        {
            var displayName = value.ToString(); // Default to enum value if display name not found

            var displayAttribute = value.GetType()
                                         .GetField(value.ToString())
                                         .GetCustomAttributes(typeof(DisplayAttribute), false)
                                         as DisplayAttribute[];

            if (displayAttribute != null && displayAttribute.Length > 0)
            {
                displayName = displayAttribute[0].Name;
            }

            return displayName;
        }
    }
}
