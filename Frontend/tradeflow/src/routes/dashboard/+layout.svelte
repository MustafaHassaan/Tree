<script lang="ts">
  import Sidebar from '$lib/components/ui/layout/Sidebar.svelte';
  import Header from '$lib/components/ui/layout/Header.svelte';

  let { children } = $props();
</script>

<div class="flex h-screen w-full overflow-hidden text-on-surface">
  <Sidebar />

  <main class="lg:ml-[260px] flex-1 flex flex-col h-screen bg-surface-bright overflow-hidden">
    <Header />

    <div class="flex-1 overflow-y-auto p-lg lg:p-xl hide-scrollbar">
      {@render children()}
      <div class="h-8 lg:h-12"></div>
    </div>
  </main>
</div>