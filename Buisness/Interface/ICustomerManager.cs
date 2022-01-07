using DTO;

namespace BLL
{
    public interface ICustomerManager
    {
        void AddCustomer(Person person);
        void UpdateCustomer(Person person);
    }


}