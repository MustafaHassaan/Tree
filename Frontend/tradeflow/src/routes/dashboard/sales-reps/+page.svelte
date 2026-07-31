<script lang="ts">
  import { onMount } from 'svelte';
  import type { SalesRep, Activity } from '../../../lib/types/salesrepperformance';
  import { employeesService } from '../../../lib/services/employees';
  import SalesActivitiesTable from '../../../lib/components/ui/dashboard/sales-reps/SalesActivitiesTable.svelte';
  import AddEmployeeModal from '../../../lib/components/ui/dashboard/sales-reps/AddEmployeeModal.svelte';
  import EmployeesListModal from '../../../lib/components/ui/dashboard/sales-reps/EmployeesListModal.svelte';
  import Dropdown from '../../../lib/components/ui/dropdown/Dropdown.svelte';

  let isLoading = $state(true);
  let error = $state('');
  let isAddModalOpen = $state(false);
  let isListModalOpen = $state(false);
  let isDropdownOpen = $state(false);
  let employeeToEdit = $state<any>(null);

  let salesReps: SalesRep[] = $state([]);
  let recentActivities: Activity[] = $state([]);

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
              initials: emp.name.split(' ').map(n => n[0]).join(''),
              role: 'Senior Rep',
              avatar: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDkHEKYumpV-yc5qvyxCHGZGirdZJziaHFM6hfB8mwDWYFWeWHwigSy5k6LHQKAs6EyTXxu3vAMcEHyNVxNUWgnjfjN4cvcASjWeKJoRFQOIHGVVda9LogsnKpvrKboLrrpgaM9wlnwk4NKr5uCZX-SqBCub4uh85avYEoOCr-mHFzsKR3siJC0-R6C-qzsNaa_4gl42Nhsqh763_iSM4-iSpsqEfI42GyQP5GRkoIpS2uq4azsKs59jXjeOT2ivdbkPE04BG7hAwY',
              targetPercent: perf.achievementPercentage,
              actualSales: perf.totalSales,
              commission: perf.earnedCommission,
              performance: perf.achievementPercentage >= 90 ? 'Over-Target' : perf.achievementPercentage >= 70 ? 'Excellent' : 'Under-performing',
              performanceColor: perf.achievementPercentage >= 70 ? 'primary' : 'error'
            };
          } catch (err) {
            return {
              id: emp.id,
              name: emp.name,
              initials: emp.name.split(' ').map(n => n[0]).join(''),
              role: 'Senior Rep',
              avatar: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDkHEKYumpV-yc5qvyxCHGZGirdZJziaHFM6hfB8mwDWYFWeWHwigSy5k6LHQKAs6EyTXxu3vAMcEHyNVxNUWgnjfjN4cvcASjWeKJoRFQOIHGVVda9LogsnKpvrKboLrrpgaM9wlnwk4NKr5uCZX-SqBCub4uh85avYEoOCr-mHFzsKR3siJC0-R6C-qzsNaa_4gl42Nhsqh763_iSM4-iSpsqEfI42GyQP5GRkoIpS2uq4azsKs59jXjeOT2ivdbkPE04BG7hAwY',
              targetPercent: 0,
              actualSales: 0,
              commission: 0,
              performance: 'Under-performing',
              performanceColor: 'error'
            };
          }
        })
      );

      // Mock activities for now (API doesn't have GetAll endpoint)
      recentActivities = [
        { id: 1, rep: salesReps[0]?.name || 'Sales Rep', initials: salesReps[0]?.initials || 'SR', color: 'primary', action: 'Contract Signature', client: 'Global Logistics Inc.', amount: 45000, status: 'COMPLETED', statusColor: 'green', time: '10:45 AM' },
        { id: 2, rep: salesReps[1]?.name || 'Sales Rep', initials: salesReps[1]?.initials || 'SR', color: 'secondary', action: 'Proposal Sent', client: 'Metro Retail Hub', amount: 128400, status: 'PENDING', statusColor: 'blue', time: '09:12 AM' },
        { id: 3, rep: salesReps[0]?.name || 'Sales Rep', initials: salesReps[0]?.initials || 'SR', color: 'primary', action: 'Client Call', client: 'Pioneer Tech', amount: 0, status: 'SCHEDULED', statusColor: 'gray', time: 'Yesterday' },
        { id: 4, rep: salesReps[2]?.name || 'Sales Rep', initials: salesReps[2]?.initials || 'SR', color: 'tertiary', action: 'Bulk Order Intake', client: 'Apex Supplies Ltd.', amount: 12200, status: 'COMPLETED', statusColor: 'green', time: 'Yesterday' },
        { id: 5, rep: salesReps[1]?.name || 'Sales Rep', initials: salesReps[1]?.initials || 'SR', color: 'secondary', action: 'Renewal Negotiation', client: 'Interstate Logistics', amount: 80000, status: 'IN PROGRESS', statusColor: 'orange', time: 'Aug 24, 2023' }
      ];
    } catch (err: any) {
      error = 'Failed to load sales representatives data';
      console.error(err);
    } finally {
      isLoading = false;
    }
  });

  async function handleEmployeeAdded() {
    // Reload sales reps data
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
              initials: emp.name.split(' ').map(n => n[0]).join(''),
              role: 'Senior Rep',
              avatar: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDkHEKYumpV-yc5qvyxCHGZGirdZJziaHFM6hfB8mwDWYFWeWHwigSy5k6LHQKAs6EyTXxu3vAMcEHyNVxNUWgnjfjN4cvcASjWeKJoRFQOIHGVVda9LogsnKpvrKboLrrpgaM9wlnwk4NKr5uCZX-SqBCub4uh85avYEoOCr-mHFzsKR3siJC0-R6C-qzsNaa_4gl42Nhsqh763_iSM4-iSpsqEfI42GyQP5GRkoIpS2uq4azsKs59jXjeOT2ivdbkPE04BG7hAwY',
              targetPercent: perf.achievementPercentage,
              actualSales: perf.totalSales,
              commission: perf.earnedCommission,
              performance: perf.achievementPercentage >= 90 ? 'Over-Target' : perf.achievementPercentage >= 70 ? 'Excellent' : 'Under-performing',
              performanceColor: perf.achievementPercentage >= 70 ? 'primary' : 'error'
            };
          } catch (err) {
            return {
              id: emp.id,
              name: emp.name,
              initials: emp.name.split(' ').map(n => n[0]).join(''),
              role: 'Senior Rep',
              avatar: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDkHEKYumpV-yc5qvyxCHGZGirdZJziaHFM6hfB8mwDWYFWeWHwigSy5k6LHQKAs6EyTXxu3vAMcEHyNVxNUWgnjfjN4cvcASjWeKJoRFQOIHGVVda9LogsnKpvrKboLrrpgaM9wlnwk4NKr5uCZX-SqBCub4uh85avYEoOCr-mHFzsKR3siJC0-R6C-qzsNaa_4gl42Nhsqh763_iSM4-iSpsqEfI42GyQP5GRkoIpS2uq4azsKs59jXjeOT2ivdbkPE04BG7hAwY',
              targetPercent: 0,
              actualSales: 0,
              commission: 0,
              performance: 'Under-performing',
              performanceColor: 'error'
            };
          }
        })
      );

      // Mock activities for now (API doesn't have GetAll endpoint)
      recentActivities = [
        { id: 1, rep: salesReps[0]?.name || 'Sales Rep', initials: salesReps[0]?.initials || 'SR', color: 'primary', action: 'Contract Signature', client: 'Global Logistics Inc.', amount: 45000, status: 'COMPLETED', statusColor: 'green', time: '10:45 AM' },
        { id: 2, rep: salesReps[1]?.name || 'Sales Rep', initials: salesReps[1]?.initials || 'SR', color: 'secondary', action: 'Proposal Sent', client: 'Metro Retail Hub', amount: 128400, status: 'PENDING', statusColor: 'blue', time: '09:12 AM' },
        { id: 3, rep: salesReps[0]?.name || 'Sales Rep', initials: salesReps[0]?.initials || 'SR', color: 'primary', action: 'Client Call', client: 'Pioneer Tech', amount: 0, status: 'SCHEDULED', statusColor: 'gray', time: 'Yesterday' },
        { id: 4, rep: salesReps[2]?.name || 'Sales Rep', initials: salesReps[2]?.initials || 'SR', color: 'tertiary', action: 'Bulk Order Intake', client: 'Apex Supplies Ltd.', amount: 12200, status: 'COMPLETED', statusColor: 'green', time: 'Yesterday' },
        { id: 5, rep: salesReps[1]?.name || 'Sales Rep', initials: salesReps[1]?.initials || 'SR', color: 'secondary', action: 'Renewal Negotiation', client: 'Interstate Logistics', amount: 80000, status: 'IN PROGRESS', statusColor: 'orange', time: 'Aug 24, 2023' }
      ];
    } catch (err: any) {
      error = 'Failed to reload sales representatives data';
      console.error(err);
    }
  }

  function handleEditEmployee(employee: any) {
    employeeToEdit = employee;
    isListModalOpen = false;
    isAddModalOpen = true;
  }

  function handleAddEmployee() {
    employeeToEdit = null;
    isAddModalOpen = true;
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
  <div class="space-y-xl">
    <!-- Page Header -->
    <section class="flex flex-col sm:flex-row justify-between items-start sm:items-end gap-md">
      <div>
        <h2 class="font-headline-lg text-headline-lg text-on-surface">Sales Representative Performance</h2>
        <p class="font-body-md text-body-md text-on-surface-variant">Real-time performance metrics and activity tracking across your sales force.</p>
      </div>
      <div class="flex gap-sm sm:gap-md w-full sm:w-auto">
        <Dropdown bind:isOpen={isDropdownOpen}>
          {#snippet trigger()}
            <button
              class="flex items-center gap-2 px-3 sm:px-4 py-2 bg-primary text-on-primary rounded-lg font-label-md text-label-md hover:opacity-90 transition-all text-xs sm:text-sm"
              type="button"
            >
              <span class="material-symbols-outlined">add</span>
              <span class="hidden sm:inline">Employee Actions</span>
            </button>
          {/snippet}

          {#snippet children()}
            <button
              onclick={() => { isDropdownOpen = false; handleAddEmployee(); }}
              class="w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 transition-colors"
              type="button"
            >
              Add New Employee
            </button>
            <button
              onclick={() => { isDropdownOpen = false; isListModalOpen = true; }}
              class="w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 transition-colors"
              type="button"
            >
              View All Employees
            </button>
          {/snippet}
        </Dropdown>
      </div>
    </section>
    <!-- Activity Table Section -->
    <SalesActivitiesTable activities={recentActivities} />

  </div>
{/if}

<AddEmployeeModal
  bind:isOpen={isAddModalOpen}
  onClose={() => isAddModalOpen = false}
  onEmployeeAdded={handleEmployeeAdded}
  employeeToEdit={employeeToEdit}
/>

<EmployeesListModal
  bind:isOpen={isListModalOpen}
  onClose={() => isListModalOpen = false}
  onEditEmployee={handleEditEmployee}
/>