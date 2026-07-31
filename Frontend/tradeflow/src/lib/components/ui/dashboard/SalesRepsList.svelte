<script lang="ts">
  let { salesReps = [] } = $props();
</script>

<div class="bg-surface-container-lowest p-md lg:p-lg border border-outline-variant rounded-xl shadow-sm">
  <div class="flex justify-between items-center mb-md lg:mb-lg">
    <h3 class="font-title-lg text-title-lg">Sales Representatives</h3>
    <button class="p-1 hover:bg-surface-container rounded transition-colors">
      <span class="material-symbols-outlined text-[20px]">filter_list</span>
    </button>
  </div>
  <div class="space-y-4 sm:space-y-5">
    {#each salesReps as rep (rep.name)}
      <div class="flex items-center justify-between group">
        <div class="flex items-center gap-sm md:gap-md">
          <div class="relative flex-shrink-0">
            <div class="w-8 h-8 sm:w-10 sm:h-10 rounded-full bg-surface-container-high border-2 {rep.active ? 'border-primary' : 'border-transparent'} overflow-hidden">
              <img src={rep.img} alt={rep.name} class="w-full h-full object-cover" />
            </div>
            <span class="absolute -bottom-1 -right-1 w-4 h-4 sm:w-5 sm:h-5 {rep.active ? 'bg-primary text-on-primary' : 'bg-outline-variant text-on-surface'} text-[10px] font-bold flex items-center justify-center rounded-full border-2 border-white">
              {rep.rank}
            </span>
          </div>
          <div class="min-w-0">
            <p class="font-body-md font-bold group-hover:text-primary transition-colors text-sm sm:text-base truncate">{rep.name}</p>
            <p class="text-label-sm text-on-surface-variant text-xs sm:text-sm">{rep.region}</p>
          </div>
        </div>
        <div class="text-right flex-shrink-0">
          <p class="font-body-md font-bold text-sm sm:text-base">{rep.sales}</p>
          <p class="text-label-sm {rep.active ? 'text-primary' : 'text-secondary'} text-xs sm:text-sm">{rep.goal}</p>
        </div>
      </div>
    {/each}
  </div>
  <div class="mt-md lg:mt-lg pt-md lg:pt-lg border-t border-outline-variant">
    <button class="w-full py-2 bg-surface-container-low text-on-surface-variant font-label-md rounded-lg hover:bg-surface-container-high transition-colors text-sm">
      Detailed Analytics
    </button>
  </div>
</div>