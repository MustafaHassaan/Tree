<script lang="ts">
  import Dropdown from '../../dropdown/Dropdown.svelte';

  let { onAddCategory, onViewCategories, onAddProduct } = $props();
</script>

<!-- src/routes/dashboard/products/components/ProductHeader.svelte -->
<div class="flex flex-col sm:flex-row justify-between items-start sm:items-end gap-md mb-lg">
  <div>
    <nav class="flex items-center gap-2 text-on-surface-variant font-label-md text-label-md mb-2">
      <span>Inventory</span>
      <span class="material-symbols-outlined text-[14px]">chevron_right</span>
      <span class="text-primary font-bold">Product Catalog</span>
    </nav>
    <h2 class="font-display-lg text-display-lg text-on-surface">Product Inventory</h2>
  </div>
  <div class="flex flex-wrap gap-sm sm:gap-md w-full sm:w-auto">
    <Dropdown>
      {#snippet trigger()}
        <button class="flex items-center gap-2 bg-primary text-on-primary px-lg py-2 rounded-lg font-bold hover:opacity-95 transition-opacity shadow-sm text-sm sm:text-base">
          <span class="material-symbols-outlined text-[18px]">category</span>
          <span class="hidden sm:inline">Category Actions</span>
        </button>
      {/snippet}

      {#snippet children()}
        <button
          onclick={onAddCategory}
          class="w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 transition-colors"
          type="button"
        >
          Add New Category
        </button>
        <button
          onclick={onViewCategories}
          class="w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 transition-colors"
          type="button"
        >
          View All Categories
        </button>
      {/snippet}
    </Dropdown>

    <button
      onclick={onAddProduct}
      class="flex items-center gap-2 bg-primary text-on-primary px-lg py-2 rounded-lg font-bold hover:opacity-95 transition-opacity shadow-sm text-sm sm:text-base"
      type="button"
    >
      <span class="material-symbols-outlined text-[18px]">add_box</span>
      <span class="hidden sm:inline">Add New Product</span>
    </button>
  </div>
</div>