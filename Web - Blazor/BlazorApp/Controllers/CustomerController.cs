using BlazorApp.Data.Services;
using BlazorApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        #region GET endpoins
        //api/customer
        [HttpGet]
        public async Task<ServiceResponse<List<Customer>>> GetAllCustomers()
        {
            return await _customerService.GetAllCustomers();
        }

        //api/customer/{id}
        //where id a number for example api/customer/2
        [HttpGet("{id}")]
        public async Task<ServiceResponse<Customer>> GetCustomer(string id)
        {
            return await _customerService.GetCustomer(id);
        }
        #endregion
        
        //api/customer/new
        [HttpPost("new")]        
        public async Task<ServiceResponse<bool>> CreateCustomer(Customer customer)
        {
            return await _customerService.CreateCustomer(customer);
        }

        //api/customer/update
        [HttpPut("update")]
        public async Task<ServiceResponse<bool>> UpdateCustomer(Customer customer)
        {
            return await _customerService.UpdateCustomer(customer);
        }

        //api/customer/delete
        [HttpDelete("delete/{id}")]
        public async Task<ServiceResponse<bool>> DeleteCharacter(string id)
        {
            return await _customerService.DeleteCustomer(id);
        }
    }
}
