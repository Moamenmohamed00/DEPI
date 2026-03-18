const BASE_URL = "http://linkvaultapi.runasp.net/api";

export async function request(endpoint, options = {}) {
    const token = localStorage.getItem("token");

    const headers = {
        "Content-Type": "application/json",
        ...(token && { Authorization: `Bearer ${token}` })
    };

    const response = await fetch(BASE_URL + endpoint, {
        ...options,
        headers: {
            ...headers,
            ...(options.headers || {})
        }
    });

    // 🔐 handle unauthorized
    if (response.status === 401) {
        localStorage.removeItem("token");
        window.location.href = "login.html";
        throw new Error("Unauthorized");
    }

    // 🧠 handle empty response (DELETE)
    if (response.status === 204) return null;

    const data = await response.json();

    if (!response.ok) {
        throw new Error(data.message || "Something went wrong");
    }

    return data;
}