using System.Linq;

namespace Registration.Models
{
    public static class Check
    {
        public static string EmailExists(RegistrationContext context, string email)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(email)) {
                var customer = context.Customers.FirstOrDefault(
                    c => c.EmailAddress.ToLower() == email.ToLower());
                if (customer != null) 
                    msg = $"Email address {email} already in use.";
            }
            return msg;
        }
    }
}