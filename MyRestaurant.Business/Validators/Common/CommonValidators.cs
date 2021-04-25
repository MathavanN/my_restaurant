using System;

namespace MyRestaurant.Business.Validators.Common
{
    public static class CommonValidators
    {
        public static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }
    }
}
