<script lang="ts">
  import type { WarehouseItem } from '../../../../types/reports';

  let { data }: { data: WarehouseItem[] } = $props();
</script>

<div class="col-span-1 lg:col-span-12 bg-white border border-outline-variant rounded-xl p-lg shadow-sm">
  <div class="flex justify-between items-center mb-xl">
    <div>
      <h3 class="font-title-lg text-title-lg text-on-surface">Warehouse Stock Turnover</h3>
      <p class="font-label-md text-label-md text-on-surface-variant">Inventory movement speed by location</p>
    </div>
    <div class="flex gap-2">
      <button class="bg-surface-container-low p-2 rounded-lg hover:bg-surface-container-high transition-colors">
        <span class="material-symbols-outlined text-[20px]">more_vert</span>
      </button>
    </div>
  </div>
  <div class="grid grid-cols-6 lg:grid-cols-12 gap-lg items-end h-48 border-b border-outline-variant pb-md">
    {#each data as warehouse (warehouse.name)}
      <div class="flex flex-col items-center gap-2">
        <div class="w-full bg-primary-container/80 rounded-t-sm hover:bg-primary transition-all cursor-pointer" style="height: {warehouse.height}%"></div>
        <span class="text-[10px] font-bold text-on-surface-variant">{warehouse.name}</span>
      </div>
    {/each}
  </div>
</div>