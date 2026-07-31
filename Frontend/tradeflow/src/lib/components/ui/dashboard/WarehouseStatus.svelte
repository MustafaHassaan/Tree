<script lang="ts">
  let { warehouses = [] } = $props();
</script>

<div class="bg-surface-container-lowest p-md lg:p-lg border border-outline-variant rounded-xl shadow-sm">
  <div class="flex justify-between items-center mb-md lg:mb-xl">
    <h3 class="font-title-lg text-title-lg">Warehouse Status</h3>
    <span class="material-symbols-outlined text-outline cursor-pointer">more_vert</span>
  </div>
  <div class="space-y-4 sm:space-y-6">
    {#each warehouses as w (w.name)}
      <div>
        <div class="flex justify-between mb-2">
          <span class="font-label-md text-on-surface text-sm">{w.name}</span>
          <span class="font-label-md font-bold text-sm">{w.percent}% Full</span>
        </div>
        <div class="w-full bg-surface-container h-2 rounded-full">
          <div class="{w.color} h-full rounded-full" style="width: {w.percent}%"></div>
        </div>
      </div>
    {/each}
  </div>
  <button class="w-full mt-md lg:mt-lg py-2 border border-primary text-primary font-label-md rounded-lg hover:bg-primary/5 transition-colors text-sm">
    Manage All Sites
  </button>
</div>