<script lang="ts">
  import { onMount } from 'svelte';
  import { auth } from '$lib/stores/auth';
  import { goto } from '$app/navigation';
  import api from '$lib/services/api';

  let phone = $state('');
  let password = $state('');
  let remember = $state(false);
  let showPassword = $state(false);

  let isLoading = $state(false);
  let isSuccess = $state(false);
  let error = $state('');

  // التحكم في حركة الخلفية عند تحريك الماوس
  let mouseX = $state(0);
  let mouseY = $state(0);

  function handleMouseMove(e: MouseEvent) {
    mouseX = (e.clientX / window.innerWidth - 0.5);
    mouseY = (e.clientY / window.innerHeight - 0.5);
  }

  function togglePasswordVisibility() {
    showPassword = !showPassword;
  }

  async function handleSubmit(e: SubmitEvent) {
    e.preventDefault();
    error = '';
    isLoading = true;

    try {
      const response = await api.post('/auth/login', {
        phone,
        password
      });

      const { token, employeeId, name, role, warehouseId } = response.data;

      auth.login(token, {
        employeeId,
        name,
        role,
        warehouseId
      });

      isSuccess = true;

      setTimeout(() => {
        goto('/dashboard');
      }, 1500);
    } catch (err: any) {
      error = err.response?.data?.detail || 'Login failed. Please check your credentials.';
      isLoading = false;
    }
  }

  // Check if already authenticated - only runs in browser
  onMount(() => {
    auth.checkAuth();
    const unsubscribe = auth.subscribe(state => {
      if (state.isAuthenticated) {
        goto('/dashboard');
      }
    });
    return unsubscribe;
  });
</script>

<svelte:head>
  <title>Login | Wholesale Pro Enterprise Suite</title>
  <!-- Material Symbols Outlined Font -->
  <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&display=swap" />
</svelte:head>

<div class="min-h-screen flex items-center justify-center p-md overflow-hidden relative bg-background" onmousemove={handleMouseMove}>
  
  <!-- Atmospheric Background Decoration -->
  <div class="absolute inset-0 z-0 pointer-events-none opacity-40">
    <div 
      class="absolute top-[-10%] right-[-5%] w-[40vw] h-[40vw] rounded-full bg-primary-fixed filter blur-[120px] transition-transform duration-300 ease-out"
      style:transform="translate({mouseX * 20}px, {mouseY * 20}px)"
    ></div>
    <div 
      class="absolute bottom-[-10%] left-[-5%] w-[30vw] h-[30vw] rounded-full bg-secondary-fixed filter blur-[100px] transition-transform duration-300 ease-out"
      style:transform="translate({mouseX * 40}px, {mouseY * 40}px)"
    ></div>
  </div>

  <!-- Main Content Shell -->
  <main class="w-full max-w-[440px] z-10">
    <!-- Login Card -->
    <div class="bg-surface-container-lowest border border-outline-variant rounded-xl shadow-sm p-xl">
      <div class="mb-lg">
        <h2 class="text-headline-md font-semibold text-on-surface">System Sign In</h2>
        <p class="text-label-md text-on-surface-variant uppercase tracking-wider mt-xs">Secure Authorization Required</p>
      </div>

      <form class="space-y-lg" onsubmit={handleSubmit}>
        {#if error}
          <div class="bg-error-container text-on-error-container p-sm rounded-lg text-label-md mb-lg">
            {error}
          </div>
        {/if}

        <!-- Phone Field -->
        <div class="space-y-sm">
          <label class="block text-label-md text-on-surface-variant" for="phone">Phone Number</label>
          <div class="relative group">
            <span class="material-symbols-outlined absolute left-md top-1/2 -translate-y-1/2 text-outline group-focus-within:text-primary transition-colors">phone</span>
            <input
              bind:value={phone}
              class="w-full pl-[44px] pr-md py-3 bg-surface border border-outline-variant rounded-lg text-body-md text-on-surface placeholder:text-outline input-focus-ring transition-all"
              id="phone"
              name="phone"
              placeholder="+1234567890"
              required
              type="tel"
            />
          </div>
        </div>

        <!-- Password Field -->
        <div class="space-y-sm">
          <div class="flex justify-between items-center">
            <label class="block text-label-md text-on-surface-variant" for="password">Security Password</label>
            <a class="text-label-md text-primary hover:underline transition-all" href="#forgot">Forgot Password?</a>
          </div>
          <div class="relative group">
            <span class="material-symbols-outlined absolute left-md top-1/2 -translate-y-1/2 text-outline group-focus-within:text-primary transition-colors">lock</span>
            <input 
              bind:value={password}
              class="w-full pl-[44px] pr-[44px] py-3 bg-surface border border-outline-variant rounded-lg text-body-md text-on-surface placeholder:text-outline input-focus-ring transition-all" 
              id="password" 
              name="password" 
              placeholder="••••••••" 
              required 
              type={showPassword ? 'text' : 'password'}
            />
            <button 
              type="button" 
              onclick={togglePasswordVisibility}
              class="absolute right-md top-1/2 -translate-y-1/2 text-outline hover:text-on-surface transition-colors" 
              id="togglePassword"
            >
              <span class="material-symbols-outlined">{showPassword ? 'visibility_off' : 'visibility'}</span>
            </button>
          </div>
        </div>

        <!-- Remember Me Toggle -->
        <div class="flex items-center gap-sm">
          <input 
            bind:checked={remember}
            class="w-4 h-4 rounded border-outline-variant text-primary focus:ring-primary cursor-pointer" 
            id="remember" 
            type="checkbox"
          />
          <label class="text-body-md text-on-surface-variant select-none cursor-pointer" for="remember">
            Remember this workstation
          </label>
        </div>

        <!-- Action Button -->
        <button 
          class="w-full bg-primary hover:bg-primary-container text-white text-body-lg font-medium py-3 rounded-lg shadow-sm active:scale-[0.98] transition-all flex items-center justify-center gap-sm" 
          type="submit"
        >
          Sign In to Dashboard
          <span class="material-symbols-outlined text-[18px]">login</span>
        </button>
      </form>
    </div>
  </main>

  <!-- Success / Loading Feedback Overlay -->
  {#if isLoading || isSuccess}
    <div class="fixed inset-0 bg-surface/80 backdrop-blur-sm z-50 flex flex-col items-center justify-center">
      {#if isLoading}
        <div class="w-12 h-12 border-4 border-primary border-t-transparent rounded-full animate-spin"></div>
        <p class="mt-md text-label-md text-on-surface animate-pulse">Authenticating with Central Server...</p>
      {:else if isSuccess}
        <div class="flex flex-col items-center animate-bounce">
          <span class="material-symbols-outlined text-primary text-[64px]" style="font-variation-settings: 'FILL' 1;">check_circle</span>
          <p class="mt-md text-headline-md text-on-surface">Access Granted</p>
          <p class="text-body-md text-on-surface-variant">Redirecting to Warehouse Dashboard...</p>
        </div>
      {/if}
    </div>
  {/if}
</div>

<style>
  .material-symbols-outlined {
    font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
  }

  .input-focus-ring:focus {
    outline: none;
    border-color: #004ac6;
    box-shadow: 0 0 0 2px rgba(0, 74, 198, 0.1);
  }
</style>