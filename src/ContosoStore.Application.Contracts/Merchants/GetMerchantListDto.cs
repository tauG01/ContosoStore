using System;
using Volo.Abp.Application.Dtos;

namespace ContosoStore.Merchants;

[Serializable]
public class GetMerchantListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
