import React, { useState } from "react";
import { loginUser, registerUser } from "../services/api";

function Login() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [message, setMessage] = useState("");

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            await loginUser(username, password);
            setMessage("Login successful!");
        } catch (err: any) {
            setMessage(err.message);
        }
    }

    return (
        <form onSubmit={handleSubmit} className="max-w-sm mx-auto">
            <h2 className="text-xl font-bold mb-4">Login</h2>
            <input
                className="w-full p-2 mb-2 border"
                type="text"
                placeholder="username"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
            />
            <input
                type="password"
                placeholder="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />
            <button
                type="submit"
                className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700"
            >Login</button>
            {message && <p className="mt-2 text-sm text-red-600">{message}</p>}
        </form>
    )
};

export default Login;