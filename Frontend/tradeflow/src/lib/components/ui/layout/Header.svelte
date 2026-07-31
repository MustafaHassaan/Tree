<script lang="ts">
  let searchQuery = $state('');
  let showMobileMenu = $state(false);

  function toggleSidebar() {
    const event = new CustomEvent('toggle-sidebar');
    window.dispatchEvent(event);
  }
</script>

<header class="h-14 sm:h-16 w-full flex justify-between items-center px-sm sm:px-lg bg-surface-container-lowest border-b border-outline-variant sticky top-0 z-50 shadow-sm">
  <div class="flex items-center gap-sm sm:gap-lg flex-1">
    <button 
      class="lg:hidden p-2 text-on-surface-variant hover:bg-surface-container-low rounded-full transition-colors"
      onclick={toggleSidebar}
    >
      <span class="material-symbols-outlined text-sm sm:text-base">menu</span>
    </button>
  </div>

  <div class="flex items-center gap-sm sm:gap-md">
    <div class="flex items-center gap-sm sm:gap-md">
    </div>

    <div class="flex items-center gap-sm sm:gap-md pl-sm">
      <div class="h-8 w-8 sm:h-10 sm:w-10 rounded-full bg-secondary-container overflow-hidden border border-outline-variant cursor-pointer flex-shrink-0">
        <img class="w-full h-full object-cover" src="https://lh3.googleusercontent.com/aida-public/AB6AXuDP2F1rOR7upT4_1F_UOkACmAgY25GxFcopeH1OPWoESpwKHDHEiciU3CIPSNZwycCFOaw5m7XOFLyXVRfUgJJFip005d-qc0PyNCPK53Gx2ejuwifSxFGho8zN9HM960UcIv6GF3GhAZsjB3sGuVCWxEAofdPoMJLfCXVtrFzheMp2qH34E0VIR735Qlw1_MUG7cOaibZypInIE1gBsGO9BLdbbb30YN2Jctm9rz3GZ4Hl4sbO3gUXguOsavfv-Gcpzt5Wu--02Mo" alt="User avatar" />
      </div>
    </div>
  </div>
</header>