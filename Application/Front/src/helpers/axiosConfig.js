import axios from "axios";
import { API_URL } from "./config.js";


const api = axios.create({
  baseURL: API_URL,
});

// Add token to the request headers
api.interceptors.request.use((config) => {
  const token = sessionStorage.getItem("ID_SESSION");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  } else {
    alert("No hay token para realizar la solicitud")
  }
  return config;
});

export default api;