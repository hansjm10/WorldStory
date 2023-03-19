using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using DnDWorldCreate.Data;

namespace DnDWorldCreate.Shared.AddEditComponents
{
    public partial class ActionButtons<TItem> : ComponentBase where TItem : BaseEntity
    {
        [Parameter] public EventCallback<TItem> OnDelete { get; set; }
        [Parameter] public EventCallback<TItem> OnDeleteAllInstances { get; set; }
        [Parameter] public EventCallback<TItem> OnSave { get; set; }
        [Parameter] public TItem EditItem { get; set; }
        [Parameter] public string SaveButtonText { get; set; }
        private int _lastItemId;
        private EditContext EditContext;

        protected override void OnParametersSet()
        {
            // Reset the form and create a new EditContext when the EditItem property changes
            if (EditContext == null || (EditItem != null && EditItem.Id != _lastItemId))
            {
                _lastItemId = EditItem?.Id ?? 0;
                EditContext = new EditContext(EditItem);
            }
        }

        protected async Task Delete()
        {
            if (EditContext.Validate())
            {
                await OnDelete.InvokeAsync(EditItem);
            }
        }
        protected async Task DeleteAllInstances()
        {
            if (EditContext.Validate())
            {
                await OnDeleteAllInstances.InvokeAsync(EditItem);
            }
        }
        protected async Task Save()
        {
            if (EditContext.Validate())
            {
                await OnSave.InvokeAsync(EditItem);
            }
        }
    }
}
