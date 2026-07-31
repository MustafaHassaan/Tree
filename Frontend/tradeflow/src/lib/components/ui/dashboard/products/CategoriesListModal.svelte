<script lang="ts">
  import { categoriesService } from '../../../../services/categories';

  let { isOpen = $bindable(false), onClose, onEditCategory } = $props();

  let categories = $state<any[]>([]);
  let isLoading = $state(true);
  let error = $state('');
  let currentPage = $state(1);
  const itemsPerPage = 10;

  $effect(() => {
    if (isOpen) {
      loadCategories();
      currentPage = 1;
    }
  });

  async function loadCategories() {
    isLoading = true;
    error = '';
    try {
      console.log('Loading categories...');
      const data = await categoriesService.getAll();
      console.log('Categories loaded:', data);
      categories = data;
    } catch (err: any) {
      error = 'Failed to load categories';
      console.error('Error loading categories:', err);
    } finally {
      isLoading = false;
    }
  }

  function handleClose() {
    onClose();
  }

  function handleEdit(category: any) {
    if (onEditCategory) {
      onEditCategory(category);
      handleClose();
    }
  }

  function nextPage() {
    if (currentPage < totalPages) {
      currentPage++;
    }
  }

  function prevPage() {
    if (currentPage > 1) {
      currentPage--;
    }
  }

  let totalPages = $derived(Math.ceil(categories.length / itemsPerPage));
  let startIndex = $derived((currentPage - 1) * itemsPerPage);
  let endIndex = $derived(startIndex + itemsPerPage);
  let paginatedCategories = $derived(categories.slice(startIndex, endIndex));
</script>

{#if isOpen}
  <div
    class="fixed inset-0 z-[999] flex items-center justify-center bg-black/60 backdrop-blur-sm"
    onclick={handleClose}
    role="dialog"
    aria-modal="true"
    tabindex="-1"
    onkeydown={(e) => e.key === 'Escape' && handleClose()}
  >
    <div
      class="bg-white rounded-2xl shadow-2xl w-[800px] flex flex-col overflow-hidden border border-gray-100"
      role="document"
      onclick={(e) => e.stopPropagation()}
    >
      <!-- Header -->
      <div class="flex items-center justify-between px-6 py-5 border-b border-gray-100 bg-white">
        <h3 class="text-lg font-bold text-gray-900">All Categories</h3>
        <button
          onclick={handleClose}
          class="p-1.5 text-gray-400 hover:text-gray-700 hover:bg-gray-100 rounded-lg transition-colors"
          type="button"
        >
          <span class="material-symbols-outlined text-[20px]">close</span>
        </button>
      </div>

      <!-- Content -->
      <div class="p-6">
        {#if isLoading}
          <div class="flex items-center justify-center py-20">
            <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600"></div>
          </div>
        {:else if error}
          <div class="bg-red-50 text-red-700 p-4 rounded-xl text-sm border border-red-100">
            {error}
          </div>
        {:else if categories.length === 0}
          <div class="text-center py-12 text-gray-500">
            <p class="text-lg">No categories found</p>
          </div>
        {:else}
          <div class="overflow-x-auto">
            <table class="w-full">
              <thead>
                <tr class="border-b border-gray-200">
                  <th class="text-center py-3 px-4 text-xs font-semibold text-gray-700 uppercase tracking-wider">ID</th>
                  <th class="text-center py-3 px-4 text-xs font-semibold text-gray-700 uppercase tracking-wider">Name</th>
                  <th class="text-center py-3 px-4 text-xs font-semibold text-gray-700 uppercase tracking-wider">Actions</th>
                </tr>
              </thead>
              <tbody>
                {#each paginatedCategories as category (category.id)}
                  <tr class="border-b border-gray-100 hover:bg-gray-50 transition-colors">
                    <td class="py-3 px-4 text-sm text-gray-900 font-medium text-center">{category.id}</td>
                    <td class="py-3 px-4 text-sm text-gray-600 text-center">{category.name}</td>
                    <td class="py-3 px-4 text-center">
                      <button
                        onclick={() => handleEdit(category)}
                        class="text-gray-500 hover:text-orange-500 transition-colors cursor-pointer"
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
          {#if totalPages > 1}
            <div class="flex items-center justify-between mt-4 pt-4 border-t border-gray-200">
              <div class="text-sm text-gray-600">
                Showing {startIndex + 1} to {Math.min(endIndex, categories.length)} of {categories.length} categories
              </div>
              <div class="flex gap-2">
                <button
                  onclick={prevPage}
                  disabled={currentPage === 1}
                  class="px-3 py-1.5 text-sm border border-gray-300 rounded-lg hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                  type="button"
                >
                  Previous
                </button>
                <span class="px-3 py-1.5 text-sm text-gray-600">
                  Page {currentPage} of {totalPages}
                </span>
                <button
                  onclick={nextPage}
                  disabled={currentPage === totalPages}
                  class="px-3 py-1.5 text-sm border border-gray-300 rounded-lg hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                  type="button"
                >
                  Next
                </button>
              </div>
            </div>
          {/if}
        {/if}
      </div>

      <!-- Footer -->
      <div class="flex items-center justify-end gap-3 px-6 py-4 border-t border-gray-100 bg-gray-50/50">
        <button
          onclick={handleClose}
          class="px-4 py-2 text-sm text-gray-700 bg-white border border-gray-200 hover:bg-gray-50 rounded-xl transition-colors font-semibold shadow-sm"
          type="button"
        >
          Close
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
