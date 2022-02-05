using BlazorApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Data.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _db;

        public CustomerService(DataContext db)
        {
            _db = db;
        }

        #region Get methods
        public async Task<ServiceResponse<List<Customer>>> GetAllCustomers()
        {
            ServiceResponse<List<Customer>> response = new ServiceResponse<List<Customer>>();
            List<Customer> customers = new List<Customer>();

            try
            {
                customers = await _db.Customers.ToListAsync();
                response.Data = customers;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;                
            }

            return response;
        }

        public async Task<ServiceResponse<Customer>> GetCustomer(string id)
        {
            ServiceResponse<Customer> response = new ServiceResponse<Customer>();

            try
            {
                Customer customer = await _db.Customers.Where(c => c.Id == id).SingleAsync();
                response.Data = customer;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        //pagination
        public async Task<ServiceResponse<PagedList<Customer>>> GetAllCustomersPage(int page)
        {
            ServiceResponse<PagedList<Customer>> response = new ServiceResponse<PagedList<Customer>>();
            PagedList<Customer> customers = new PagedList<Customer>();

            //the amount of customers shown in each page
            int pageItems = 2;

            try
            {
                int pageCount = (int)Math.Ceiling((double)_db.Customers.Count() / pageItems);

                customers.DataList = await _db.Customers
                                .Skip((page - 1) * pageItems)
                                .Take(pageItems)
                                .ToListAsync();
                customers.CurrentPage = page;
                customers.TotalPages = pageCount;

                response.Data = customers;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
        #endregion

        //create new customer
        public async Task<ServiceResponse<bool>> CreateCustomer(Customer newCustomer)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            bool result = false;

            try
            {
                //get max id
                int maxId = Convert.ToInt32(_db.Customers.Max(c => c.Id)) + 1;
                //set new customers id
                newCustomer.Id = maxId.ToString();

                await _db.Customers.AddAsync(newCustomer);
                await _db.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;                
            }

            response.Data = result;
            return response;
        }

        //update customer entry
        public async Task<ServiceResponse<bool>> UpdateCustomer(Customer customer)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            bool result = false;

            try
            {
                _db.Customers.Update(customer);
                await _db.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            response.Data = result;
            return response;
        }

        //delete customer
        public async Task<ServiceResponse<bool>> DeleteCustomer(string id)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            bool result = false;

            try
            {
                Customer customer = await _db.Customers.Where(c => c.Id == id).SingleAsync();
                _db.Customers.Remove(customer);
                await _db.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            response.Data = result;
            return response;
        }
    }
}
