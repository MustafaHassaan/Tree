<script lang="ts">
  import { onMount } from 'svelte';
  import type { Warehouse, RestockItem } from '../../../lib/types/warehouse';
  import { warehousesService } from '../../../lib/services/warehouses';
  import WarehouseTable from '../../../lib/components/ui/dashboard/warehouses/WarehouseTable.svelte';

  let transferModalOpen = $state(false);
  let viewMode = $state('list');
  let isLoading = $state(true);
  let error = $state('');

  let warehouses: Warehouse[] = $state([]);
  let restockItems: RestockItem[] = $state([]);

  onMount(async () => {
    try {
      const data = await warehousesService.getAll();
      warehouses = data
        .sort((a, b) => b.id - a.id) // Sort descending by ID
        .map((w, index) => ({
          id: w.id,
          name: w.name,
          code: `Hub #${w.id}`,
          region: w.location,
          utilization: Math.floor(Math.random() * 80) + 20,
          totalSkus: Math.floor(Math.random() * 4000) + 500,
          status: Math.random() > 0.5 ? 'Optimal' : Math.random() > 0.3 ? 'Healthy' : 'Low Stock',
          statusColor: Math.random() > 0.5 ? 'green' : Math.random() > 0.3 ? 'blue' : 'error',
          lastAudit: `${Math.floor(Math.random() * 5) + 1}h ago`,
          icon: index === 0 ? 'north' : index === 1 ? 'south' : 'center_focus_strong',
          iconColor: index === 0 ? 'primary' : index === 1 ? 'secondary' : 'error'
        }));

      // Mock restock items for now (will be replaced with API later)
      restockItems = [
        {
          id: 1,
          sku: 'IND-9203-X',
          name: 'Titanium Drill Bit Set (12pc)',
          currentStock: 42,
          image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuBucBglnIhpavR8eFFkXRccvFs0mpwFDdK5pnW9ku7Z7cZt8qg2Fy2UAVOVOooE97xfwTFbVHYQMiUdXQlVbccw-iPd0CUrbnKpukJTvj9Pf8nA6zSoRzhztT_QA5e57PkFRm9hZaIEDo5Y4aozzMgVXJzGEKg529ipU0pvGy8FiaDCQY_FKgPhT_gRnZA_UhydQSFmYD1cKAQACxTSDbj8pIwLP5FG-HWm3TtHk6T9po88aPCsxpDfsF_5h8_ugafzpU8kxNSgCO0',
          isCritical: true
        },
        {
          id: 2,
          sku: 'HYD-8822-Y',
          name: 'Industrial Hydraulic Jack (5 Ton)',
          currentStock: 15,
          image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuCqErGUNndRK8Vr0FFIsjLKNZ667NHY3AEUPjkW6CNyTgDwJ16xsULp95zcAjHuzMGN2nYMA2h1kXvoiKcav5TIwEueHmk9PkvVhqR0T5DprTUgo9aff85DtZw13G6907EGjyTSqVdyMSYDoW7lXNQgR9WyrqhJOY10KS0lU0I6VvdL8_R2uWOk8AZAXYv0N--6L2RkMMW7tvLy7OlbvZqRFnb0FJVLV34BCYcPdl2V9tHJLNcBUPSIjauDR_qTQZCipoTcgsRdmYw',
          isCritical: false
        }
      ];
    } catch (err: any) {
      error = 'Failed to load warehouses data';
      console.error(err);
    } finally {
      isLoading = false;
    }
  });

  function openModal() {
    transferModalOpen = true;
  }

  function closeModal() {
    transferModalOpen = false;
  }
</script>

<div class="space-y-xl">
  <!-- Page Header -->
  <div class="flex flex-col sm:flex-row justify-between items-start sm:items-end gap-md">
    <div>
      <h2 class="font-display-lg text-display-lg text-on-surface">Warehouse & Inventory</h2>
      <p class="font-body-lg text-body-lg text-on-surface-variant">Real-time cross-location stock monitoring and allocation.</p>
    </div>
  </div>

  {#if isLoading}
    <div class="flex items-center justify-center py-20">
      <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-primary"></div>
    </div>
  {:else if error}
    <div class="bg-error-container text-on-error-container p-4 rounded-lg">
      {error}
    </div>
  {:else}
    <!-- Warehouse Status Table -->
    <WarehouseTable {warehouses} bind:viewMode />

  {/if}
</div>