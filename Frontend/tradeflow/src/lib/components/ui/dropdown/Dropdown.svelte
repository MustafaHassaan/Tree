<script lang="ts">
  let { isOpen = $bindable(false), trigger, children } = $props();

  function toggle() {
    isOpen = !isOpen;
  }

  function close() {
    isOpen = false;
  }

  function handleKeydown(e: KeyboardEvent) {
    if (e.key === 'Escape') {
      close();
    }
  }
</script>

<div class="relative" onkeydown={handleKeydown}>
  <!-- Trigger Button -->
  <button
    onclick={toggle}
    aria-expanded={isOpen}
    aria-haspopup="true"
    type="button"
  >
    {@render trigger()}
  </button>

  <!-- Dropdown Menu -->
  {#if isOpen}
    <div
      class="absolute right-0 mt-2 w-48 bg-white rounded-lg shadow-lg border border-gray-100 py-1 z-50"
      role="menu"
      onmouseleave={close}
    >
      {@render children()}
    </div>
  {/if}
</div>
