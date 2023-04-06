using Microsoft.AspNetCore.Components;

public abstract class EnumListComponentBase<TEnum> : ComponentBase where TEnum : Enum
{
    [Parameter] public string Label { get; set; }
    [Parameter] public EventCallback<TEnum> SelectedValueChanged { get; set; }
    [Parameter] public IReadOnlyList<TEnum> EnumValues { get; set; } = new List<TEnum>();
    [Parameter] public TEnum SelectedValue { get; set; }
    protected async Task OnSelect(ChangeEventArgs e)
    {
        if (Enum.TryParse(typeof(TEnum), e.Value.ToString(), out var result))
        {
            SelectedValue = (TEnum)result;
            await SelectedValueChanged.InvokeAsync((TEnum)result);
        }
    }
}
