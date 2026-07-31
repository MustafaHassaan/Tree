<script lang="ts">
  interface Props {
    title: string;
    value: string;
    change: string;
    icon: string;
    progressPercentage: number;
    variant?: 'primary' | 'secondary' | 'tertiary' | 'error';
  }

  let { 
    title, 
    value, 
    change, 
    icon, 
    progressPercentage, 
    variant = 'primary' 
  }: Props = $props();

  const variantStyles = {
    primary: { iconBg: 'bg-primary/10', iconColor: 'text-primary', badgeBg: 'bg-primary/10', badgeColor: 'text-primary', barBg: 'bg-primary' },
    secondary: { iconBg: 'bg-secondary/10', iconColor: 'text-secondary', badgeBg: 'bg-primary/10', badgeColor: 'text-primary', barBg: 'bg-secondary' },
    tertiary: { iconBg: 'bg-tertiary-container/20', iconColor: 'text-tertiary', badgeBg: 'bg-tertiary-fixed/30', badgeColor: 'text-on-tertiary-fixed-variant', barBg: 'bg-tertiary' },
    error: { iconBg: 'bg-error/10', iconColor: 'text-error', badgeBg: 'bg-error/10', badgeColor: 'text-error', barBg: 'bg-error', border: 'border-l-4 border-l-error' }
  };

  const style = $derived(variantStyles[variant]);
</script>

<div class="bg-surface-container-lowest p-lg border border-outline-variant rounded-xl shadow-sm hover:shadow-md transition-shadow {style.border ?? ''}">
  <div class="flex justify-between items-start mb-md">
    <div class="p-2 {style.iconBg} rounded-lg">
      <span class="material-symbols-outlined {style.iconColor}" style={variant === 'error' ? "font-variation-settings: 'FILL' 1;" : ''}>
        {icon}
      </span>
    </div>
    <span class="{style.badgeColor} font-bold text-label-sm px-2 py-1 {style.badgeBg} rounded-full">
      {change}
    </span>
  </div>
  <p class="text-on-surface-variant font-label-md uppercase tracking-wide">{title}</p>
  <h2 class="text-headline-lg font-bold mt-1 {variant === 'error' ? 'text-error' : ''}">{value}</h2>
  <div class="mt-md w-full h-1 bg-surface-container rounded-full overflow-hidden">
    <div class="{style.barBg} h-full" style="width: {progressPercentage}%"></div>
  </div>
</div>