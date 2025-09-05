const API_BASE = "http://localhost:5000";

export const registerUser = async (username: string, password: string) => {
    const res = await fetch(`${API_BASE}/auth/register`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({username, password})
    });
    if (!res.ok) throw new Error(await res.text());
};

export const loginUser = async (username: string, password: string) => {
    const res = await fetch(`${API_BASE}/auth/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({username, password }),
    });
    if (!res.ok) throw new Error(await res.text());

    const data = await res.json();
    localStorage.setItem("token", data.token);
}