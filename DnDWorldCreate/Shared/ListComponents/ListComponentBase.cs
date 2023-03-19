using DnDWorldCreate.Data;
using Microsoft.AspNetCore.Components;

namespace DnDWorldCreate.Shared.ListComponents
{
    public class ListComponentBase<TItem> : ComponentBase where TItem : BaseEntity
    {
        [Parameter] public string Label { get; set; }
        [Parameter] public EventCallback<TItem> OnSelected { get; set; }
        [Parameter] public IEnumerable<TItem> Items { get; set; }
        [Parameter] public TItem SelectedValue { get; set; }
        [Parameter] public bool ItemsChanged { get; set; }
        protected async Task OnSelectChanged(ChangeEventArgs e)
        {
            var selectedId = int.Parse(e.Value.ToString());
            SelectedValue = Items.FirstOrDefault(item => item.Id == selectedId);
            await OnSelected.InvokeAsync(SelectedValue);
        }
        protected async Task OnItemsChanged()
        {
            if (Items != null && Items.Count() > 0)
            {
                await OnSelected.InvokeAsync(SelectedValue);
            }
        }
    }

}