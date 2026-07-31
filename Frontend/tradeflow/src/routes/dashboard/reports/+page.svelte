<script lang="ts">
  import { onMount } from 'svelte';
  import type { SalesRep, WarehouseItem } from '../../../lib/types/reports';
  import { employeesService } from '../../../lib/services/employees';
  import NetSalesChart from '../../../lib/components/ui/dashboard/reports/NetSalesChart.svelte';
  import RevenueSegmentChart from '../../../lib/components/ui/dashboard/reports/RevenueSegmentChart.svelte';
  import SalesRepsList from '../../../lib/components/ui/dashboard/reports/SalesRepsList.svelte';
  import WarehouseTurnoverChart from '../../../lib/components/ui/dashboard/reports/WarehouseTurnoverChart.svelte';

  let activeTab = $state('sales');
  let dateRange = $state('Oct 01, 2023 - Oct 31, 2023');
  let isLoading = $state(true);
  let error = $state('');

  let salesReps: SalesRep[] = $state([]);
  let warehouseData: WarehouseItem[] = $state([]);

  onMount(async () => {
    try {
      const employees = await employeesService.getAll();
      const salesRepsData = employees.filter(e => e.role === 0); // SalesRepresentative

      salesReps = await Promise.all(
        salesRepsData.map(async (emp, index) => {
          try {
            const perf = await employeesService.getPerformance(emp.id);
            return {
              id: emp.id,
              name: emp.name,
              sales: perf.totalSales,
              percentage: perf.achievementPercentage,
              avatar: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDvXpWkJz9a8rxwK3EPPXm20JhcRg96VpUQ6xG9G6Bb9rsNULdtJ01JAhha-R_l3-vmWzJBZGMSrD4drCTTX8-Ae8DTm9lzV8fMGVXJYXpoBXvvENfDBdL-QpGaf68oT9HxelUpq-VDnyoVGQnjp6wEovRVtZzA2DrwUyniOZRjGXp-JE-2Q6aB4y3S7As8XGvefRufexJZQ5kXb6qXgsX9Jw5qmAwAvARFZAWfNrLEBjGMNbYBVSDrWvOLljGv-jJppU-iurzGxUs'
            };
          } catch (err) {
            return {
              id: emp.id,
              name: emp.name,
              sales: 0,
              percentage: 0,
              avatar: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDvXpWkJz9a8rxwK3EPPXm20JhcRg96VpUQ6xG9G6Bb9rsNULdtJ01JAhha-R_l3-vmWzJBZGMSrD4drCTTX8-Ae8DTm9lzV8fMGVXJYXpoBXvvENfDBdL-QpGaf68oT9HxelUpq-VDnyoVGQnjp6wEovRVtZzA2DrwUyniOZRjGXp-JE-2Q6aB4y3S7As8XGvefRufexJZQ5kXb6qXgsX9Jw5qmAwAvARFZAWfNrLEBjGMNbYBVSDrWvOLljGv-jJppU-iurzGxUs'
            };
          }
        })
      );

      // Mock warehouse data for now (will be replaced with API later)
      warehouseData = [
        { name: 'WH-East', height: 85 },
        { name: 'WH-West', height: 60 },
        { name: 'CH-North', height: 40 },
        { name: 'Global-S', height: 95 },
        { name: 'WH-Metro', height: 70 },
        { name: 'Port-Z', height: 55 },
        { name: 'Log-A1', height: 75 },
        { name: 'Log-A2', height: 45 },
        { name: 'WH-B12', height: 30 },
        { name: 'Central', height: 90 },
        { name: 'WH-South', height: 65 },
        { name: 'Regional', height: 80 }
      ];
    } catch (err: any) {
      error = 'Failed to load reports data';
      console.error(err);
    } finally {
      isLoading = false;
    }
  });

  function setTab(tab: string) {
    activeTab = tab;
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
  <!-- Context Header & Filters -->
  <div class="flex flex-col lg:flex-row justify-between items-start lg:items-center gap-md mb-xl">
    <div>
      <h2 class="font-headline-lg text-headline-lg text-on-surface">Reports Center</h2>
      <p class="font-body-md text-body-md text-on-surface-variant">Analyze performance, inventory trends, and sales velocity across all channels.</p>
    </div>
    <div class="flex flex-wrap gap-sm items-center w-full lg:w-auto">
      <div class="flex items-center bg-white border border-outline-variant rounded-lg px-md py-2 shadow-sm">
        <span class="material-symbols-outlined text-on-surface-variant text-[20px] mr-2">calendar_today</span>
        <select class="bg-transparent border-none focus:ring-0 text-label-md font-medium cursor-pointer" bind:value={dateRange}>
          <option>Oct 01, 2023 - Oct 31, 2023</option>
          <option>Last 7 Days</option>
          <option>Last 30 Days</option>
          <option>This Quarter</option>
        </select>
      </div>
      <button class="bg-white border border-outline-variant text-on-surface px-md py-2 rounded-lg flex items-center gap-2 hover:bg-surface-container-low transition-colors shadow-sm font-label-md text-xs sm:text-sm">
        <span class="material-symbols-outlined text-[20px]">filter_list</span>
        <span class="hidden sm:inline">Filters</span>
      </button>
      <button class="bg-primary text-white px-md py-2 rounded-lg flex items-center gap-2 hover:opacity-90 transition-all shadow-sm font-label-md font-bold text-xs sm:text-sm">
        <span class="material-symbols-outlined text-[20px]">download</span>
        <span class="hidden sm:inline">Download PDF</span>
      </button>
    </div>
  </div>

  <!-- Report Selection Tabs -->
  <div class="flex border-b border-outline-variant gap-lg mb-lg overflow-x-auto">
    <button
      class="pb-3 border-b-2 {activeTab === 'sales' ? 'border-primary text-primary font-bold' : 'border-transparent text-on-surface-variant hover:text-on-surface'} font-label-md flex items-center gap-2 whitespace-nowrap transition-colors"
      onclick={() => setTab('sales')}
    >
      <span class="material-symbols-outlined text-[18px]">payments</span>
      Sales Reports
    </button>
    <button
      class="pb-3 border-b-2 {activeTab === 'inventory' ? 'border-primary text-primary font-bold' : 'border-transparent text-on-surface-variant hover:text-on-surface'} font-label-md flex items-center gap-2 whitespace-nowrap transition-colors"
      onclick={() => setTab('inventory')}
    >
      <span class="material-symbols-outlined text-[18px]">inventory_2</span>
      Inventory Reports
    </button>
    <button
      class="pb-3 border-b-2 {activeTab === 'team' ? 'border-primary text-primary font-bold' : 'border-transparent text-on-surface-variant hover:text-on-surface'} font-label-md flex items-center gap-2 whitespace-nowrap transition-colors"
      onclick={() => setTab('team')}
    >
      <span class="material-symbols-outlined text-[18px]">groups</span>
      Team Performance
    </button>
  </div>

  <!-- Bento Grid Dashboard -->
  <div class="grid grid-cols-1 lg:grid-cols-12 gap-lg">
    <NetSalesChart />
    <RevenueSegmentChart />

    <!-- KPI Quick Stats -->
    <div class="col-span-1 lg:col-span-3 bg-white border border-outline-variant rounded-xl p-lg shadow-sm">
      <div class="flex justify-between items-start mb-md">
        <div class="p-2 bg-primary/10 text-primary rounded-lg">
          <span class="material-symbols-outlined">trending_up</span>
        </div>
        <span class="text-xs font-bold text-green-600 bg-green-50 px-2 py-1 rounded">+12.4%</span>
      </div>
      <p class="text-label-sm text-on-surface-variant font-medium uppercase tracking-wider">Average Order Value</p>
      <h4 class="font-display-lg text-display-lg font-black text-on-surface mt-1">$2,840.00</h4>
    </div>

    <div class="col-span-1 lg:col-span-3 bg-white border border-outline-variant rounded-xl p-lg shadow-sm">
      <div class="flex justify-between items-start mb-md">
        <div class="p-2 bg-tertiary/10 text-tertiary rounded-lg">
          <span class="material-symbols-outlined">shopping_bag</span>
        </div>
        <span class="text-xs font-bold text-red-600 bg-red-50 px-2 py-1 rounded">-2.1%</span>
      </div>
      <p class="text-label-sm text-on-surface-variant font-medium uppercase tracking-wider">Total Order Count</p>
      <h4 class="font-display-lg text-display-lg font-black text-on-surface mt-1">14,208</h4>
    </div>

    <SalesRepsList reps={salesReps} />
    <WarehouseTurnoverChart data={warehouseData} />
  </div>
{/if}