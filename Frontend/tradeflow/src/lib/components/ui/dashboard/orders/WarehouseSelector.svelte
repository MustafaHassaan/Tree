<!-- src/lib/components/ui/dashboard/orders/WarehouseSelector.svelte -->
<script lang="ts">
  type Warehouse = {
    id: number;
    name: string;
    location: string;
  };

  let { warehouses = [], selectedWarehouse = $bindable(), onChange }: { warehouses: Warehouse[], selectedWarehouse: number, onChange?: (warehouseId: number) => void } = $props();
</script>

<section class="bg-surface-container-lowest border border-outline-variant rounded-xl p-lg shadow-sm flex-shrink-0">
  <div class="flex flex-col sm:flex-row items-start sm:items-center justify-between mb-md gap-sm">
    <h3 class="font-title-lg text-title-lg flex items-center gap-2">
      <span class="material-symbols-outlined text-primary">warehouse</span>
      Warehouse Selection
    </h3>
    <span class="text-label-md text-primary bg-primary/5 px-2 py-1 rounded text-xs sm:text-sm">Stock Source</span>
  </div>
  <div class="grid grid-cols-1 sm:grid-cols-3 gap-md">
    {#each warehouses as warehouse (warehouse.id)}
      <button
        class="flex flex-col items-start p-md border-2 {selectedWarehouse === warehouse.id ? 'border-primary bg-primary/5' : 'border-outline-variant'} rounded-xl text-left transition-all group"
        onclick={() => {
          selectedWarehouse = warehouse.id;
          onChange?.(warehouse.id);
        }}
      >
        <div class="flex justify-between w-full mb-2">
          <span class="material-symbols-outlined {selectedWarehouse === warehouse.id ? 'text-primary bg-primary/10' : 'text-outline-variant bg-surface-container group-hover:text-primary group-hover:bg-primary/10'} p-2 rounded-lg">warehouse</span>
          {#if selectedWarehouse === warehouse.id}
            <span class="material-symbols-outlined text-primary">check_circle</span>
          {/if}
        </div>
        <span class="font-label-sm text-label-sm {selectedWarehouse === warehouse.id ? 'text-primary' : 'text-on-surface-variant'} uppercase">Warehouse</span>
        <span class="font-body-lg text-body-lg font-bold">{warehouse.name}</span>
        <span class="font-label-md text-label-md text-on-surface-variant">{warehouse.location}</span>
      </button>
    {/each}
  </div>
</section>
