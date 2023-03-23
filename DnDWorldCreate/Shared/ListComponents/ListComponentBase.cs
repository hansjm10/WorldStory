using DnDWorldCreate.Data.Entitys;
using Microsoft.AspNetCore.Components;

namespace DnDWorldCreate.Shared.ListComponents
{
    public class ListComponentBase<TItem> : ComponentBase where TItem : BaseEntity
    {
        [Parameter] public string Label { get; set; }
        [Parameter] public EventCallback<TItem> OnSelected { get; set; }
        [Parameter] public IEnumerable<TItem> Items { get; set; } = new List<TItem>();
        [Parameter] public TItem SelectedValue { get; set; }
        [Parameter] public bool ItemsChanged { get; set; }
        public ListComponentBase() 
        {
            SelectedValue = CreateDefaultSelectedItem();
            Label = string.Empty;
        }
        protected async Task OnSelectChanged(ChangeEventArgs e)
        {
            int.TryParse(e.Value?.ToString(), out var selectedID);
            SelectedValue = Items.FirstOrDefault(item => item.Id == selectedID) ?? throw new InvalidOperationException("Item not found");
            await OnSelected.InvokeAsync(SelectedValue);
        }
        protected async Task OnItemsChanged()
        {
            if (Items != null && Items.Count() > 0)
            {
                await OnSelected.InvokeAsync(SelectedValue);
            }
        }
        private static TItem CreateDefaultSelectedItem()
        {
            // Create a default instance of TItem with default values.
            // This method should be adjusted according to your specific TItem type.
            return Activator.CreateInstance<TItem>();
        }
    }

}