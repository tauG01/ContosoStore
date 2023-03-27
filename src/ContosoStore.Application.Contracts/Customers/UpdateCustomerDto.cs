using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoStore.Customers;

public class UpdateCustomerDto
{
    public Guid MerchantId { get; set; }
    [Required]
    [StringLength(CustomerConsts.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    [StringLength(CustomerConsts.MaxEmailLength)]
    public string Email { get; set; }
}

