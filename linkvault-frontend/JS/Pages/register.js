const BASE_URL = "http://linkvaultapi.runasp.net/api";

document.getElementById("registerForm").addEventListener("submit", async (e) => {
    e.preventDefault();

    // 🔹 جمع البيانات من الفورم
    const firstName = document.getElementById("firstName").value;
    const lastName = document.getElementById("lastName").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    try {
        // 🔹 إرسال request للـ API
        const response = await fetch(`${BASE_URL}/auth/register`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                firstName,
                lastName,
                email,
                password
            })
        });

        const data = await response.json();

        // 🔹 لو التسجيل نجح
        if (response.ok) {
            alert("Registration successful 🎉");
            localStorage.setItem("token", data.token);
            window.location.href = "login.html";
        } else {
            console.log(data);
            alert("Registration failed ❌");
        }

    } catch (error) {
        console.error(error);
        alert("Something went wrong 🚨");
    }
});