import { FlightRequest } from "../nswag"
import { ApiClient } from "../api"

export const flightFindAll=(flight?:FlightRequest)=>{
    return ApiClient().findAll(flight);
}