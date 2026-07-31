<script lang="ts">
  let { isOpen = $bindable(), warehouse = $bindable(), onSave }: {
    isOpen: boolean,
    warehouse: any,
    onSave: (data: any) => void
  } = $props();

  let formData = $state({
    name: '',
    location: ''
  });

  let isLoading = $state(false);

  $effect(() => {
    if (warehouse) {
      formData = {
        name: warehouse.name || '',
        location: warehouse.region || warehouse.location || ''
      };
    } else {
      formData = {
        name: '',
        location: ''
      };
    }
  });

  function handleSubmit() {
    if (formData.name && formData.location) {
      onSave(formData);
    }
  }

  function handleClose() {
    if (!isLoading) {
      isOpen = false;
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
        <h3 class="text-lg font-bold text-gray-900">{warehouse ? 'Edit Warehouse' : 'Add New Warehouse'}</h3>
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
        <div class="space-y-4">
          <!-- Name -->
          <div class="space-y-1.5">
            <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="warehouseName">Warehouse Name *</label>
            <input
              bind:value={formData.name}
              class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800"
              id="warehouseName"
              placeholder="Enter warehouse name"
              type="text"
            />
          </div>

          <!-- Location -->
          <div class="space-y-1.5">
            <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="warehouseLocation">Location *</label>
            <input
              bind:value={formData.location}
              class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800"
              id="warehouseLocation"
              placeholder="Enter location"
              type="text"
            />
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
            {warehouse ? 'Updating...' : 'Adding...'}
          {:else}
            <span class="material-symbols-outlined text-[18px]">{warehouse ? 'edit' : 'add'}</span>
            {warehouse ? 'Update Warehouse' : 'Add Warehouse'}
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
