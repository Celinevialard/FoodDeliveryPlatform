using DTO;

namespace BLL
{
    public interface ICustomerManager
    {
        bool AddCustomer(Person person);
        void UpdateCustomer(Person person);
    }


}