<script lang="ts">
  import { page } from '$app/state';
  import { onMount, onDestroy } from 'svelte';
  import { auth } from '$lib/stores/auth';
  import { goto } from '$app/navigation';

  interface NavItem {
    label: string;
    icon: string;
    href: string;
  }

  let isOpen = $state(false);

  const navItems: NavItem[] = [
    { label: 'Dashboard', icon: 'dashboard', href: '/dashboard' },
    { label: 'Products', icon: 'inventory_2', href: '/dashboard/products' },
    { label: 'Warehouses', icon: 'warehouse', href: '/dashboard/warehouses' },
    { label: 'Customers', icon: 'group', href: '/dashboard/customers' },
    { label: 'Sales Reps', icon: 'badge', href: '/dashboard/sales-reps' },
    { label: 'Orders', icon: 'shopping_cart', href: '/dashboard/orders' }
    // { label: 'Reports', icon: 'analytics', href: '/dashboard/reports' }
  ];

  function toggleSidebar() {
    isOpen = !isOpen;
  }

  function closeSidebar() {
    isOpen = false;
  }

  function handleToggleSidebar() {
    toggleSidebar();
  }

  function handleLogout() {
    auth.logout();
    goto('/');
  }

  onMount(() => {
    if (typeof window !== 'undefined') {
      window.addEventListener('toggle-sidebar', handleToggleSidebar);
    }
  });

  onDestroy(() => {
    if (typeof window !== 'undefined') {
      window.removeEventListener('toggle-sidebar', handleToggleSidebar);
    }
  });
</script>

<!-- Mobile Overlay -->
{#if isOpen}
  <div 
    class="fixed inset-0 bg-black/60 z-[100] lg:hidden backdrop-blur-sm" 
    onclick={closeSidebar}
    aria-hidden="true"
  ></div>
{/if}

<aside 
  class="fixed left-0 top-0 h-full w-[260px] bg-white flex flex-col border-r border-outline-variant z-[110] shadow-2xl transition-transform duration-300 ease-in-out {isOpen ? 'translate-x-0' : '-translate-x-full lg:translate-x-0'}"
>
  <div class="p-lg flex flex-col gap-sm">
    <div class="font-headline-md text-headline-md font-bold text-on-primary mb-md">
      B2B Wholesale
    </div>
    <div class="text-on-primary opacity-70 font-label-md text-label-md uppercase tracking-wider mb-lg">
      Enterprise Suite
    </div>
    
    <nav class="flex flex-col gap-1">
      {#each navItems as item (item.href)}
        {@const isActive = page.url.pathname === item.href}
        <a 
          href={item.href}
          onclick={closeSidebar}
          class="flex items-center gap-3 px-4 py-3 font-body-md text-body-md transition-all duration-200 ease-in-out {isActive ? 'bg-secondary-container text-on-secondary-container border-l-4 border-primary font-bold' : 'text-on-surface-variant hover:bg-surface-container-high'}"
        >
          <span class="material-symbols-outlined">{item.icon}</span>
          <span>{item.label}</span>
        </a>
      {/each}
    </nav>
  </div>

  <div class="mt-auto p-lg border-t border-outline-variant/20">
    <nav class="flex flex-col gap-1">
      <button
        class="flex items-center gap-3 px-4 py-3 text-on-surface-variant hover:bg-surface-container-high transition-colors w-full text-left"
        onclick={() => {
          handleLogout();
          closeSidebar();
        }}
      >
        <span class="material-symbols-outlined">logout</span>
        <span class="font-body-md text-body-md">Logout</span>
      </button>
    </nav>
  </div>
</aside>