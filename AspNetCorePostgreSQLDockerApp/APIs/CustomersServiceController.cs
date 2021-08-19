using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePostgreSQLDockerApp.Apis
{
    [Route("api/[controller]/customers")]
    public class CustomersServiceController : ControllerBase
    {
        private readonly ICustomersRepository _repo;

        public CustomersServiceController(ICustomersRepository repo)
        {
            _repo = repo;
        }

        // GET api/CustomersService/customers
        [HttpGet]
        [ProducesResponseType(typeof(List<Customer>), 200)]
        [ProducesResponseType(typeof(List<Customer>), 404)]
        public async Task<ActionResult> Customers()
        {
            var customers = await _repo.GetCustomersAsync();
            if (customers == null) return NotFound();

            return Ok(customers);
        }
        
        [HttpGet("search")]
        [ProducesResponseType(typeof(List<Customer>), 200)]
        [ProducesResponseType(typeof(List<Customer>), 404)]
        public async Task<ActionResult> SearchCustomers([FromQuery] string email)
        {
            var customers = await _repo.SearchCustomerByEmail(email);
            if (customers == null) return NotFound();

            return Ok(customers);
        }

        // GET api/CustomersService/customers/5
        [HttpGet("{id}", Name = "GetCustomersRoute")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(Customer), 404)]
        public async Task<ActionResult> Customers(int id)
        {
            var customer = await _repo.GetCustomerAsync(id);
            if (customer == null) return NotFound();

            return Ok(customer);
        }

        // POST api/CustomersService/customers
        [HttpPost]
        [ProducesResponseType(typeof(Customer), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newCustomer = await _repo.InsertCustomerAsync(customer);
            if (newCustomer == null) return BadRequest("Unable to insert customer");

            return CreatedAtRoute("GetCustomersRoute", new { id = newCustomer.Id }, newCustomer);
        }

        // PUT api/CustomersService/customers/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(bool), 400)]
        public async Task<ActionResult> PutCustomer(int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var status = await _repo.UpdateCustomerAsync(customer);
            if (!status) return BadRequest("Unable to update customer");

            return Ok(status);
        }

        // DELETE api/CustomersService/customers/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(bool), 404)]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var status = await _repo.DeleteCustomerAsync(id);
            if (!status) return NotFound();

            return Ok(status);
        }

        [HttpGet("states")]
        [ProducesResponseType(typeof(List<State>), 200)]
        [ProducesResponseType(typeof(List<State>), 404)]
        public async Task<ActionResult> States()
        {
            var states = await _repo.GetStatesAsync();
            if (states == null) return NotFound();

            return Ok(states);
        }
    }

    public static class HttpRequestExtensions
    {
        public static Uri ToUri(this HttpRequest request)
        {
            var hostComponents = request.Host.ToUriComponent().Split(':');

            var builder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = hostComponents[0],
                Path = request.Path,
                Query = request.QueryString.ToUriComponent()
            };

            if (hostComponents.Length == 2) builder.Port = Convert.ToInt32(hostComponents[1]);

            return builder.Uri;
        }
    }
}