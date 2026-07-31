<script lang="ts">
  import { customersService } from '../../../../services/customers';

  let { isOpen = $bindable(false), onClose, onCustomerAdded, customerToEdit } = $props();

  let name = $state('');
  let type = $state<number>(0);
  let address = $state('');
  let phone = $state('');
  let isLoading = $state(false);
  let error = $state('');
  let isEditMode = $state(false);

  const customerTypes = [
    { value: 0, label: 'Restaurant' },
    { value: 1, label: 'Hotel' },
    { value: 2, label: 'Shop' }
  ];

  $effect(() => {
    if (isOpen) {
      if (customerToEdit) {
        isEditMode = true;
        name = customerToEdit.name;
        type = typeof customerToEdit.type === 'number' ? customerToEdit.type : 0;
        address = customerToEdit.address;
        phone = customerToEdit.phone;
      } else {
        isEditMode = false;
        resetForm();
      }
    }
  });

  function resetForm() {
    name = '';
    type = 0;
    address = '';
    phone = '';
    error = '';
  }

  async function handleSubmit() {
    if (!name || !address || !phone) {
      error = 'Please fill in all required fields';
      return;
    }

    isLoading = true;
    error = '';

    try {
      if (isEditMode && customerToEdit) {
        const updateData: any = {
          id: customerToEdit.id,
          name,
          type,
          address,
          phone
        };
        await customersService.update(customerToEdit.id, updateData);
      } else {
        await customersService.create({
          name,
          type,
          address,
          phone
        });
      }

      resetForm();
      onClose();
      onCustomerAdded();
    } catch (err: any) {
      console.error('Error:', err);
      error = err.response?.data?.detail || `Failed to ${isEditMode ? 'update' : 'create'} customer. Please try again.`;
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
        <h3 class="text-lg font-bold text-gray-900">{isEditMode ? 'Edit Customer' : 'Add New Customer'}</h3>
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
            <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="customerName">Customer Name *</label>
            <input
              bind:value={name}
              class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800"
              id="customerName"
              placeholder="Enter customer name"
              type="text"
            />
          </div>

          <!-- Type -->
          <div class="space-y-1.5">
            <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="customerType">Customer Type *</label>
            <select
              bind:value={type}
              class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800 cursor-pointer"
              id="customerType"
            >
              {#each customerTypes as customerType}
                <option value={customerType.value}>{customerType.label}</option>
              {/each}
            </select>
          </div>

          <!-- Address -->
          <div class="space-y-1.5">
            <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="customerAddress">Address *</label>
            <input
              bind:value={address}
              class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800"
              id="customerAddress"
              placeholder="Enter address"
              type="text"
            />
          </div>

          <!-- Phone -->
          <div class="space-y-1.5">
            <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="customerPhone">Phone *</label>
            <input
              bind:value={phone}
              class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800"
              id="customerPhone"
              placeholder="Enter phone number"
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
            {isEditMode ? 'Updating...' : 'Adding...'}
          {:else}
            <span class="material-symbols-outlined text-[18px]">{isEditMode ? 'edit' : 'add'}</span>
            {isEditMode ? 'Update Customer' : 'Add Customer'}
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
