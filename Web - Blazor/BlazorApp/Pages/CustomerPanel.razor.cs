using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Data.Services;

namespace BlazorApp.Pages
{
    public partial class CustomerPanel
    {
        ServiceResponse<List<Customer>> customersResponse = new ServiceResponse<List<Customer>>();
        List<Customer> customers = new List<Customer>();

        protected override async Task OnInitializedAsync()
        {
            customersResponse = await CustomerService.GetAllCustomers(); 
            if(customersResponse.Success)
            {
                customers = customersResponse.Data;
            }
        }
    }
}
