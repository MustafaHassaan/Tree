<script lang="ts">
  import type { Activity } from '../../../../types/salesrepperformance';

  let { activities }: { activities: Activity[] } = $props();

  let currentPage = $state(1);
  const itemsPerPage = 5;

  let paginatedActivities = $derived(
    activities.slice((currentPage - 1) * itemsPerPage, currentPage * itemsPerPage)
  );

  let totalPages = $derived(Math.ceil(activities.length / itemsPerPage));

  function getStatusDotColor(color: string) {
    switch (color) {
      case 'green': return 'bg-green-600';
      case 'blue': return 'bg-blue-600';
      case 'gray': return 'bg-gray-600';
      case 'orange': return 'bg-orange-600';
      default: return 'bg-gray-600';
    }
  }

  function getStatusBgColor(color: string) {
    switch (color) {
      case 'green': return 'bg-green-100 text-green-800';
      case 'blue': return 'bg-blue-100 text-blue-800';
      case 'gray': return 'bg-gray-100 text-gray-800';
      case 'orange': return 'bg-orange-100 text-orange-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  }

  function getInitialsColor(color: string) {
    switch (color) {
      case 'primary': return 'bg-primary-fixed text-primary';
      case 'secondary': return 'bg-secondary-fixed text-secondary';
      case 'tertiary': return 'bg-tertiary-fixed text-tertiary';
      default: return 'bg-primary-fixed text-primary';
    }
  }
</script>

<section class="bg-surface-container-lowest border border-outline-variant rounded-xl overflow-hidden shadow-sm">
  <div class="p-lg border-b border-outline-variant">
    <h3 class="font-title-lg text-title-lg text-on-surface">Recent Sales Activities</h3>
  </div>

  <div class="overflow-x-auto">
    <table class="w-full text-left border-collapse min-w-[600px]">
      <thead>
        <tr class="bg-surface-container-low font-label-sm text-label-sm text-outline uppercase">
          <th class="px-lg py-4 font-bold">Representative</th>
          <th class="px-lg py-4 font-bold">Action / Deal</th>
          <th class="px-lg py-4 font-bold">Client Name</th>
          <th class="px-lg py-4 font-bold">Amount</th>
          <th class="px-lg py-4 font-bold">Status</th>
          <th class="px-lg py-4 font-bold text-right">Time</th>
        </tr>
      </thead>
      <tbody class="font-body-md text-body-md divide-y divide-outline-variant">
        {#each paginatedActivities as activity (activity.id)}
          <tr class="hover:bg-primary/5 transition-colors cursor-pointer">
            <td class="px-lg py-4">
              <div class="flex items-center gap-3">
                <div class="h-8 w-8 rounded-full {getInitialsColor(activity.color)} font-bold flex items-center justify-center text-[10px]">{activity.initials}</div>
                <span class="hidden sm:inline">{activity.rep}</span>
                <span class="sm:hidden">{activity.initials}</span>
              </div>
            </td>
            <td class="px-lg py-4 font-bold text-on-surface">{activity.action}</td>
            <td class="px-lg py-4 text-on-surface-variant">{activity.client}</td>
            <td class="px-lg py-4 font-bold text-primary">{activity.amount > 0 ? '$' + activity.amount.toLocaleString() : 'N/A'}</td>
            <td class="px-lg py-4">
              <span class="inline-flex items-center gap-1 px-2 py-1 rounded-full {getStatusBgColor(activity.statusColor)} text-[11px] font-bold">
                <span class="w-1.5 h-1.5 rounded-full {getStatusDotColor(activity.statusColor)}"></span>
                {activity.status}
              </span>
            </td>
            <td class="px-lg py-4 text-right text-outline">{activity.time}</td>
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