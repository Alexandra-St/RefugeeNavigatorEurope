(() => {
    const root = document.documentElement;
    const preference = window.matchMedia("(prefers-color-scheme: dark)");
    const valid = value => value === "light" || value === "dark";
    const syncToggle = () => {
        const theme = root.dataset.theme;
        document.querySelectorAll("[data-theme-toggle]").forEach(button => {
            button.setAttribute("aria-pressed", String(theme === "dark"));
            button.setAttribute("aria-label", theme === "dark" ? "Switch to light theme (dark theme active)" : "Switch to dark theme (light theme active)");
            button.textContent = theme === "dark" ? "☀" : "☾";
        });
    };
    const apply = () => {
        let saved = null;
        try { saved = localStorage.getItem("theme"); } catch {}
        const theme = valid(saved) ? saved : (preference.matches ? "dark" : "light");
        root.dataset.theme = theme;
        syncToggle();
    };
    window.syncThemeToggle = syncToggle;
    window.applyEffectiveTheme = apply;
    apply();
    document.addEventListener("DOMContentLoaded", () => {
        syncToggle();
    });
    document.addEventListener("click", event => {
        const target = event.target;
        if (!(target instanceof Element)) return;
        if (!target.closest("[data-theme-toggle]")) return;
        const nextTheme = root.dataset.theme === "dark" ? "light" : "dark";
        try { localStorage.setItem("theme", nextTheme); } catch {}
        apply();
    });
    preference.addEventListener("change", event => {
        let saved = null;
        try { saved = localStorage.getItem("theme"); } catch {}
        if (!valid(saved)) apply();
    });
})();
