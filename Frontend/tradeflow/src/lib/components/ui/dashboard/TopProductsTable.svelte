<script lang="ts">
  let { products = [] } = $props();
</script>

<div class="bg-surface-container-lowest border border-outline-variant rounded-xl shadow-sm overflow-hidden">
  <div class="p-md lg:p-lg border-b border-outline-variant flex justify-between items-center">
    <h3 class="font-title-lg text-title-lg">Top Selling Products</h3>
    <a href="/products" class="text-primary font-label-md hover:underline text-sm">View All</a>
  </div>
  <div class="overflow-x-auto">
    <table class="w-full text-left min-w-[500px]">
      <thead class="bg-surface-container-low">
        <tr>
          <th class="px-md lg:px-lg py-3 font-label-sm uppercase text-on-surface-variant text-xs">Product Name</th>
          <th class="px-md lg:px-lg py-3 font-label-sm uppercase text-on-surface-variant text-xs">SKU</th>
          <th class="px-md lg:px-lg py-3 font-label-sm uppercase text-on-surface-variant text-xs">Units Sold</th>
          <th class="px-md lg:px-lg py-3 font-label-sm uppercase text-on-surface-variant text-xs">Revenue</th>
          <th class="px-md lg:px-lg py-3 font-label-sm uppercase text-on-surface-variant text-right text-xs">Trend</th>
        </tr>
      </thead>
      <tbody class="divide-y divide-outline-variant">
        {#each products as item (item.sku)}
          <tr class="hover:bg-primary/5 transition-colors cursor-pointer group">
            <td class="px-md lg:px-lg py-3 lg:py-4">
              <div class="flex items-center gap-sm md:gap-md">
                <div class="w-8 h-8 sm:w-10 sm:h-10 rounded-lg bg-surface-container flex items-center justify-center flex-shrink-0">
                  <span class="material-symbols-outlined text-outline text-sm sm:text-base">{item.icon}</span>
                </div>
                <span class="font-body-md font-bold text-sm sm:text-base truncate">{item.name}</span>
              </div>
            </td>
            <td class="px-md lg:px-lg py-3 lg:py-4 text-on-surface-variant text-body-md text-sm">{item.sku}</td>
            <td class="px-md lg:px-lg py-3 lg:py-4 text-body-md text-sm">{item.sold}</td>
            <td class="px-md lg:px-lg py-3 lg:py-4 text-body-md font-bold text-sm">{item.revenue}</td>
            <td class="px-md lg:px-lg py-3 lg:py-4 text-right">
              <span class="{item.trendClass} material-symbols-outlined text-[16px] sm:text-[18px]">{item.trend}</span>
            </td>
          </tr>
        {/each}
      </tbody>
    </table>
  </div>
</div>
