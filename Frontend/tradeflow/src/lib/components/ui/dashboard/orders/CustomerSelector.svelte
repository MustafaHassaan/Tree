<!-- src/routes/dashboard/orders/components/CustomerSelector.svelte -->
<script lang="ts">
  type Customer = {
    id: number;
    name: string;
    type: string;
    customerId: string;
    icon: string;
  };

  let { customers = [], selectedCustomer = $bindable() }: { customers: Customer[], selectedCustomer: number } = $props();
</script>

<section class="bg-surface-container-lowest border border-outline-variant rounded-xl shadow-sm flex flex-col overflow-hidden h-[500px]">
  <div class="p-lg border-b border-outline-variant flex flex-col sm:flex-row items-start sm:items-center justify-between gap-sm flex-shrink-0">
    <h3 class="font-title-lg text-title-lg flex items-center gap-2">
      <span class="material-symbols-outlined text-primary">person_search</span>
      Customer Selection
    </h3>
    <span class="text-label-md text-primary bg-primary/5 px-2 py-1 rounded text-xs sm:text-sm">Active Session</span>
  </div>
  <div class="p-lg overflow-y-auto custom-scrollbar">
    <div class="grid grid-cols-1 sm:grid-cols-3 gap-md">
      {#each customers as customer (customer.id)}
        <button
          class="flex flex-col items-start p-md border-2 {selectedCustomer === customer.id ? 'border-primary bg-primary/5' : 'border-outline-variant'} rounded-xl text-left transition-all group"
          onclick={() => selectedCustomer = customer.id}
        >
          <div class="flex justify-between w-full mb-2">
            <span class="material-symbols-outlined {selectedCustomer === customer.id ? 'text-primary bg-primary/10' : 'text-outline-variant bg-surface-container group-hover:text-primary group-hover:bg-primary/10'} p-2 rounded-lg">{customer.icon}</span>
            {#if selectedCustomer === customer.id}
              <span class="material-symbols-outlined text-primary">check_circle</span>
            {/if}
          </div>
          <span class="font-label-sm text-label-sm {selectedCustomer === customer.id ? 'text-primary' : 'text-on-surface-variant'} uppercase">{customer.type}</span>
          <span class="font-body-lg text-body-lg font-bold">{customer.name}</span>
          <span class="font-label-md text-label-md text-on-surface-variant">ID: {customer.customerId}</span>
        </button>
      {/each}
    </div>
  </div>
</section>