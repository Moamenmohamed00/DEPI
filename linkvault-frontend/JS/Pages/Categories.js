const BASE_URL = "http://linkvaultapi.runasp.net/api";

// 🔐 check token
const token = localStorage.getItem("token");
if (!token) {
    window.location.href = "login.html";
}

// 📦 عناصر من الصفحة
const categoryList = document.getElementById("categoryList");
const categoryForm = document.getElementById("categoryForm");

// ============================
// 📥 GET Categories
// ============================
async function loadCategories() {
    try {
        const response = await fetch(`${BASE_URL}/categories`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        const data = await response.json();

        renderCategories(data);

    } catch (error) {
        console.error("Error loading categories:", error);
    }
}

// ============================
// 🎨 Render
// ============================
function renderCategories(categories) {
    categoryList.innerHTML = "";

    categories.forEach(cat => {
        const div = document.createElement("div");
        div.className = "category-card";

        div.innerHTML = `
            <div>
                <strong>${cat.categoryName}</strong>
                <p>${cat.description || ""}</p>
            </div>
            <div class="actions">
                <button onclick="deleteCategory(${cat.id})">Delete</button>
            </div>
        `;

        categoryList.appendChild(div);
    });
}

// ============================
// ➕ Add Category
// ============================
categoryForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const categoryName = document.getElementById("categoryName").value;
    const description = document.getElementById("description").value;

    try {
        const response = await fetch(`${BASE_URL}/categories`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify({
                categoryName,
                description
            })
        });

        if (response.ok) {
            categoryForm.reset();
            loadCategories(); // refresh
        } else {
            alert("Failed to add category ❌");
        }

    } catch (error) {
        console.error("Error adding category:", error);
    }
});

// ============================
// ❌ Delete Category
// ============================
async function deleteCategory(id) {
    if (!confirm("Are you sure?")) return;

    try {
        const response = await fetch(`${BASE_URL}/categories/${id}`, {
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.ok) {
            loadCategories();
        } else {
            alert("Delete failed ❌");
        }

    } catch (error) {
        console.error("Error deleting:", error);
    }
}

// ============================
// 🚀 Init
// ============================
loadCategories();