<script lang="ts">
  import type { Warehouse } from '../../../../types/warehouse';
  import WarehouseModal from './WarehouseModal.svelte';
  import { warehousesService } from '../../../../services/warehouses';

  let {
    warehouses,
    viewMode = $bindable()
  }: {
    warehouses: Warehouse[],
    viewMode: string
  } = $props();

  let modalOpen = $state(false);
  let editingWarehouse = $state<any>(null);
  let isSaving = $state(false);

  async function openAddModal() {
    editingWarehouse = null;
    modalOpen = true;
  }

  async function openEditModal(warehouse: Warehouse) {
    try {
      const warehouseData = await warehousesService.getById(warehouse.id);
      editingWarehouse = warehouseData;
      modalOpen = true;
    } catch (error) {
      console.error('Failed to load warehouse data:', error);
      alert('Failed to load warehouse data. Please try again.');
    }
  }

  function closeModal() {
    modalOpen = false;
    editingWarehouse = null;
  }

  async function handleSave(data: any) {
    try {
      isSaving = true;
      if (editingWarehouse) {
        // Update existing warehouse
        await warehousesService.update(editingWarehouse.id, data);
      } else {
        // Add new warehouse
        await warehousesService.create(data);
      }
      closeModal();
    } catch (error) {
      console.error('Failed to save warehouse:', error);
      alert('Failed to save warehouse. Please try again.');
    } finally {
      isSaving = false;
    }
  }

  function getStatusBgColor(color: string) {
    switch (color) {
      case 'green': return 'bg-green-100 text-green-800';
      case 'blue': return 'bg-blue-100 text-blue-800';
      case 'error': return 'bg-error-container text-on-error-container';
      default: return 'bg-gray-100 text-gray-800';
    }
  }

  function getIconColorClass(color: string) {
    switch (color) {
      case 'primary': return 'text-primary';
      case 'secondary': return 'text-secondary';
      case 'error': return 'text-error';
      default: return 'text-primary';
    }
  }

  function getIconBgClass(color: string) {
    switch (color) {
      case 'primary': return 'bg-surface-container';
      case 'secondary': return 'bg-surface-container';
      case 'error': return 'bg-error/10';
      default: return 'bg-surface-container';
    }
  }
</script>

<div class="bg-surface-container-lowest border border-outline-variant rounded-xl overflow-hidden">
  <div class="p-lg border-b border-outline-variant flex flex-col sm:flex-row justify-between items-start sm:items-center gap-sm">
    <h3 class="font-headline-md text-headline-md text-on-surface">Warehouse Status Overview</h3>
    <button
      onclick={openAddModal}
      class="px-4 py-2 bg-primary text-on-primary rounded-lg font-label-md hover:bg-primary/90 shadow-md transition-all flex items-center gap-2 text-sm"
      type="button"
    >
      <span class="material-symbols-outlined text-[18px]">add</span>
      <span>Add Warehouse</span>
    </button>
  </div>

  <div class="overflow-x-auto">
    <table class="w-full text-left border-collapse min-w-[700px]">
      <thead class="bg-surface-container-low border-b border-outline-variant">
        <tr>
          <th class="px-lg py-4 font-label-sm text-label-sm text-on-surface-variant uppercase tracking-wider">Warehouse Name</th>
          <th class="px-lg py-4 font-label-sm text-label-sm text-on-surface-variant uppercase tracking-wider">Region</th>
          <th class="px-lg py-4 font-label-sm text-label-sm text-on-surface-variant uppercase tracking-wider text-right">Actions</th>
        </tr>
      </thead>
      <tbody class="divide-y divide-outline-variant">
        {#each warehouses as warehouse (warehouse.id)}
          <tr class="hover:bg-primary-container/5 transition-colors cursor-pointer group" style:background-color={warehouse.statusColor === 'error' ? 'rgba(186, 26, 26, 0.05)' : ''}>
            <td class="px-lg py-5">
              <div class="flex items-center gap-3">
                <div class="w-10 h-10 {getIconBgClass(warehouse.iconColor)} rounded-lg flex items-center justify-center">
                  <span class="material-symbols-outlined {getIconColorClass(warehouse.iconColor)}">{warehouse.icon}</span>
                </div>
                <div>
                  <p class="font-bold text-on-surface">{warehouse.name}</p>
                  <p class="text-xs text-on-surface-variant">{warehouse.code}</p>
                </div>
              </div>
            </td>
            <td class="px-lg py-5 text-body-md text-on-surface-variant hidden sm:table-cell">{warehouse.region}</td>
            <td class="px-lg py-5 text-right">
              <button
                onclick={() => openEditModal(warehouse)}
                class="p-2 text-outline hover:text-orange-500 transition-colors opacity-0 group-hover:opacity-100"
                type="button"
              >
                <span class="material-symbols-outlined">edit</span>
              </button>
            </td>
          </tr>
        {/each}
      </tbody>
    </table>
  </div>
</div>

<WarehouseModal bind:isOpen={modalOpen} bind:warehouse={editingWarehouse} onSave={handleSave} />