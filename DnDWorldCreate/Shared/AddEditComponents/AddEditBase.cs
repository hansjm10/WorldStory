using DnDWorldCreate.Data;
using DnDWorldCreate.Services;
using DnDWorldCreate.Data.Entitys;
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
        [Parameter] public string ErrorMessage { get; set; }

        private int _lastItemId;
        private EditContext EditContext;

        public AddEditBase()
        {
            EditItem = CreateDefaultEditItem();
            EditContext = new EditContext(EditItem);
            ErrorMessage = string.Empty;
            SaveButtonText = "Save";
        }
        protected override void OnParametersSet()
        {
            // Reset the form and create a new EditContext when the EditItem property changes
            if (EditItem != null && (EditContext == null || EditItem.Id != _lastItemId))
            {
                _lastItemId = EditItem.Id;
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
        private static TItem CreateDefaultEditItem()
        {
            // Create a default instance of TItem with default values.
            // This method should be adjusted according to your specific TItem type.
            return Activator.CreateInstance<TItem>();
        }
    }
}
