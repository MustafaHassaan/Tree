---
name: Core Enterprise
colors:
  surface: '#f7f9fb'
  surface-dim: '#d8dadc'
  surface-bright: '#f7f9fb'
  surface-container-lowest: '#ffffff'
  surface-container-low: '#f2f4f6'
  surface-container: '#eceef0'
  surface-container-high: '#e6e8ea'
  surface-container-highest: '#e0e3e5'
  on-surface: '#191c1e'
  on-surface-variant: '#434655'
  inverse-surface: '#2d3133'
  inverse-on-surface: '#eff1f3'
  outline: '#737686'
  outline-variant: '#c3c6d7'
  surface-tint: '#0053db'
  primary: '#004ac6'
  on-primary: '#ffffff'
  primary-container: '#2563eb'
  on-primary-container: '#eeefff'
  inverse-primary: '#b4c5ff'
  secondary: '#515f74'
  on-secondary: '#ffffff'
  secondary-container: '#d5e3fc'
  on-secondary-container: '#57657a'
  tertiary: '#943700'
  on-tertiary: '#ffffff'
  tertiary-container: '#bc4800'
  on-tertiary-container: '#ffede6'
  error: '#ba1a1a'
  on-error: '#ffffff'
  error-container: '#ffdad6'
  on-error-container: '#93000a'
  primary-fixed: '#dbe1ff'
  primary-fixed-dim: '#b4c5ff'
  on-primary-fixed: '#00174b'
  on-primary-fixed-variant: '#003ea8'
  secondary-fixed: '#d5e3fc'
  secondary-fixed-dim: '#b9c7df'
  on-secondary-fixed: '#0d1c2e'
  on-secondary-fixed-variant: '#3a485b'
  tertiary-fixed: '#ffdbcd'
  tertiary-fixed-dim: '#ffb596'
  on-tertiary-fixed: '#360f00'
  on-tertiary-fixed-variant: '#7d2d00'
  background: '#f7f9fb'
  on-background: '#191c1e'
  surface-variant: '#e0e3e5'
typography:
  display-lg:
    fontFamily: Inter
    fontSize: 36px
    fontWeight: '700'
    lineHeight: 44px
    letterSpacing: -0.02em
  headline-lg:
    fontFamily: Inter
    fontSize: 24px
    fontWeight: '600'
    lineHeight: 32px
    letterSpacing: -0.01em
  headline-md:
    fontFamily: Inter
    fontSize: 20px
    fontWeight: '600'
    lineHeight: 28px
  title-lg:
    fontFamily: Inter
    fontSize: 18px
    fontWeight: '600'
    lineHeight: 26px
  body-lg:
    fontFamily: Inter
    fontSize: 16px
    fontWeight: '400'
    lineHeight: 24px
  body-md:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '400'
    lineHeight: 20px
  label-md:
    fontFamily: Inter
    fontSize: 12px
    fontWeight: '500'
    lineHeight: 16px
    letterSpacing: 0.01em
  label-sm:
    fontFamily: Inter
    fontSize: 11px
    fontWeight: '600'
    lineHeight: 16px
rounded:
  sm: 0.125rem
  DEFAULT: 0.25rem
  md: 0.375rem
  lg: 0.5rem
  xl: 0.75rem
  full: 9999px
spacing:
  unit: 4px
  xs: 4px
  sm: 8px
  md: 16px
  lg: 24px
  xl: 32px
  container-margin: 32px
  column-gutter: 20px
---

## Brand & Style
The design system is engineered for high-density B2B wholesale environments where clarity, speed of navigation, and reliability are paramount. The personality is institutional yet modern—evoking a sense of precision and "quiet power" through a refined minimalist aesthetic. 

The visual strategy employs a **Corporate / Modern** style characterized by:
- **Functional Minimalism:** Eliminating unnecessary decorative elements to focus on data density and actionable insights.
- **Structural Integrity:** A rigid adherence to grid systems and logical information architecture.
- **Calm Professionalism:** A palette and layout that reduces cognitive load during long periods of use.
- **High Readability:** Prioritizing typographic clarity for complex SKU lists, inventory counts, and financial data.

## Colors
This design system utilizes a high-contrast, light-mode-first palette designed for long-term workspace comfort.

- **Primary Blue (#2563EB):** Used exclusively for primary actions, active states, and essential brand identifiers. It signifies utility and trust.
- **Slate Accents (#475569):** Applied to secondary icons, labels, and metadata to provide contrast without competing with the primary action.
- **Light Gray Backgrounds (#F8FAFC):** Used for the main application canvas to separate content areas from the pure white surfaces of cards and inputs.
- **Semantic Colors:**
    - **Success:** #10B981 (Green) for fulfilled orders and stock availability.
    - **Warning:** #F59E0B (Amber) for low stock or pending approvals.
    - **Destructive:** #EF4444 (Red) for cancellations or stock-outs.

## Typography
Inter is the foundational typeface, selected for its exceptional legibility in data-heavy enterprise interfaces. 

- **Scale:** The system uses a tight scale to maximize information density. `body-md` (14px) is the standard size for most UI text, including table rows and form labels.
- **Weights:** Regular (400) is used for body text; Medium (500) for labels and navigation; Semibold (600) for section headers; Bold (700) is reserved for page titles.
- **Readability:** High x-height and open counters ensure that numbers (SKUs, prices, quantities) remain distinct even at smaller sizes.

## Layout & Spacing
The layout follows a **Fluid Grid** model with fixed-width sidebars.

- **Grid:** A 12-column grid system is used for the main content area.
- **Sidebar:** A persistent left-hand navigation set to a fixed 260px width.
- **Breakpoints:**
    - **Desktop (1280px+):** Full 12-column visibility with 32px margins.
    - **Tablet (768px - 1279px):** Sidebar collapses to icon-only (64px) or becomes a drawer. Margins reduce to 20px.
    - **Mobile (<767px):** Single-column stack. Navigation moves to a bottom bar or top hamburger menu.
- **Density:** Spacing is modular. Use `md` (16px) for standard padding within cards and `lg` (24px) for spacing between major sections.

## Elevation & Depth
Depth is used sparingly to maintain a "flat" professional feel, utilizing **Tonal Layers** and **Subtle Ambient Shadows**.

- **Level 0 (Background):** #F8FAFC. The base canvas.
- **Level 1 (Cards/Surface):** Pure #FFFFFF with a 1px border of #E2E8F0. This is the primary work surface.
- **Level 2 (Hover/Active):** A very soft shadow: `0px 4px 6px -1px rgba(0, 0, 0, 0.05), 0px 2px 4px -2px rgba(0, 0, 0, 0.05)`.
- **Level 3 (Modals/Popovers):** Standard elevation shadow to pull the element off the page: `0px 20px 25px -5px rgba(0, 0, 0, 0.1)`.

## Shapes
The design system uses a **Soft** shape language to balance the clinical nature of enterprise data. 

- **Base Radius (4px):** Standard for buttons, input fields, and small UI elements. 
- **Large Radius (8px):** Used for cards and containers.
- **Extra Large Radius (12px):** Reserved for large modal overlays or distinctive dashboard widgets.
- **Consistency:** Never use fully rounded (pill) shapes for functional elements; keep them rectangular with soft corners to maintain a "structured" business appearance.

## Components
- **Buttons:**
    - *Primary:* Filled #2563EB with white text. 4px border radius.
    - *Secondary:* Ghost style with #E2E8F0 border and #475569 text.
- **Tables (Enterprise Grade):**
    - Header rows use `label-sm` with a light gray background (#F1F5F9).
    - Rows have a 1px bottom border (#E2E8F0).
    - Hover state on rows uses a subtle blue tint (#EFF6FF).
- **Cards:** White background, 1px border, 8px radius. Use for grouping related data like "Order Summary" or "Customer Details."
- **Input Fields:** 1px solid #D1D5DB border. On focus, the border changes to #2563EB with a 2px soft blue glow.
- **Sidebar Navigation:**
    - High-contrast Slate-800 or Slate-900 background.
    - Active states indicated by a left-hand 3px Primary Blue "accent bar" and a slightly lighter background tint.
- **Status Chips:** Small, semi-transparent background with high-contrast text (e.g., Light Green background with Dark Green text).