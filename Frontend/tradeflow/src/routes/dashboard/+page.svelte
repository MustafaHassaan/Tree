<script lang="ts">
  import { onMount } from 'svelte';
  import KpiCard from '../../lib/components/ui/dashboard/KpiGrid.svelte';
  import TopProductsTable from '../../lib/components/ui/dashboard/TopProductsTable.svelte';
  import SalesChart from '../../lib/components/ui/dashboard/SalesChart.svelte';
  import WarehouseStatus from '../../lib/components/ui/dashboard/WarehouseStatus.svelte';
  import SalesRepsList from '../../lib/components/ui/dashboard/SalesRepsList.svelte';
  import { dashboardService, type DashboardStats } from '../../lib/services/dashboard';

  let isLoading = $state(true);
  let dashboardData = $state<DashboardStats | null>(null);

  // Derived data for UI
  let chartDays = $derived(
    dashboardData?.dailySales.map(d => ({
      day: d.day,
      total: `${d.total}%`,
      actual: `${d.actual}%`
    })) || []
  );

  let warehouseStatus = $derived(
    dashboardData?.warehouseStatus.map(w => ({
      name: w.name,
      percent: Math.floor(w.capacityPercentage),
      color: w.color
    })) || []
  );

  let topProducts = $derived(
    dashboardData?.topProducts.map(p => ({
      name: p.name,
      sku: p.sku,
      sold: p.sold,
      revenue: `$${p.revenue.toLocaleString()}`,
      icon: 'inventory_2',
      trend: p.trend,
      trendClass: p.trendClass
    })) || []
  );

  let salesReps = $derived(
    dashboardData?.salesRepsPerformance.map(e => ({
      name: e.name,
      region: e.position,
      sales: `$${e.sales.toLocaleString()}`,
      goal: e.goal,
      rank: e.rank,
      active: e.active,
      img: 'https://lh3.googleusercontent.com/aida-public/AB6AXuBTt9G9707Bcv9ud2HMMXzsUoWBjLBFE01MUj4Qc2Wa6NA4vWsdpuTt-NvTU2etM1kN4PV0_iLTSsW5HrN_cx6q27vhhiWk4EpSAPt7EWSvbDxwEU99a3_C88LxyVAvJHXiAfiSJESyzxsev_wKx9WlOW5zo5KquS2DwgYjzHsT-Mf66dI7Qp-UehHcmBRksrE1SKa3VKMju-_cWPyWVzj_vPEswjYxRphjZcKt1DQI17JitP5NOMzBho5C5zMRNxwt_Iq7-sxe8hg'
    })) || []
  );

  let activeCustomersCount = $derived(dashboardData?.activeCustomers || 0);

  onMount(async () => {
    try {
      dashboardData = await dashboardService.getStats();
    } catch (err) {
      console.error('Failed to load dashboard data:', err);
    } finally {
      isLoading = false;
    }
  });
</script>

<!-- Header Title -->
<div class="mb-lg lg:mb-xl">
  <div>
    <h1 class="font-display-lg text-display-lg text-on-surface">Dashboard</h1>
    <p class="text-on-surface-variant font-body-lg">Operations overview for March 24, 2024</p>
  </div>
</div>

{#if isLoading}
  <div class="flex items-center justify-center py-20">
    <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-primary"></div>
  </div>
{:else}
  <!-- KPI Grid Section -->
  <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-md lg:gap-lg mb-lg lg:mb-xl">
    <KpiCard 
      title="Total Sales" 
      value={`$${(dashboardData?.totalSales || 0).toLocaleString()}`} 
      change="+12.5%" 
      icon="payments" 
      progressPercentage={78} 
      variant="primary" 
    />
    <KpiCard 
      title="Monthly Revenue" 
      value={`$${(dashboardData?.monthlyRevenue || 0).toLocaleString()}`} 
      change="+8.2%" 
      icon="trending_up" 
      progressPercentage={62} 
      variant="secondary" 
    />
    <KpiCard 
      title="Active Customers" 
      value={activeCustomersCount.toString()} 
      change="+{activeCustomersCount}" 
      icon="group" 
      progressPercentage={85} 
      variant="tertiary" 
    />
    <KpiCard 
      title="Low Stock Alerts" 
      value={`${dashboardData?.lowStockAlerts || 0} SKUs`} 
      change={dashboardData && dashboardData.lowStockAlerts > 0 ? "CRITICAL" : "OK"} 
      icon="warning" 
      progressPercentage={dashboardData && dashboardData.lowStockAlerts > 0 ? 45 : 90} 
      variant={dashboardData && dashboardData.lowStockAlerts > 0 ? "error" : "tertiary"} 
    />
  </div>

  <!-- Charts & Warehouse Section -->
  <div class="grid grid-cols-1 lg:grid-cols-3 gap-md lg:gap-lg mb-lg lg:mb-xl">
    <SalesChart {chartDays} />
    <WarehouseStatus warehouses={warehouseStatus} />
  </div>

  <!-- Products & Reps Section -->
  <div class="grid grid-cols-1 xl:grid-cols-3 gap-md lg:gap-lg">
    <div class="xl:col-span-2">
      <TopProductsTable products={topProducts} />
    </div>
    <SalesRepsList salesReps={salesReps} />
  </div>
{/if}