import axios from "@/lib/axios_customize";

export const callLogin = (emailOrUsername: string, password: string) => {
    return axios.post('/auth/login', { usernameOrEmail: emailOrUsername, password: password })
}

export const callSignup = (name: string, email: string, password: string) => {
    return axios.post('/auth/register', { Username: name, email, password })
}