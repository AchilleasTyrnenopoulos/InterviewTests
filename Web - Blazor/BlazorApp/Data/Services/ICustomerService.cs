using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Data.Services
{
    public interface ICustomerService
    {
        //get
        Task<ServiceResponse<List<Customer>>> GetAllCustomers();
        Task<ServiceResponse<Customer>> GetCustomer(string id);
        Task<ServiceResponse<PagedList<Customer>>> GetAllCustomersPage(int page);

        //create
        Task<ServiceResponse<bool>> CreateCustomer(Customer newCustomer);

        //update
        Task<ServiceResponse<bool>> UpdateCustomer(Customer customer);

        //delete
        Task<ServiceResponse<bool>> DeleteCustomer(string id);
    }
}
