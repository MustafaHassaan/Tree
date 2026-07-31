<!-- src/routes/dashboard/orders/components/RecentOrdersTable.svelte -->
<script lang="ts">
  type RecentOrder = {
    id: string;
    customer: string;
    date: string;
    amount: number;
    status: string;
    icon: string;
  };

  let { orders = [] }: { orders: RecentOrder[] } = $props();

  let currentPage = $state(1);
  const itemsPerPage = 5;

  let paginatedOrders = $derived(
    orders.slice((currentPage - 1) * itemsPerPage, currentPage * itemsPerPage)
  );

  let totalPages = $derived(Math.ceil(orders.length / itemsPerPage));

  function getStatusClass(status: string) {
    switch (status) {
      case 'Delivered':
        return 'bg-green-100 text-green-700 border-green-200';
      case 'Processing':
        return 'bg-blue-100 text-blue-700 border-blue-200';
      case 'Pending Pay':
        return 'bg-amber-100 text-amber-700 border-amber-200';
      default:
        return 'bg-surface-container-highest text-on-surface-variant';
    }
  }
</script>

<section class="bg-surface-container-lowest border border-outline-variant rounded-xl shadow-sm overflow-hidden">
  <div class="p-lg border-b border-outline-variant bg-surface-container-low/20">
    <h3 class="font-headline-md text-headline-md">Recent Orders</h3>
  </div>
  <div class="w-full overflow-x-auto">
    <table class="w-full text-left border-collapse min-w-[600px]">
      <thead>
        <tr class="bg-surface-container-low border-b border-outline-variant">
          <th class="px-lg py-4 font-label-sm text-label-sm text-on-surface-variant uppercase">Order ID</th>
          <th class="px-lg py-4 font-label-sm text-label-sm text-on-surface-variant uppercase">Customer</th>
          <th class="px-lg py-4 font-label-sm text-label-sm text-on-surface-variant uppercase">Date</th>
          <th class="px-lg py-4 font-label-sm text-label-sm text-on-surface-variant uppercase">Amount</th>
          <th class="px-lg py-4 font-label-sm text-label-sm text-on-surface-variant uppercase">Status</th>
        </tr>
      </thead>
      <tbody class="divide-y divide-outline-variant">
        {#each paginatedOrders as order (order.id)}
          <tr class="hover:bg-surface-container-low/50 transition-colors cursor-pointer">
            <td class="px-lg py-4 font-body-md font-bold text-primary">#{order.id}</td>
            <td class="px-lg py-4">
              <div class="flex items-center gap-3">
                <div class="w-8 h-8 rounded bg-surface-container flex items-center justify-center">
                  <span class="material-symbols-outlined text-[18px]">{order.icon}</span>
                </div>
                <span class="font-body-md">{order.customer}</span>
              </div>
            </td>
            <td class="px-lg py-4 text-on-surface-variant font-body-md">{order.date}</td>
            <td class="px-lg py-4 font-body-md font-bold">${order.amount.toFixed(2)}</td>
            <td class="px-lg py-4">
              <span class="px-3 py-1 {getStatusClass(order.status)} rounded-full text-label-sm font-bold border">{order.status}</span>
            </td>
          </tr>
        {/each}
      </tbody>
    </table>
  </div>
  {#if totalPages > 1}
    <div class="p-md bg-surface-container-low flex justify-center items-center gap-sm">
      <button
        onclick={() => currentPage = Math.max(1, currentPage - 1)}
        disabled={currentPage === 1}
        class="px-3 py-1 rounded border border-outline-variant hover:bg-surface-container disabled:opacity-50 disabled:cursor-not-allowed text-sm"
        type="button"
      >
        Previous
      </button>
      <span class="text-sm text-on-surface-variant">
        Page {currentPage} of {totalPages}
      </span>
      <button
        onclick={() => currentPage = Math.min(totalPages, currentPage + 1)}
        disabled={currentPage === totalPages}
        class="px-3 py-1 rounded border border-outline-variant hover:bg-surface-container disabled:opacity-50 disabled:cursor-not-allowed text-sm"
        type="button"
      >
        Next
      </button>
    </div>
  {/if}
</section>