using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoStore.Merchants;
using AutoMapper.Internal.Mappers;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace ContosoStore.Blazor.Pages;

public partial class Merchants
{
    private IReadOnlyList<MerchantDto> MerchantList { get; set; }

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private CreateMerchantDto NewMerchant { get; set; }

    private Guid EditingMerchantId { get; set; }
    private UpdateMerchantDto EditingMerchant { get; set; }

    private Modal CreateMerchantModal { get; set; }
    private Modal EditMerchantModal { get; set; }

    private Validations CreateValidationsRef;

    private Validations EditValidationsRef;

    public Merchants()
    {
        NewMerchant = new CreateMerchantDto();
        EditingMerchant = new UpdateMerchantDto();
    }

    protected override async Task OnInitializedAsync()
    {
        await GetMerchantsAsync();
    }


    private async Task GetMerchantsAsync()
    {
        var result = await MerchantAppService.GetListAsync(
            new GetMerchantListDto
            {
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting
            }
        );

        MerchantList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<MerchantDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");
        CurrentPage = e.Page - 1;

        await GetMerchantsAsync();

        await InvokeAsync(StateHasChanged);
    }

    private void OpenCreateMerchantModal()
    {
        CreateValidationsRef.ClearAll();

        NewMerchant = new CreateMerchantDto();
        CreateMerchantModal.Show();
    }

    private void CloseCreateMerchantModal()
    {
        CreateMerchantModal.Hide();
    }

    private void OpenEditMerchantModal(MerchantDto merchant)
    {
        EditValidationsRef.ClearAll();

        EditingMerchantId = merchant.Id;
        EditingMerchant = ObjectMapper.Map<MerchantDto, UpdateMerchantDto>(merchant);
        EditMerchantModal.Show();
    }

    private async Task DeleteMerchantAsync(MerchantDto merchant)
    {
        var confirmMessage = L["MerchantDeletionConfirmationMessage", merchant.BusinessName];
        if (!await Message.Confirm(confirmMessage))
        {
            return;
        }

        await MerchantAppService.DeleteAsync(merchant.Id);
        await GetMerchantsAsync();
    }

    private void CloseEditMerchantModal()
    {
        EditMerchantModal.Hide();
    }

    private async Task CreateMerchantAsync()
    {
        if (await CreateValidationsRef.ValidateAll())
        {
            await MerchantAppService.CreateAsync(NewMerchant);
            await GetMerchantsAsync();
            CreateMerchantModal.Hide();
        }
    }

    private async Task UpdateMerchantAsync()
    {
        if (await EditValidationsRef.ValidateAll())
        {
            await MerchantAppService.UpdateAsync(EditingMerchantId, EditingMerchant);
            await GetMerchantsAsync();
            EditMerchantModal.Hide();
        }
    }
}
