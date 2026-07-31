<script lang="ts">
  import { onMount } from 'svelte';
  import { employeesService } from '../../../../services/employees';
  import { commissionsService } from '../../../../services/commissions';

  let { isOpen = $bindable(false), onClose, onEmployeeAdded, employeeToEdit } = $props();

  let name = $state('');
  let role = $state(0);
  let phone = $state('');
  let password = $state('');
  let commissionId = $state<number | null>(null);
  let isLoading = $state(false);
  let error = $state('');
  let isEditMode = $state(false);

  let commissions = $state<{ id: number; targetAmount: number; percentage: number; notes: string }[]>([]);

  const roles = [
    { value: 0, label: 'Sales Representative' },
    { value: 1, label: 'Manager' },
    { value: 2, label: 'Worker' },
    { value: 3, label: 'Engineer' },
    { value: 4, label: 'Accountant' }
  ];

  $effect(() => {
    if (isOpen) {
      loadCommissions();
      if (employeeToEdit) {
        isEditMode = true;
        name = employeeToEdit.name;
        role = employeeToEdit.role;
        phone = employeeToEdit.phone;
        password = '';
        commissionId = employeeToEdit.commissionId || null;
      } else {
        isEditMode = false;
        resetForm();
      }
    }
  });

  async function loadCommissions() {
    try {
      commissions = await commissionsService.getAll();
    } catch (err) {
      console.error('Failed to load commissions:', err);
    }
  }

  function resetForm() {
    name = '';
    role = 0;
    phone = '';
    password = '';
    commissionId = null;
    error = '';
  }

  async function handleSubmit() {
    if (!name || !phone || (!isEditMode && !password)) {
      error = 'Please fill in all required fields';
      return;
    }

    isLoading = true;
    error = '';

    try {
      if (isEditMode && employeeToEdit) {
        const updateData: any = {
          id: employeeToEdit.id,
          name,
          role,
          phone,
          commissionId: commissionId || undefined
        };
        console.log('Updating employee with ID:', employeeToEdit.id);
        console.log('Update data:', updateData);
        await employeesService.update(employeeToEdit.id, updateData);
      } else {
        console.log('Creating new employee with data:', {
          name,
          role,
          phone,
          password,
          commissionId: commissionId || undefined
        });
        await employeesService.create({
          name,
          role,
          phone,
          password,
          commissionId: commissionId || undefined
        });
      }

      resetForm();
      onClose();
      onEmployeeAdded();
    } catch (err: any) {
      console.error('Error:', err);
      console.error('Error response:', err.response?.data);
      console.error('Full error:', JSON.stringify(err.response?.data, null, 2));
      error = err.response?.data?.detail || `Failed to ${isEditMode ? 'update' : 'create'} employee. Please try again.`;
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
        <h3 class="text-lg font-bold text-gray-900">{isEditMode ? 'Edit Employee' : 'Add New Employee'}</h3>
        <button
          onclick={handleClose}
          disabled={isLoading}
          class="p-1.5 text-gray-400 hover:text-gray-700 hover:bg-gray-100 rounded-lg transition-colors disabled:opacity-50"
          type="button"
        >
          <span class="material-symbols-outlined text-[20px]">close</span>
        </button>
      </div>

      <!-- Form Content -->
      <div class="p-6 space-y-4">
        {#if error}
          <div class="bg-red-50 text-red-700 p-3 rounded-xl text-sm border border-red-100">
            {error}
          </div>
        {/if}

        <!-- Name -->
        <div class="space-y-1.5">
          <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="employeeName">Name *</label>
          <input
            bind:value={name}
            class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800"
            id="employeeName"
            placeholder="Enter employee name"
            type="text"
          />
        </div>

        <!-- Role -->
        <div class="space-y-1.5">
          <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="employeeRole">Role *</label>
          <select
            bind:value={role}
            class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800 cursor-pointer"
            id="employeeRole"
          >
            {#each roles as r}
              <option value={r.value}>{r.label}</option>
            {/each}
          </select>
        </div>

        <!-- Phone -->
        <div class="space-y-1.5">
          <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="employeePhone">Phone *</label>
          <input
            bind:value={phone}
            class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800"
            id="employeePhone"
            placeholder="+1234567890"
            type="tel"
          />
        </div>

        <!-- Password (only in add mode) -->
        {#if !isEditMode}
          <div class="space-y-1.5">
            <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="employeePassword">Password *</label>
            <input
              bind:value={password}
              class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800"
              id="employeePassword"
              placeholder="Enter password"
              type="password"
            />
          </div>
        {/if}

        <!-- Commission -->
        <div class="space-y-1.5">
          <label class="block text-xs font-semibold text-gray-700 uppercase tracking-wider" for="employeeCommission">Commission</label>
          <select
            bind:value={commissionId}
            class="w-full px-3.5 py-2.5 bg-gray-50 border border-gray-200 rounded-xl focus:bg-white focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all text-sm text-gray-800 cursor-pointer"
            id="employeeCommission"
          >
            <option value={null}>No Commission</option>
            {#each commissions as c}
              <option value={c.id}>
                {c.percentage}% - Target: ${c.targetAmount.toLocaleString()}
              </option>
            {/each}
          </select>
        </div>
      </div>

      <!-- Footer -->
      <div class="flex items-center justify-end gap-3 px-6 py-4 border-t border-gray-100 bg-gray-50/50">
        <button
          onclick={handleClose}
          disabled={isLoading}
          class="px-4 py-2 text-sm text-gray-700 bg-white border border-gray-200 hover:bg-gray-50 rounded-xl transition-colors disabled:opacity-50 font-semibold shadow-sm"
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
            {isEditMode ? 'Update Employee' : 'Add Employee'}
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
