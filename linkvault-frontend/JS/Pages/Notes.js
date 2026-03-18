const BASE_URL = "http://linkvaultapi.runasp.net/api";

// 🔐 check token
const token = localStorage.getItem("token");
if (!token) {
    window.location.href = "login.html";
}

// 📦 عناصر الصفحة
const notesList = document.getElementById("notesList");
const noteForm = document.getElementById("noteForm");

// ============================
// 📥 GET Notes (with filters)
// ============================
async function loadNotes(query = "") {
    try {
        const response = await fetch(`${BASE_URL}/notes${query}`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        const data = await response.json();

        renderNotes(data);

    } catch (error) {
        console.error("Error loading notes:", error);
    }
}

// ============================
// 🎨 Render
// ============================
function renderNotes(notes) {
    notesList.innerHTML = "";

    notes.forEach(note => {
        const div = document.createElement("div");
        div.className = "note-card";

        if (note.pinned) {
            div.classList.add("pinned");
        }

        div.innerHTML = `
            <strong>${note.title}</strong>
            <p>${note.content}</p>

            <div class="actions">
                <button onclick="togglePin(${note.id})">
                    ${note.pinned ? "Unpin" : "Pin"}
                </button>
                <button onclick="deleteNote(${note.id})">Delete</button>
            </div>
        `;

        notesList.appendChild(div);
    });
}

// ============================
// ➕ Add Note
// ============================
noteForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const title = document.getElementById("title").value;
    const content = document.getElementById("content").value;
    const categoryId = document.getElementById("categoryId").value;

    try {
        const response = await fetch(`${BASE_URL}/notes`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify({
                title,
                content,
                categoryId: Number(categoryId)
            })
        });

        if (response.ok) {
            noteForm.reset();
            loadNotes();
        } else {
            alert("Failed to add note ❌");
        }

    } catch (error) {
        console.error("Error adding note:", error);
    }
});

// ============================
// ❌ Delete Note
// ============================
async function deleteNote(id) {
    if (!confirm("Delete this note?")) return;

    try {
        const response = await fetch(`${BASE_URL}/notes/${id}`, {
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.ok) {
            loadNotes();
        } else {
            alert("Delete failed ❌");
        }

    } catch (error) {
        console.error("Error deleting:", error);
    }
}

// ============================
// 📌 Pin / Unpin
// ============================
async function togglePin(id) {
    try {
        const response = await fetch(`${BASE_URL}/notes/${id}/pin`, {
            method: "PATCH",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.ok) {
            loadNotes();
        } else {
            alert("Pin failed ❌");
        }

    } catch (error) {
        console.error("Error pinning:", error);
    }
}

// ============================
// 🔍 Filters
// ============================
function applyFilters() {
    const search = document.getElementById("searchWord").value;
    const pinned = document.getElementById("pinnedFilter").checked;

    let query = "?";

    if (search) query += `searchWord=${encodeURIComponent(search)}&`;
    if (pinned) query += `Pinned=true&`;

    loadNotes(query);
}

// ============================
// 🚀 Init
// ============================
loadNotes();