<script lang="ts">
  import type { Customer } from '../../../../types/cust';

  let { customer, onEdit }: { customer: Customer; onEdit?: (customer: Customer) => void } = $props();

  function getOrderStatusClass(status: string) {
    switch (status) {
      case 'Processing':
        return 'bg-blue-100 text-blue-700';
      case 'Delivered':
        return 'bg-green-100 text-green-700';
      default:
        return 'bg-surface-container-highest text-on-surface-variant';
    }
  }
</script>

<div class="bg-surface-container-lowest border border-outline-variant rounded-xl shadow-sm overflow-hidden sticky top-20 lg:top-24">
  <div class="p-lg bg-surface-container-low border-b border-outline-variant relative overflow-hidden">
    <div class="absolute right-0 top-0 w-48 h-full lg:w-64">
      <div class="w-full h-full bg-cover opacity-10 grayscale" style="background-image: url('https://lh3.googleusercontent.com/aida-public/AB6AXuAUJ5eepXAYKLyy-SqPUokr4uf8SQwMgmGrfVvliCCI3DaEbmfADWFVmu3MYrrNpePza8z098lRdqY2y_ta0gi6LoHAwkQ9AhrGF18Q9Q6Sc8X6fPJ1YsGiLpGmdhKTjdIOfZo8C-a2zCvyINleBViQTLrRP2uD6tFkDb6i-IAgyj-j_JP8utnTA_V3vG3Dwtrzo8xMJ_VUDcIFD11ra2_mxHwTWf0iKlMmQg7TAQauikEDGyBErJRs8nVcmh00kEdt-2iokMR7mcI')"></div>
    </div>
    <div class="relative z-10 flex flex-col md:flex-row md:items-center justify-between gap-md">
      <div class="flex items-center gap-lg">
        <div class="w-16 h-16 sm:w-20 sm:h-20 rounded-2xl bg-primary text-on-primary flex items-center justify-center text-4xl shadow-lg">
          <span class="material-symbols-outlined text-3xl sm:text-4xl">{customer.icon}</span>
        </div>
        <div>
          <span class="font-label-sm text-label-sm px-2 py-1 rounded bg-primary-fixed-dim text-on-primary-fixed-variant mb-2 inline-block uppercase tracking-widest text-xs">{customer.accountType}</span>
          <h3 class="font-display-lg text-display-lg text-on-surface">{customer.name}</h3>
          <p class="font-body-lg text-body-lg text-on-surface-variant">Global Hospitality Group ID: {customer.groupId}</p>
        </div>
      </div>
      <div class="flex gap-sm">
        <button
          onclick={() => onEdit?.(customer)}
          class="p-3 bg-surface-container-lowest rounded-full border border-outline-variant hover:shadow-md transition-all cursor-pointer"
          type="button"
        >
          <span class="material-symbols-outlined text-primary hover:text-orange-500 transition-colors">edit</span>
        </button>
      </div>
    </div>
  </div>

  <div class="p-lg grid grid-cols-1 md:grid-cols-2 gap-lg">
    <!-- Contact Info -->
    <div class="col-span-1 md:col-span-1">
      <h5 class="font-label-sm text-label-sm text-outline uppercase mb-md tracking-wider">Primary Contact</h5>
      <div class="space-y-md">
        <div class="flex items-center gap-md">
          <span class="material-symbols-outlined text-primary">person</span>
          <div>
            <p class="font-body-md text-body-md text-on-surface-variant">Manager Name</p>
            <p class="font-title-lg text-title-lg text-on-surface">{customer.contact?.manager || 'N/A'}</p>
          </div>
        </div>
        <div class="flex items-center gap-md">
          <span class="material-symbols-outlined text-primary">call</span>
          <div>
            <p class="font-body-md text-body-md text-on-surface-variant">Phone Number</p>
            <p class="font-title-lg text-title-lg text-on-surface">{customer.contact?.phone || customer.phone}</p>
          </div>
        </div>
        <div class="flex items-center gap-md">
          <span class="material-symbols-outlined text-primary">mail</span>
          <div>
            <p class="font-body-md text-body-md text-on-surface-variant">Email Address</p>
            <p class="font-title-lg text-title-lg text-on-surface">{customer.contact?.email || 'N/A'}</p>
          </div>
        </div>
      </div>
    </div>

    <!-- Billing Info -->
    <div class="col-span-1 md:col-span-1">
      <h5 class="font-label-sm text-label-sm text-outline uppercase mb-md tracking-wider">Billing Address</h5>
      <div class="bg-surface-container-low p-md rounded-lg border border-outline-variant/30 flex gap-md">
        <span class="material-symbols-outlined text-on-surface-variant">location_on</span>
        <div>
          <p class="font-body-lg text-body-lg text-on-surface font-medium leading-relaxed">
            {customer.addressDetail?.street || 'N/A'},<br/>
            {customer.addressDetail?.city || customer.address || 'N/A'},<br/>
            {customer.addressDetail?.zip || 'N/A'}, {customer.addressDetail?.country || 'N/A'}
          </p>
          <div class="mt-md">
            <p class="font-label-md text-label-md text-on-surface-variant">Tax ID: {customer.addressDetail?.taxId || 'N/A'}</p>
            <p class="font-label-md text-label-md text-on-surface-variant">Credit Limit: ${customer.addressDetail?.creditLimit?.toFixed(2) || '0.00'}</p>
          </div>
        </div>
      </div>
    </div>

    <!-- Order History Summary -->
    <div class="col-span-1 md:col-span-2">
      <h5 class="font-label-sm text-label-sm text-outline uppercase mb-md tracking-wider">Order History Summary</h5>
      <div class="grid grid-cols-3 gap-md">
        <div class="bg-surface-container-lowest p-md rounded-xl border border-outline-variant text-center">
          <p class="font-label-md text-label-md text-on-surface-variant">Total Orders</p>
          <p class="font-display-lg text-display-lg text-primary">{customer.stats?.totalOrders || 0}</p>
        </div>
        <div class="bg-surface-container-lowest p-md rounded-xl border border-outline-variant text-center">
          <p class="font-label-md text-label-md text-on-surface-variant">Lifetime Value</p>
          <p class="font-display-lg text-display-lg text-primary">${((customer.stats?.lifetimeValue || 0) / 1000).toFixed(1)}k</p>
        </div>
        <div class="bg-surface-container-lowest p-md rounded-xl border border-outline-variant text-center">
          <p class="font-label-md text-label-md text-on-surface-variant">Avg. Order</p>
          <p class="font-display-lg text-display-lg text-primary">${customer.stats?.avgOrder || 0}</p>
        </div>
      </div>

      <div class="mt-lg border border-outline-variant rounded-xl overflow-hidden">
        <div class="w-full overflow-x-auto">
          <table class="w-full text-left min-w-[400px]">
            <thead class="bg-surface-container-low">
              <tr>
                <th class="px-md py-3 font-label-sm text-label-sm text-on-surface-variant uppercase">Order ID</th>
                <th class="px-md py-3 font-label-sm text-label-sm text-on-surface-variant uppercase">Date</th>
                <th class="px-md py-3 font-label-sm text-label-sm text-on-surface-variant uppercase">Amount</th>
                <th class="px-md py-3 font-label-sm text-label-sm text-on-surface-variant uppercase text-right">Status</th>
              </tr>
            </thead>
            <tbody class="divide-y divide-outline-variant">
              {#if customer.recentOrders && customer.recentOrders.length > 0}
                {#each customer.recentOrders as order (order.id)}
                  <tr class="hover:bg-primary/5 transition-colors">
                    <td class="px-md py-3 font-body-md text-body-md font-bold">#{order.id}</td>
                    <td class="px-md py-3 font-body-md text-body-md text-on-surface-variant">{order.date}</td>
                    <td class="px-md py-3 font-body-md text-body-md">${order.amount.toFixed(2)}</td>
                    <td class="px-md py-3 text-right">
                      <span class="px-2 py-1 rounded {getOrderStatusClass(order.status)} font-label-sm text-label-sm">{order.status}</span>
                    </td>
                  </tr>
                {/each}
              {:else}
                <tr>
                  <td colspan="4" class="px-md py-3 text-center text-on-surface-variant">No orders yet</td>
                </tr>
              {/if}
            </tbody>
          </table>
        </div>
        <div class="p-md text-center bg-surface-container-low border-t border-outline-variant">
          <button class="text-primary font-bold text-body-md hover:underline">View Full Order History</button>
        </div>
      </div>
    </div>
  </div>
</div>