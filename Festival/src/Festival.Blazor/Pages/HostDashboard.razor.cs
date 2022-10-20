using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.Blazor.Pages.Shared.AverageExecutionDurationPerDayWidget;
using Volo.Abp.AuditLogging.Blazor.Pages.Shared.ErrorRateWidget;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.BlazoriseUI;
using Volo.Abp.Timing;
using Volo.Saas.Host;
using Volo.Saas.Host.Blazor.Pages.Shared.Components.SaasEditionPercentageWidget;
using Volo.Saas.Host.Blazor.Pages.Shared.Components.SaasLatestTenantsWidget;

namespace Festival.Blazor.Pages;

public partial class HostDashboard
{
    [Inject]
    public IPermissionChecker PermissionChecker { get; set; }
    
    protected List<BreadcrumbItem> BreadcrumbItems = new();

    protected AuditLoggingErrorRateWidgetComponent ErrorRateWidgetComponent;
    
    protected AuditLoggingAverageExecutionDurationPerDayWidgetComponent AverageExecutionDurationPerDayWidgetComponent;
    
    protected SaasEditionPercentageWidgetComponent SaasEditionPercentageWidgetComponent;
    
    protected SaasLatestTenantsWidgetComponent SaasLatestTenantsWidgetComponent;
    
    protected DateTime StartDate { get; set; }
    
    protected DateTime EndDate { get; set; }
    
    protected bool HasAuditLoggingPermission { get; set; }
    
    protected bool HasSaasPermission { get; set; }
    
    protected async override Task OnInitializedAsync()
    {
        StartDate = Clock.Now.AddMonths(-1).Date;
        EndDate = Clock.Now.Date;
        HasAuditLoggingPermission = await PermissionChecker.IsGrantedAsync(AbpAuditLoggingPermissions.AuditLogs.Default);
        HasSaasPermission = await PermissionChecker.IsGrantedAsync(SaasHostPermissions.Tenants.Default);
        await SetBreadcrumbItemsAsync();
    }
    
    protected virtual async Task RefreshAsync()
    {
        if (HasAuditLoggingPermission)
        {
            await ErrorRateWidgetComponent.RefreshAsync();
            await AverageExecutionDurationPerDayWidgetComponent.RefreshAsync();
        }

        if (HasSaasPermission)
        {
            await SaasEditionPercentageWidgetComponent.RefreshAsync();
        }
    }

    protected virtual ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new BreadcrumbItem(L["Dashboard"]));
        return ValueTask.CompletedTask;
    }
}