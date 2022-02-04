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
        Customer Customer { get; set; } = new Customer();
        bool showForm = false;

        protected override async Task OnInitializedAsync()
        {
            await GetAllCustomers();
        }

        #region Service calls
        private async Task DeleteCustomer(string id)
        {
            //delete customer
            await CustomerService.DeleteCustomer(id);

            //get customers again
            await GetAllCustomers();
        }

        private async Task GetAllCustomers()
        {
            customersResponse = await CustomerService.GetAllCustomers();
            if (customersResponse.Success)
            {
                customers = customersResponse.Data;
            }
        }

        private async Task AddCustomer(Customer customer)
        {
            await CustomerService.CreateCustomer(customer);
        }

        private async Task UpdateCustomer(Customer customer)
        {
            await CustomerService.UpdateCustomer(customer);
        }
        #endregion

        #region Button methods
        private void AddCustomerButtonClick()
        {
            //re-initialize Customer prop
            Customer = new Customer();

            ToggleShowForm(!showForm);
        }       

        private async Task EditButtonClick(string id)
        {
            ToggleShowForm(!showForm);

            if (showForm)
            {
                try
                {
                    //get customer
                    Customer customer = customers.Where(c => c.Id == id).SingleOrDefault();

                    //fill values of form
                    Customer = customer;

                    //update customer
                    await UpdateCustomer(customer);

                    await GetAllCustomers();
                }
                catch (Exception ex)
                {
                    //show exception message
                }
            }
            else
            {
                //re-initialize Customer model
                Customer = new Customer();
            }
        }

        private async Task HandleSubmit()
        {
            //check if we are updating or adding a new customer
            bool updating = customers.Where(c => c.Id == Customer.Id).Any();

            if(updating)
            {
                await UpdateCustomer(Customer);
            }
            else
            {
                await AddCustomer(Customer);
            }

            await GetAllCustomers();
            ToggleShowForm(false);
        }
        #endregion

        private void ToggleShowForm(bool show)
        {
            showForm = show;
        }
    }
}
