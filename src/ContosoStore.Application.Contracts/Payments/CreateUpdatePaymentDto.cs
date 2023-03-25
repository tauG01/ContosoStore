using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoStore.Payments;

public class CreateUpdatePaymentDto
{
    public Guid CustomerId { get; set; }
    [Required]
    [StringLength(128)]
    public string Reference { get; set; }


    [Required]
    [DataType(DataType.Date)]
    public DateTime PaymentDate { get; set; } = DateTime.Now;

    [Required]
    public string Naration { get; set; }
}
