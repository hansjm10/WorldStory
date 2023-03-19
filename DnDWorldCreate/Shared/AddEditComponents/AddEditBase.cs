using DnDWorldCreate.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DnDWorldCreate.Shared.AddEditComponents
{
    public class AddEditBase<TItem> : ComponentBase where TItem : BaseEntity
    {
        [Parameter] public string SaveButtonText { get; set; }
        [Parameter] public EventCallback<TItem> OnSave { get; set; }
        [Parameter] public EventCallback<TItem> OnDelete { get; set; }
        [Parameter] public EventCallback<TItem> OnDeleteAllInstances { get; set; }
        [Parameter] public TItem EditItem { get; set; }

        private int _lastItemId;
        private EditContext EditContext;

        protected override void OnParametersSet()
        {
            // Reset the form and create a new EditContext when the EditItem property changes
            if (EditContext == null || EditItem != null && EditItem.Id != _lastItemId)
            {
                _lastItemId = EditItem?.Id ?? 0;
                EditContext = new EditContext(EditItem);
            }
        }
        protected async Task SaveForm()
        {
            if (EditContext.Validate())
            {
                await OnSave.InvokeAsync(EditItem);
            }
        }
        protected async Task Delete()
        {
            if (EditContext.Validate())
            {
                await OnDelete.InvokeAsync(EditItem);
            }
        }
    }
}
