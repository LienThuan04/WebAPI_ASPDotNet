import axios from "axios";

const instance = axios.create({
    baseURL: import.meta.env.VITE_URL_BACKEND as string,
    headers: {
        "Content-Type": "application/json",
    },
    withCredentials: true, // Include cookies in requests
});


let accessToken: string | null = null;

export const setAccessToken = (token: string) => {
    accessToken = token;
};

export const getAccessToken = () => accessToken;

// Add a request interceptor to include the access token in headers
instance.interceptors.request.use((config) => {
    if (accessToken) {
        config.headers.Authorization = `Bearer ${accessToken}`;
    }
    return config;
},
    (error) => {
        return Promise.reject(error);
    }
);

instance.interceptors.response.use((response) => response,
    async (error) => {
        const originalRequest = error.config;
        //if error response is 401 and the request has not already been retried
        // only attempt refresh when the original request included an Authorization header
        if (error.response.status === 401 && !originalRequest._retry && originalRequest.headers?.Authorization && !originalRequest.url.includes("/auth/refresh-token")) {
            originalRequest._retry = true;
            try {
                // Attempt to refresh the access token
                const response = await instance.post("/auth/refresh-token", {}, {
                    withCredentials: true, // Include cookies in the refresh request
                });
                const newAccessToken = response.data.accessToken;
                setAccessToken(newAccessToken);
                // Update the original request with the new access token
                originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
                // Retry the original request
                return instance(originalRequest);
            } catch (refreshError) {
                // If refreshing the token fails, reject the original error
                console.error("Token refresh failed:", refreshError);
                if (location.pathname !== "/login") {
                    window.location.href = "/login"; // Redirect to login page
                }
                return Promise.reject(refreshError);
            }
        }
        return Promise.reject(error);
    });

export default instance;