export function decodeToken() {
    const token = localStorage.getItem("token");
    if (!token) return null;

    try {
        const payload = token.split('.')[1];
        const decoded = JSON.parse(atob(payload));
        return decoded;
    } catch {
        return null;
    }
}

export function showAlert(message, type = "success") {
    const alertsContainer = document.getElementById("alerts");

    const alert = document.createElement("div");
    alert.className = `alert alert-${type} alert-dismissible fade show`;
    alert.role = "alert";

    alert.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;

    alertsContainer.appendChild(alert);

    // auto remove
    setTimeout(() => {
        alert.classList.remove("show");
        alert.remove();
    }, 3000);
}