<script lang="ts">
  import { onMount } from 'svelte';
  import type { Customer } from '../../../lib/types/cust';
  import { customersService } from '../../../lib/services/customers';
  import CustomerList from '../../../lib/components/ui/dashboard/customers/CustomerList.svelte';
  import CustomerDetail from '../../../lib/components/ui/dashboard/customers/CustomerDetail.svelte';
  import AddCustomerModal from '../../../lib/components/ui/dashboard/customers/AddCustomerModal.svelte';

  let selectedCustomer = $state(0);
  let isLoading = $state(true);
  let error = $state('');
  let isModalOpen = $state(false);
  let customerToEdit = $state<any>(null);

  let customers: Customer[] = $state([]);
  let enrichedCustomers: any[] = $state([]);

  onMount(async () => {
    try {
      customers = await customersService.getAll();

      // Sort customers by ID descending (newest first)
      const sortedCustomers = customers.sort((a, b) => b.id - a.id);

      enrichedCustomers = sortedCustomers.map((c, index) => ({
        ...c,
        type: c.type === 0 ? 'Restaurant' : c.type === 1 ? 'Hotel' : 'Shop',
        location: c.address,
        status: 'Active',
        lastOrder: 'N/A',
        totalSpend: 0,
        icon: c.type === 0 ? 'restaurant' : c.type === 1 ? 'hotel' : 'storefront',
        iconColor: index === 0 ? 'primary' : index === 1 ? 'tertiary' : 'outline',
        accountType: 'Standard Account',
        groupId: `#GHG-${c.id}`,
        contact: {
          manager: 'N/A',
          phone: c.phone,
          email: 'N/A'
        },
        address: {
          street: 'N/A',
          city: c.address,
          zip: 'N/A',
          country: 'N/A',
          taxId: 'N/A',
          creditLimit: 0
        },
        stats: {
          totalOrders: 0,
          lifetimeValue: 0,
          avgOrder: 0
        },
        recentOrders: []
      }));

      if (enrichedCustomers.length > 0) {
        selectedCustomer = enrichedCustomers[0].id;
      }
    } catch (err: any) {
      error = 'Failed to load customers data';
      console.error(err);
    } finally {
      isLoading = false;
    }
  });

  let activeCustomer = $derived(
    enrichedCustomers.find(c => c.id === selectedCustomer)
  );

  function handleAddCustomer() {
    customerToEdit = null;
    isModalOpen = true;
  }

  function handleEditCustomer(customer: any) {
    // Find the original customer from the API data
    const originalCustomer = customers.find(c => c.id === customer.id);
    if (originalCustomer) {
      customerToEdit = originalCustomer;
      isModalOpen = true;
    }
  }

  async function handleCustomerAdded() {
    try {
      customers = await customersService.getAll();

      // Sort customers by ID descending (newest first)
      const sortedCustomers = customers.sort((a, b) => b.id - a.id);

      enrichedCustomers = sortedCustomers.map((c, index) => ({
        ...c,
        type: c.type === 0 ? 'Restaurant' : c.type === 1 ? 'Hotel' : 'Shop',
        location: c.address,
        status: 'Active',
        lastOrder: 'N/A',
        totalSpend: 0,
        icon: c.type === 0 ? 'restaurant' : c.type === 1 ? 'hotel' : 'storefront',
        iconColor: index === 0 ? 'primary' : index === 1 ? 'tertiary' : 'outline',
        accountType: 'Standard Account',
        groupId: `#GHG-${c.id}`,
        contact: {
          manager: 'N/A',
          phone: c.phone,
          email: 'N/A'
        },
        address: {
          street: 'N/A',
          city: c.address,
          zip: 'N/A',
          country: 'N/A',
          taxId: 'N/A',
          creditLimit: 0
        },
        stats: {
          totalOrders: 0,
          lifetimeValue: 0,
          avgOrder: 0
        },
        recentOrders: []
      }));

      if (enrichedCustomers.length > 0) {
        selectedCustomer = enrichedCustomers[0].id;
      }
    } catch (err) {
      console.error('Failed to reload customers:', err);
    }
  }
</script>

{#if isLoading}
  <div class="flex items-center justify-center py-20">
    <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-primary"></div>
  </div>
{:else if error}
  <div class="bg-error-container text-on-error-container p-4 rounded-lg">
    {error}
  </div>
{:else}
  <div class="flex justify-end mb-md">
    <button
      onclick={handleAddCustomer}
      class="flex items-center gap-2 bg-primary text-on-primary px-lg py-2 rounded-lg font-bold hover:opacity-95 transition-opacity shadow-sm text-sm sm:text-base"
      type="button"
    >
      <span class="material-symbols-outlined text-[18px]">add_box</span>
      <span class="hidden sm:inline">Add New Customer</span>
    </button>
  </div>

  <div class="grid grid-cols-1 lg:grid-cols-12 gap-lg">
    <div class="col-span-1 lg:col-span-5">
      <CustomerList
        customers={enrichedCustomers}
        bind:selectedCustomer
      />
    </div>

    <div class="col-span-1 lg:col-span-7">
      {#if activeCustomer}
        <CustomerDetail customer={activeCustomer} onEdit={handleEditCustomer} />
      {/if}
    </div>
  </div>

  <AddCustomerModal
    bind:isOpen={isModalOpen}
    onClose={() => isModalOpen = false}
    onCustomerAdded={handleCustomerAdded}
    customerToEdit={customerToEdit}
  />
{/if}