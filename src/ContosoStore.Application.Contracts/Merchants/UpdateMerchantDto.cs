using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoStore.Merchants;

public class UpdateMerchantDto
{
    [Required]
    [StringLength(MerchantConsts.MaxBusinessNameLength)]
    public string BusinessName { get; set; }

    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(MerchantConsts.MaxBusinessEmailLength)]
    public string Email { get; set; }
}

