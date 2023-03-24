using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoStore.Customers;

public class CreateCustomerDto
{
    public string Name { get; set; }

    [Required]
    [StringLength(CustomerConsts.MaxNameLength)]
    public string Email { get; set; }
}
