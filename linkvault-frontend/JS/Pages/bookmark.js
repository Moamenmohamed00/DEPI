const BASE_URL = "http://linkvaultapi.runasp.net/api";

// 🔐 check token
const token = localStorage.getItem("token");
if (!token) {
    window.location.href = "login.html";
}

// 📦 عناصر الصفحة
const bookmarkList = document.getElementById("bookmarkList");
const bookmarkForm = document.getElementById("bookmarkForm");

// ============================
// 📥 GET Bookmarks (with filters)
// ============================
async function loadBookmarks(query = "") {
    try {
        const response = await fetch(`${BASE_URL}/bookmarks${query}`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        const data = await response.json();

        renderBookmarks(data);

    } catch (error) {
        console.error("Error loading bookmarks:", error);
    }
}

// ============================
// 🎨 Render
// ============================
function renderBookmarks(bookmarks) {
    bookmarkList.innerHTML = "";

    bookmarks.forEach(b => {
        const div = document.createElement("div");
        div.className = "bookmark-card";

        div.innerHTML = `
            <strong>${b.title}</strong>
            <a href="${b.url}" target="_blank" class="link">${b.url}</a>

            <div class="actions">
                <button onclick="openNotes(${b.id})">Notes</button>
                <button onclick="deleteBookmark(${b.id})">Delete</button>
            </div>
        `;

        bookmarkList.appendChild(div);
    });
}

// ============================
// ➕ Add Bookmark
// ============================
bookmarkForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const title = document.getElementById("title").value;
    const url = document.getElementById("url").value;
    const categoryId = document.getElementById("categoryId").value;

    try {
        const response = await fetch(`${BASE_URL}/bookmarks`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify({
                title,
                url,
                categoryId: Number(categoryId)
            })
        });

        if (response.ok) {
            bookmarkForm.reset();
            loadBookmarks();
        } else {
            alert("Failed to add bookmark ❌");
        }

    } catch (error) {
        console.error("Error adding bookmark:", error);
    }
});

// ============================
// ❌ Delete Bookmark
// ============================
async function deleteBookmark(id) {
    if (!confirm("Delete this bookmark?")) return;

    try {
        const response = await fetch(`${BASE_URL}/bookmarks/${id}`, {
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.ok) {
            loadBookmarks();
        } else {
            alert("Delete failed ❌");
        }

    } catch (error) {
        console.error("Error deleting bookmark:", error);
    }
}

// ============================
// 🔍 Filters
// ============================
function applyFilters() {
    const search = document.getElementById("search").value;
    const favorite = document.getElementById("favorite").checked;
    const archived = document.getElementById("archived").checked;

    let query = "?";

    if (search) query += `Search=${encodeURIComponent(search)}&`;
    if (favorite) query += `IsFavorite=true&`;
    if (archived) query += `IsArchived=true&`;

    loadBookmarks(query);
}

// ============================
// 🔗 Navigate to Bookmark Notes
// ============================
function openNotes(id) {
    window.location.href = `bookmark-notes.html?bookmarkId=${id}`;
}

// ============================
// 🚀 Init
// ============================
loadBookmarks();