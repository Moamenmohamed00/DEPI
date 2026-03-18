const BASE_URL = "http://linkvaultapi.runasp.net/api";

document.getElementById("loginForm").addEventListener("submit", async (e) => {
    e.preventDefault();

    // 🔹 جمع البيانات
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    try {
        // 🔹 إرسال request
        const response = await fetch(`${BASE_URL}/auth/login`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ email, password })
        });

        const data = await response.json();

        // 🔹 لو نجح
        if (response.ok) {
            console.log(data);

            // ✅ حفظ التوكن
            localStorage.setItem("token", data.token);

            alert("Login successful 🎉");

            // ✅ تحويل
            window.location.href = "categories.html";
        } else {
            console.log(data);
            alert("Invalid email or password ❌");
        }

    } catch (error) {
        console.error(error);
        alert("Something went wrong 🚨");
    }
});