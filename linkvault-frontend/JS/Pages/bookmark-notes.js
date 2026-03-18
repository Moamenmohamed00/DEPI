const BASE_URL = "http://linkvaultapi.runasp.net/api";

// 🔐 check token
const token = localStorage.getItem("token");
if (!token) {
    window.location.href = "login.html";
}

// 📌 نجيب bookmarkId من الـ URL
const params = new URLSearchParams(window.location.search);
const bookmarkId = params.get("bookmarkId");

if (!bookmarkId) {
    alert("Invalid bookmark ❌");
    window.location.href = "bookmark.html";
}

// 📦 عناصر الصفحة
const notesList = document.getElementById("notesList");
const noteForm = document.getElementById("noteForm");

// ============================
// 📥 GET Notes for Bookmark
// ============================
async function loadNotes() {
    try {
        const response = await fetch(`${BASE_URL}/bookmarks/${bookmarkId}/notes`, {
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

        div.innerHTML = `
            <p>${note.content}</p>
            <div class="actions">
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

    const content = document.getElementById("content").value;

    try {
        const response = await fetch(`${BASE_URL}/bookmarks/${bookmarkId}/notes`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify({ content })
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
        const response = await fetch(`${BASE_URL}/bookmarks/${bookmarkId}/notes/${id}`, {
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
        console.error("Error deleting note:", error);
    }
}

// ============================
// 🚀 Init
// ============================
loadNotes();