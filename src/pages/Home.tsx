import { useState } from "react";

export default function Home() {
    const [count, setCount] = useState(0);
    return (
        <div className="flex items-center justify-center h-screen flex-col gap-4">
            <h1 className="text">Home</h1>
            <button className="bg-black text-white p-2 rounded-md" onClick={() => setCount(count + 1)}>Count: {count}</button>
        </div>
    );
}