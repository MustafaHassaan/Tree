<script lang="ts">
  let { chartDays = [] } = $props();
</script>

<div class="lg:col-span-2 bg-surface-container-lowest p-md lg:p-lg border border-outline-variant rounded-xl shadow-sm">
  <div class="flex flex-col sm:flex-row justify-between items-start sm:items-center mb-md lg:mb-xl gap-sm">
    <div>
      <h3 class="font-title-lg text-title-lg">Sales Performance</h3>
      <p class="text-label-md text-on-surface-variant">Revenue vs Projections (Last 7 Days)</p>
    </div>
    <div class="flex items-center gap-md">
      <div class="flex items-center gap-2">
        <span class="w-3 h-3 rounded-full bg-primary"></span>
        <span class="text-label-sm">Actual</span>
      </div>
      <div class="flex items-center gap-2">
        <span class="w-3 h-3 rounded-full bg-outline-variant"></span>
        <span class="text-label-sm">Projected</span>
      </div>
    </div>
  </div>

  <div class="relative h-48 sm:h-64 w-full flex items-end justify-between gap-2 sm:gap-4 px-2">
    <div class="absolute inset-0 grid grid-rows-4 pointer-events-none">
      <div class="border-b border-outline-variant/30"></div>
      <div class="border-b border-outline-variant/30"></div>
      <div class="border-b border-outline-variant/30"></div>
      <div class="border-b border-outline-variant/30"></div>
    </div>
    {#each chartDays as c (c.day)}
      <div class="flex-1 flex flex-col items-center gap-1 sm:gap-2 h-full justify-end group">
        <div class="w-full bg-primary-container/20 rounded-t-sm relative overflow-hidden" style="height: {c.total}">
          <div class="absolute bottom-0 left-0 w-full bg-primary rounded-t-sm group-hover:opacity-80 transition-all" style="height: {c.actual}"></div>
        </div>
        <span class="text-label-xs sm:text-label-sm text-on-surface-variant">{c.day}</span>
      </div>
    {/each}
  </div>
</div>