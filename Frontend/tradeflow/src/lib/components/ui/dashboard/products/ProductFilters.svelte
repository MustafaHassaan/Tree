<!-- src/routes/dashboard/products/components/ProductFilters.svelte -->
<script lang="ts">
  let {
    selectedCategory = $bindable(),
    categories = []
  } = $props();
</script>

<div class="bg-surface-container-lowest p-md rounded-xl border border-outline-variant shadow-sm flex flex-col sm:flex-row gap-md items-start sm:items-center mb-lg">
  <div class="flex items-center gap-2 pr-md border-r border-outline-variant w-full sm:w-auto">
    <span class="material-symbols-outlined text-outline">filter_alt</span>
    <span class="font-label-md text-label-md text-on-surface-variant">Filters:</span>
  </div>
  <div class="flex flex-wrap gap-sm sm:gap-md w-full sm:flex-1">
    <select
      class="bg-surface-container-low border-none rounded-lg px-3 sm:px-4 py-1.5 font-label-md text-label-md text-on-surface-variant focus:ring-1 focus:ring-primary/20 text-sm"
      bind:value={selectedCategory}
    >
      {#each categories as category}
        <option value={category}>{category}</option>
      {/each}
    </select>
  </div>
</div>