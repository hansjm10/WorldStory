using Microsoft.AspNetCore.Components;

namespace DnDWorldCreate.Shared.ClassComponents
{
    public abstract class BaseClassComponent<TItem, TService, TSecondService> : ComponentBase
    where TItem : class, new()
    where TService : class
    {
        [Inject] protected TService Service { get; set; }
        [Inject] protected TSecondService SecondService { get; set; }

        protected abstract Func<TService, TSecondService, Task<IEnumerable<TItem>>> GetAllItemsAsync { get; }
        protected abstract Func<TService, TItem, Task> AddItemAsync { get; }
        protected abstract Func<TService, TItem, Task> UpdateItemAsync { get; }
        protected abstract Func<TService, int, Task> DeleteItemAsync { get; }
        protected virtual Func<TSecondService, int, Task> DeleteItemAndChildrenItemsAsync { get; }
        protected List<TItem> Items { get; set; } = new List<TItem>();
        protected TItem SelectedItem { get; set; }
        protected bool _selectedItemIsNew = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadItemsAsync();
        }

        protected async Task LoadItemsAsync()
        {
            Items = (await GetAllItemsAsync(Service, SecondService)).ToList();
        }

        protected async Task OnSaveItem(TItem item)
        {
            var itemId = GetItemId(item);

            if (itemId == 0)
            {
                await AddItemAsync(Service, item);
            }
            else
            {
                await UpdateItemAsync(Service, item);
            }

            await ReloadForm();
        }

        protected void OnItemSelected(TItem item)
        {
            SelectedItem = item;
            _selectedItemIsNew = item == null;
        }

        protected async Task OnDeleteItem(TItem item)
        {
            await DeleteItemAsync(Service, GetItemId(item));

            await ReloadForm();
        }

        protected async Task ReloadForm()
        {
            await LoadItemsAsync();

            SelectedItem = null;
            _selectedItemIsNew = true;
            StateHasChanged();
        }
        protected async Task OnDeleteItemAndChildrenItems(TItem item)
        {
            await DeleteItemAndChildrenItemsAsync(SecondService, GetItemId(item));

            await ReloadForm();
        }
        protected abstract int GetItemId(TItem item);
    }
}
