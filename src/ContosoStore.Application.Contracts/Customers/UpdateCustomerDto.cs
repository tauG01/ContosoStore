using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoStore.Customers;

public class UpdateCustomerDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    [StringLength(CustomerConsts.MaxNameLength)]
    public string Email { get; set; }
}

