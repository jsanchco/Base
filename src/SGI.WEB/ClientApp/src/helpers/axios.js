import axios from "axios";
import { config } from "../constants/defaultValues";

axios.defaults.baseURL = config.URL_API;
axios.defaults.headers.common = { "Authorization": `Bearer ${localStorage.getItem("jwt")}` }
export default axios;