<!-- src/routes/dashboard/products/components/ProductTable.svelte -->
<script lang="ts">
  let { products, currentPage = $bindable(1), rowsPerPage = $bindable(15), onEditProduct } = $props();

  function handleEdit(product: any) {
    if (onEditProduct) {
      onEditProduct(product);
    }
  }

  function nextPage() {
    currentPage++;
  }

  function prevPage() {
    if (currentPage > 1) {
      currentPage--;
    }
  }

  let totalPages = $derived(Math.ceil(products.length / rowsPerPage));
  let startIndex = $derived((currentPage - 1) * rowsPerPage);
  let endIndex = $derived(startIndex + rowsPerPage);
  let paginatedProducts = $derived(products.slice(startIndex, endIndex));
</script>

<div class="bg-surface-container-lowest rounded-xl border border-outline-variant shadow-sm overflow-hidden">
  <div class="overflow-x-auto">
    <table class="w-full text-left border-collapse min-w-[800px]">
      <thead class="bg-surface-container-low border-b border-outline-variant">
        <tr>
          <th class="px-lg py-md font-label-sm text-label-sm text-outline uppercase tracking-wider">Product Info</th>
          <th class="px-md py-md font-label-sm text-label-sm text-outline uppercase tracking-wider">Category</th>
          <th class="px-md py-md font-label-sm text-label-sm text-outline uppercase tracking-wider">Barcode/SKU</th>
          <th class="px-md py-md font-label-sm text-label-sm text-outline uppercase tracking-wider text-right">Price</th>
          <th class="px-lg py-md font-label-sm text-label-sm text-outline uppercase tracking-wider text-right">Actions</th>
        </tr>
      </thead>
      <tbody class="divide-y divide-outline-variant">
        {#each paginatedProducts as product (product.id)}
          <tr class="hover:bg-primary-fixed/5 transition-colors group">
            <td class="px-lg py-md">
              <div>
                <div class="font-title-lg text-[14px] text-on-surface">{product.name}</div>
                <div class="text-[12px] text-on-surface-variant font-label-md">SKU: {product.sku}</div>
              </div>
            </td>
            <td class="px-md py-md font-body-md text-body-md">{product.category}</td>
            <td class="px-md py-md font-body-md text-body-md">{product.barcode}</td>
            <td class="px-md py-md text-right font-bold text-on-surface">${product.price.toFixed(2)}</td>
            <td class="px-lg py-md text-right">
              <button
                onclick={(e) => { e.stopPropagation(); handleEdit(product); }}
                class="p-2 text-gray-500 hover:text-orange-500 transition-colors cursor-pointer"
                type="button"
                title="Edit"
              >
                <span class="material-symbols-outlined text-[20px]">edit</span>
              </button>
            </td>
          </tr>
        {/each}
      </tbody>
    </table>
  </div>

  <!-- Pagination -->
  <div class="bg-surface-container-low/50 px-lg py-md flex flex-col sm:flex-row items-center justify-between gap-md sm:gap-0 border-t border-outline-variant">
    <div class="flex items-center gap-md w-full sm:w-auto justify-center sm:justify-start">
      <span class="font-label-md text-label-md text-on-surface-variant text-xs sm:text-sm">Rows per page:</span>
      <select
        class="bg-transparent border-none font-bold text-label-md text-primary focus:ring-0 text-sm"
        bind:value={rowsPerPage}
      >
        <option value={15}>15</option>
        <option value={30}>30</option>
        <option value={50}>50</option>
      </select>
      <span class="text-sm text-on-surface-variant">
        Showing {startIndex + 1} to {Math.min(endIndex, products.length)} of {products.length}
      </span>
    </div>
    <div class="flex items-center gap-md w-full sm:w-auto justify-center sm:justify-end">
      <button
        onclick={prevPage}
        disabled={currentPage === 1}
        class="p-1 text-outline hover:text-primary disabled:opacity-30"
        type="button"
      >
        <span class="material-symbols-outlined">chevron_left</span>
      </button>
      <span class="text-sm text-on-surface-variant">
        Page {currentPage} of {totalPages || 1}
      </span>
      <button
        onclick={nextPage}
        disabled={currentPage >= totalPages}
        class="p-1 text-outline hover:text-primary disabled:opacity-30"
        type="button"
      >
        <span class="material-symbols-outlined">chevron_right</span>
      </button>
    </div>
  </div>
</div>