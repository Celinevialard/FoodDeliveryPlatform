using DTO;

namespace DAL
{
	public interface ICustomersDB
	{
		Customer AddCustomer(Customer customer);
		Customer GetCustomer(int customerId);
	}
}