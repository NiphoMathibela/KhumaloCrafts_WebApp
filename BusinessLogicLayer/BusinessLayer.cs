using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class BusinessLayer
    {
        DataLayer dataClass = new DataLayer();

        public void NewUser(string name, string email, string passwordHash, string shippingAddress, string billingAddress)
        {
            dataClass.CreateUser(name, email, passwordHash, shippingAddress, billingAddress);
        }
    }
}
