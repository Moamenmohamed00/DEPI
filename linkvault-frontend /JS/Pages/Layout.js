import { decodeToken } from "./utils.js";

export function loadNavbar() {
    const token = localStorage.getItem("token");

    if (!token) return;

    const payload = decodeToken();

    const email =
        payload?.email ||
        payload?.["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] ||
        "User";

    const navbar = document.createElement("div");
    navbar.className = "navbar";

    navbar.innerHTML = `
        <a href="categories.html">Categories</a>
        <a href="bookmark.html">Bookmarks</a>
        <a href="notes.html">Notes</a>

        <span style="float:right">
            ${email}
            <button onclick="logout()">Logout</button>
        </span>
    `;

    document.body.prepend(navbar);
}
export function logout() {
    localStorage.removeItem("token");
    window.location.href = "login.html";
}
window.logout=logout;
