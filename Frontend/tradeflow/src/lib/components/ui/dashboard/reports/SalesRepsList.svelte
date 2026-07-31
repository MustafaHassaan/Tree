<script lang="ts">
  import type { SalesRep } from '../../../../types/reports';

  let { reps }: { reps: SalesRep[] } = $props();
</script>

<div class="col-span-1 lg:col-span-6 bg-white border border-outline-variant rounded-xl p-lg shadow-sm overflow-hidden flex flex-col">
  <div class="flex justify-between items-center mb-lg">
    <h3 class="font-title-lg text-title-lg text-on-surface">Top Sales Representatives</h3>
    <a class="text-primary font-label-md font-bold hover:underline" href="#">View All</a>
  </div>
  <div class="flex-1 space-y-md">
    {#each reps as rep (rep.id)}
      <div class="flex items-center gap-md">
        <img class="w-10 h-10 rounded-full object-cover" src={rep.avatar} alt={rep.name} />
        <div class="flex-1">
          <div class="flex justify-between text-body-md font-bold mb-1">
            <span>{rep.name}</span>
            <span>${rep.sales.toLocaleString()}</span>
          </div>
          <div class="w-full bg-surface-container-low h-2 rounded-full overflow-hidden">
            <div class="bg-primary h-full rounded-full" style="width: {rep.percentage}%"></div>
          </div>
        </div>
      </div>
    {/each}
  </div>
</div>