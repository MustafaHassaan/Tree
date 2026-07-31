<script lang="ts">
  import { productsService } from '../../../../services/products';
  import { categoriesService } from '../../../../services/categories';

  let { isOpen = $bindable(false), onClose, onProductAdded, productToEdit } = $props();

  let name = $state('');
  let barcode = $state('');
  let price = $state(0);
  let cost = $state(0);
  let categoryId = $state<number | null>(null);
  let isLoading = $state(false);
  let error = $state('');
  let isEditMode = $state(false);

  let categories = $state<{ id: number; name: string }[]>([]);

  $effect(() => {
    if (isOpen) {
      loadCategories();
      if (productToEdit) {
        isEditMode = true;
        name = productToEdit.name;
        barcode = productToEdit.barcode;
        price = productToEdit.price;
        cost = productToEdit.cost;
        categoryId = productToEdit.categoryId;
      } else {
        isEditMode = false;
        resetForm();
      }
    }
  });

  async function loadCategories() {
    try {
      categories = await categoriesService.getAll();
    } catch (err) {
      console.error('Failed to load categories:', err);
    }
  }

  function resetForm() {
    name = '';
    barcode = '';
    price = 0;
    cost = 0;
    categoryId = null;
    error = '';
  }

  async function handleSubmit() {
    if (!name || !barcode || !price || !cost || !categoryId) {
      error = 'Please fill in all required fields';
      return;
    }

    isLoading = true;
    error = '';

    try {
      if (isEditMode && productToEdit) {
        const updateData: any = {
          id: productToEdit.id,
          name,
          barcode,
          price,
          cost,
          categoryId
        };
        console.log('Updating product with ID:', productToEdit.id);
        console.log('Update data:', updateData);
        await productsService.update(productToEdit.id, updateData);
      } else {
        console.log('Creating new product with data:', {
          name,
          barcode,
          price,
          cost,
          categoryId
        });
        await productsService.create({
          name,
          barcode,
          price,
          cost,
          categoryId
        });
      }

      resetForm();
      onClose();
      onProductAdded();
    } catch (err: any) {
      console.error('Error:', err);
      console.error('Error response:', err.response?.data);
      console.error('Full error:', JSON.stringify(err.response?.data, null, 2));
      error = err.response?.data?.detail || `Failed to ${isEditMode ? 'update' : 'create'} product. Please try again.`;
    } finally {
      isLoading = false;
    }
  }

  function handleClose() {
    if (!isLoading) {
      resetForm();
      onClose();
    }
  }
</script>

{#if isOpen}
  <!-- Backdrop -->
  <div
    class="fixed inset-0 z-[999] flex items-center justify-center bg-black/60 backdrop-blur-sm"
    onclick={handleClose}
    role="dialog"
    aria-modal="true"
    tabindex="-1"
    onkeydown={(e) => e.key === 'Escape' && handleClose()}
  >
    <!-- Modal Card -->
    <div
      class="bg-white rounded-2xl shadow-2xl w-[500px] flex flex-col overflow-hidden border border-gray-100"
      role="document"
      onclick={(e) => e.stopPropagation()}
    >
      <!-- Header -->
      <div class="flex items-center justify-between px-6 py-5 border-b border-gray-100 bg-white">
        <h3 class="text-lg font-bold text-gray-900">{isEditMode ? 'Edit Product' : 'Add New Product'}</h3>
        <button
          onclick={handleClose}
          disabled={isLoading}
          class="p-1.5 text-gray-400 hover:text-gray-700 hover:bg-gray-100 rounded-lg transition-colors disabled:opacity-50"
          type="button"
        >
          <span class="material-symbols-outlined text-[20px]">close</span>
        </button>
      </div>

      <!-- Content -->
      <div class="p-6">
        {#if error}
          <div class="bg-red-50 text-red-700 p-4 rounded-xl text-sm border border-red-100 mb-4">
            {error}
          </div>
        {/if}

        <div class="space-y-4">
          <!-- Name -->
          <div class="space-y-1.5">
            <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="productName">Product Name *</label>
            <input
              bind:value={name}
              class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800"
              id="productName"
              placeholder="Enter product name"
              type="text"
            />
          </div>

          <!-- Barcode -->
          <div class="space-y-1.5">
            <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="productBarcode">Barcode *</label>
            <input
              bind:value={barcode}
              class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800"
              id="productBarcode"
              placeholder="Enter barcode"
              type="text"
            />
          </div>

          <!-- Price and Cost -->
          <div class="grid grid-cols-2 gap-4">
            <div class="space-y-1.5">
              <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="productPrice">Price *</label>
              <input
                bind:value={price}
                class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800"
                id="productPrice"
                placeholder="0.00"
                type="number"
                step="0.01"
                min="0"
              />
            </div>
            <div class="space-y-1.5">
              <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="productCost">Cost *</label>
              <input
                bind:value={cost}
                class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800"
                id="productCost"
                placeholder="0.00"
                type="number"
                step="0.01"
                min="0"
              />
            </div>
          </div>

          <!-- Category -->
          <div class="space-y-1.5">
            <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="productCategory">Category *</label>
            <select
              bind:value={categoryId}
              class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800 cursor-pointer"
              id="productCategory"
            >
              <option value={null}>Select a category</option>
              {#each categories as category}
                <option value={category.id}>{category.name}</option>
              {/each}
            </select>
          </div>
        </div>
      </div>

      <!-- Footer -->
      <div class="flex items-center justify-end gap-3 px-6 py-4 border-t border-gray-100 bg-gray-50/50">
        <button
          onclick={handleClose}
          disabled={isLoading}
          class="px-4 py-2 text-sm text-gray-700 bg-white border border-gray-200 hover:bg-gray-50 rounded-xl transition-colors font-semibold shadow-sm disabled:opacity-50"
          type="button"
        >
          Cancel
        </button>
        <button
          onclick={handleSubmit}
          disabled={isLoading}
          class="px-5 py-2 text-sm text-white bg-blue-600 hover:bg-blue-700 rounded-xl transition-colors disabled:opacity-50 font-semibold shadow-sm flex items-center gap-2"
          type="button"
        >
          {#if isLoading}
            <span class="material-symbols-outlined text-[18px] animate-spin">refresh</span>
            {isEditMode ? 'Updating...' : 'Adding...'}
          {:else}
            <span class="material-symbols-outlined text-[18px]">{isEditMode ? 'edit' : 'add'}</span>
            {isEditMode ? 'Update Product' : 'Add Product'}
          {/if}
        </button>
      </div>
    </div>
  </div>
{/if}

<style>
  .material-symbols-outlined {
    font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
  }
</style>
