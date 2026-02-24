import { Route, Routes, BrowserRouter } from 'react-router-dom';
import Home from "@/pages/Home.tsx";
import { LoginForm } from "@/pages/login-form";
import { SignupForm } from "@/pages/signup-form";
import Dashboard from "@/pages/Dashboard";

export default function RoutesApp() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<LoginForm />} />
                <Route path="/register" element={<SignupForm />} />
                <Route path="/dashboard" element={<Dashboard />} />
            </Routes>

            <Routes>
                <Route path="*" element={<h1 className='flex items-center justify-center mt-96'>404 Not Found</h1>} />
            </Routes>
        </BrowserRouter>
    )
}