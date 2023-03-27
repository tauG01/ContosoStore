using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoStore.Customers;
using ContosoStore.Merchants;
using AutoMapper.Internal.Mappers;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace ContosoStore.Blazor.Pages;

public partial class Customers
{
    private IReadOnlyList<CustomerDto> CustomerList { get; set; }
    private IReadOnlyList<MerchantDto> MerchantList { get; set; }
    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private CreateCustomerDto NewCustomer { get; set; }

    private Guid EditingCustomerId { get; set; }
    private UpdateCustomerDto EditingCustomer { get; set; }

    private Modal CreateCustomerModal { get; set; }
    private Modal EditCustomerModal { get; set; }

    private Validations CreateValidationsRef;

    private Validations EditValidationsRef;

    public Customers()
    {
        NewCustomer = new CreateCustomerDto();
        EditingCustomer = new UpdateCustomerDto();
    }

    //protected override async Task OnInitializedAsync()
    //{
    //    await GetCustomersAsync();
    //}

    private async Task GetCustomersAsync()
    {
        var result = await CustomerAppService.GetListAsync(
            new GetCustomerListDto
            {
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting
            }
        );

        CustomerList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<CustomerDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");
        CurrentPage = e.Page - 1;

        await GetCustomersAsync();

        await InvokeAsync(StateHasChanged);
    }

    private void OpenCreateCustomerModal()
    {
        CreateValidationsRef.ClearAll();

        NewCustomer = new CreateCustomerDto();
        CreateCustomerModal.Show();
    }

    private void CloseCreateCustomerModal()
    {
        CreateCustomerModal.Hide();
    }

    private void OpenEditCustomerModal(CustomerDto customer)
    {
        EditValidationsRef.ClearAll();

        EditingCustomerId = customer.Id;
        EditingCustomer = ObjectMapper.Map<CustomerDto, UpdateCustomerDto>(customer);
        EditCustomerModal.Show();
    }

    private async Task DeleteCustomerAsync(CustomerDto customer)
    {
        var confirmMessage = L["CustomerDeletionConfirmationMessage", customer.Name];
        if (!await Message.Confirm(confirmMessage))
        {
            return;
        }

        await CustomerAppService.DeleteAsync(customer.Id);
        await GetCustomersAsync();
    }

    private void CloseEditCustomerModal()
    {
        EditCustomerModal.Hide();
    }

    private async Task CreateCustomerAsync()
    {
        if (await CreateValidationsRef.ValidateAll())
        {
            await CustomerAppService.CreateAsync(NewCustomer);
            await GetCustomersAsync();
            CreateCustomerModal.Hide();
        }
    }

    private async Task UpdateCustomerAsync()
    {
        if (await EditValidationsRef.ValidateAll())
        {
            await CustomerAppService.UpdateAsync(EditingCustomerId, EditingCustomer);
            await GetCustomersAsync();
            EditCustomerModal.Hide();
        }
    }
}
