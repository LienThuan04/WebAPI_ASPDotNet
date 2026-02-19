import { useState } from "react";

export default function Home() {
    const [count, setCount] = useState(0);
    return (
        <div className="flex items-center justify-center h-screen flex-col gap-4">
            <h1 className="text-3xl font-bold">Home</h1>
            <button className="bg-black text-white p-2 rounded-md" onClick={() => setCount(count + 1)}>Count: {count}</button>
            <div className="flex items-center justify-center gap-4">
                <a href="/login" className="text-blue-500 underline inline-block px-6 py-2.5 bg-slate-300 text-slate-900 font-semibold rounded-md hover:bg-slate-200 transition">Login</a>
                <a href="/register" className="text-white-500 underline inline-block px-6 py-2.5 bg-gray-600 text-white font-semibold rounded-md hover:bg-gray-900 transition shadow-lg">Register</a>
            </div>
        </div>
    );
}