import { Route, Routes, BrowserRouter } from 'react-router-dom';
import Home from "@/pages/Home.tsx";
import { LoginForm } from "@/pages/login-form";
import { SignupForm } from "@/pages/signup-form";

export default function RoutesApp() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<LoginForm />} />
                <Route path="/register" element={<SignupForm />} />
            </Routes>
        </BrowserRouter>
    )
}