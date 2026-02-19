import axios from "@/lib/axios_customize";

export const callLogin = (emailOrUsername: string, password: string) => {
    return axios.post('/auth/login', { usernameOrEmail: emailOrUsername, password: password })
}
