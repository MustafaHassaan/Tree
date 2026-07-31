<script lang="ts">
  import type { Customer } from '../../../../types/cust';

  let {
    customers,
    selectedCustomer = $bindable()
  }: {
    customers: Customer[],
    selectedCustomer: number
  } = $props();

  function getStatusClass(status: string) {
    switch (status) {
      case 'Active':
        return 'bg-green-100 text-green-700';
      case 'Inactive':
      default:
        return 'bg-surface-container-highest text-on-surface-variant';
    }
  }

  function getIconColorClass(color: string) {
    switch (color) {
      case 'primary':
        return 'bg-primary/10 text-primary';
      case 'tertiary':
        return 'bg-tertiary/10 text-tertiary';
      case 'outline':
      default:
        return 'bg-outline/10 text-outline';
    }
  }
</script>

<!-- Header Section -->
<div class="flex flex-col sm:flex-row justify-between items-start sm:items-end gap-md mb-xl">
  <div>
    <h2 class="font-headline-lg text-headline-lg text-on-surface">Customer Directory</h2>
    <p class="font-body-md text-body-md text-on-surface-variant mt-1">Manage {customers.length} customers</p>
  </div>
</div>

<!-- Client List Cards -->
<div class="bg-surface-container-lowest border border-outline-variant rounded-xl overflow-hidden shadow-sm">
  <div class="p-md border-b border-outline-variant bg-surface-container-low flex justify-between items-center">
    <span class="font-label-sm text-label-sm text-on-surface-variant uppercase tracking-wider">Active Customers</span>
    <span class="font-label-sm text-label-sm text-primary">Sort: Newest First</span>
  </div>
  <div class="divide-y divide-outline-variant max-h-[500px] lg:max-h-[700px] overflow-y-auto no-scrollbar">
    {#each customers as customer (customer.id)}
      <div
        class="p-md hover:bg-surface-container transition-colors cursor-pointer group {selectedCustomer === customer.id ? 'bg-surface-container' : ''}"
        onclick={() => selectedCustomer = customer.id}
      >
        <div class="flex items-start justify-between">
          <div class="flex gap-md">
            <div class="w-12 h-12 rounded-lg {getIconColorClass(customer.iconColor || 'outline')} flex items-center justify-center">
              <span class="material-symbols-outlined">{customer.icon || 'person'}</span>
            </div>
            <div>
              <h4 class="font-title-lg text-title-lg {selectedCustomer === customer.id ? 'text-primary' : 'text-on-surface'} group-hover:text-primary transition-colors">{customer.name}</h4>
              <p class="font-body-md text-body-md text-on-surface-variant">{customer.type} • {customer.location || 'N/A'}</p>
            </div>
          </div>
          <span class="px-2 py-1 rounded {getStatusClass(customer.status || 'Active')} font-label-sm text-label-sm">{customer.status || 'Active'}</span>
        </div>
        <div class="mt-md flex justify-between items-center">
          <span class="font-label-md text-label-md text-on-surface-variant text-xs sm:text-sm">Last Order: <span class="font-bold text-on-surface">{customer.lastOrder || 'N/A'}</span></span>
          <span class="font-title-lg text-title-lg font-bold text-on-surface">${(customer.totalSpend || 0).toFixed(2)}</span>
        </div>
      </div>
    {/each}
  </div>
</div>