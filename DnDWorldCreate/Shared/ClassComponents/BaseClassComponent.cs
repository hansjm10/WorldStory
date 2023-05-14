using Microsoft.AspNetCore.Components;

namespace DnDWorldCreate.Shared.ClassComponents
{
    public abstract class BaseClassComponent<TItem, TPrimaryService, TSecondaryService> : ComponentBase
    where TItem : class, new()
    where TPrimaryService : class
    {
        [Inject] protected TPrimaryService? PrimaryService { get; set; }
        [Inject] protected TSecondaryService? SecondaryService { get; set; }

        protected abstract Func<TPrimaryService, TSecondaryService, Task<IEnumerable<TItem>>> GetAllEntitysAsyncFunc { get; }
        protected abstract Func<TPrimaryService, TSecondaryService, Task<IReadOnlyList<TItem>>> GetAllEntitysReadOnlyAsyncFunc { get; }
        protected abstract Func<TPrimaryService, TItem, Task> AddEntityAsyncFunc { get; }
        protected abstract Func<TPrimaryService, TItem, Task> UpdateEntityAsyncFunc { get; }
        protected abstract Func<TPrimaryService, int, Task> DeleteEntityAsyncFunc { get; }
        protected virtual Func<TSecondaryService, int, Task>? DeleteEntityAndChildrenEntitesAsyncFunc { get; }
        protected List<TItem> Entities { get; set; } = new List<TItem>();
        protected TItem? SelectedEntity { get; set; }
        protected bool _selectedEntityIsNew = true;
        protected string ErrorMessage { get; set; }
        public BaseClassComponent() 
        {
            ErrorMessage = string.Empty;
        }
        protected override async Task OnInitializedAsync()
        {
            ErrorMessage = string.Empty;
            await LoadEntitesAsync();
        }

        protected async Task LoadEntitesAsync()
        {
            Entities = (await GetAllEntitysAsyncFunc(PrimaryService ?? throw new InvalidOperationException("PrimaryService is null"), SecondaryService ?? throw new InvalidOperationException("SecondaryService is null"))).ToList();
        }

        protected async Task OnSaveEntityAsync(TItem item)
        {
            ErrorMessage = string.Empty;
            var itemId = GetEntityId(item);

            if (itemId == 0)
            {
                await AddEntityAsyncFunc(PrimaryService!, item);
            }
            else
            {
                await UpdateEntityAsyncFunc(PrimaryService!, item);
            }

            await ReloadFormAsync();
        }

        protected void OnEntitySelected(TItem item)
        {
            ErrorMessage = string.Empty;
            SelectedEntity = item;
            _selectedEntityIsNew = item == null;
        }

        protected async Task OnDeleteEntityAsync(TItem item)
        {
            try
            {
                await DeleteEntityAsyncFunc(PrimaryService!, GetEntityId(item));
            }
            catch(InvalidOperationException ex)
            {
                ErrorMessage = ex.Message;
            }
            await ReloadFormAsync();
        }

        protected async Task ReloadFormAsync()
        {
            await LoadEntitesAsync();

            SelectedEntity = null;
            _selectedEntityIsNew = true;
            StateHasChanged();
        }
        protected async Task OnDeleteEntityAndChildrenEntitesAsync(TItem item)
        {
            if (DeleteEntityAndChildrenEntitesAsyncFunc != null)
            {
                await DeleteEntityAndChildrenEntitesAsyncFunc(SecondaryService!, GetEntityId(item));
                await ReloadFormAsync();
            }
            else
            {
                throw new InvalidOperationException("Unable to delete items.");
            }
        }
        protected abstract int GetEntityId(TItem item);
    }
}
