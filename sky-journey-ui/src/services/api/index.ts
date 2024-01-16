import { Env } from "../../config/environment";
import { Client } from "../nswag"

export const ApiClient=()=>{
    const url = Env.apiEnv ?? "https://localhost:7116";
    return new Client(url);
}
