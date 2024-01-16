import { ReservationRequest } from "../nswag";
import { ApiClient } from "../api"

export const reservationService=(reservation?:ReservationRequest)=>{
    return ApiClient().save(reservation);
}
