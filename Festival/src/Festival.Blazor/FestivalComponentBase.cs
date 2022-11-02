using Festival.Localization;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System;
using Volo.Abp;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.Http.Client;
using Volo.Abp.Validation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Components.Messages;

namespace Festival.Blazor;

public abstract class FestivalComponentBase : AbpComponentBase
{
    [Inject] IUiMessageService UiMessageServicea { get; set; }
    //[Inject] IParametroProvider ParametroProvider { get; set; }
    // public AccionSobreRecord Accion { get; set; }
    public bool FirstDataRead;
    [Parameter] public bool IsCalledByAnotherModule { get; set; }
    public string StyleDetail { get; set; }
    [Inject] IStringLocalizer<FestivalResource> Localization { get; set; }
    protected FestivalComponentBase()
    {
        LocalizationResource = typeof(FestivalResource);
    }

    new public async Task HandleErrorAsync(Exception vEx)
    {
        await InvokeAsync(async () => {
            StringBuilder message = new StringBuilder();
            if ((vEx as AbpValidationException) != null)
            {
                AbpValidationException ValidationException = vEx as AbpValidationException;
                foreach (var detail in ValidationException.ValidationErrors)
                {
                    message.AppendLine(detail.ErrorMessage);
                }
                await UiMessageServicea.Error(message.ToString(), ValidationException.Message);
                StateHasChanged();
            }
            else if ((vEx as AbpRemoteCallException) != null)
            {
                AbpRemoteCallException RemoteCallException = vEx as AbpRemoteCallException;
                if (RemoteCallException.Details.IsNullOrEmpty())
                {
                    await UiMessageServicea.Error(RemoteCallException.Message, null);
                }
                else
                {
                    await UiMessageServicea.Error(RemoteCallException.Details, vEx.Message);
                }
                StateHasChanged();
            }
            else
            {
                UserFriendlyException vex = new UserFriendlyException(vEx.Message, "", "", vEx, Microsoft.Extensions.Logging.LogLevel.Warning);
                foreach (DictionaryEntry detail in vex.Data)
                {
                    message.AppendLine("    Key: {?????0,-20}?????      Value: {?????1}?????" + "'" + detail.Key.ToString() + "'" + detail.Value); ;
                }
                if (vex.Details.IsNullOrEmpty())
                {
                    await UiMessageServicea.Error(vEx.Message, null);
                }
                else
                {
                    await UiMessageServicea.Warn(message.ToString(), vEx.Message);
                }
                StateHasChanged();
            }
        });
    }
}
