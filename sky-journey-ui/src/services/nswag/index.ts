//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

import { PaginationResponse } from "../../common/types/pagination-response";

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

export interface IClient {
  /**
   * @param body (optional)
   * @return Success
   */
  save(body?: ReservationRequest | undefined): Promise<BooleanMainResponse>;

  /**
   * @param body (optional)
   * @return Success
   */
  findAll(
    body?: FlightRequest | undefined
  ): Promise<FlightResponseMainResponse>;

  /**
   * @return Success
   */
  cities(): Promise<CityResponseIEnumerableMainResponse>;
}

export class Client implements IClient {
  private http: {
    fetch(url: RequestInfo, init?: RequestInit): Promise<Response>;
  };
  private baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined =
    undefined;

  constructor(
    baseUrl?: string,
    http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }
  ) {
    this.http = http ? http : (window as any);
    this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
  }

  /**
   * @return Success
   */
  cities(): Promise<CityResponseIEnumerableMainResponse> {
    let url_ = this.baseUrl + "/api/Cities";
    url_ = url_.replace(/[?&]$/, "");

    let options_: RequestInit = {
      method: "GET",
      headers: {
        Accept: "text/plain",
      },
    };

    return this.http.fetch(url_, options_).then((_response: Response) => {
      return this.processCities(_response);
    });
  }

  protected processCities(
    response: Response
  ): Promise<CityResponseIEnumerableMainResponse> {
    const status = response.status;
    let _headers: any = {};
    if (response.headers && response.headers.forEach) {
      response.headers.forEach((v: any, k: any) => (_headers[k] = v));
    }
    if (status === 200) {
      return response.text().then((_responseText) => {
        let result200: any = null;
        let resultData200 =
          _responseText === ""
            ? null
            : JSON.parse(_responseText, this.jsonParseReviver);
        result200 = CityResponseIEnumerableMainResponse.fromJS(resultData200);
        return result200;
      });
    } else if (status !== 200 && status !== 204) {
      return response.text().then((_responseText) => {
        return throwException(
          "An unexpected server error occurred.",
          status,
          _responseText,
          _headers
        );
      });
    }
    return Promise.resolve<CityResponseIEnumerableMainResponse>(null as any);
  }

  /**
   * @param body (optional)
   * @return Success
   */
  save(body?: ReservationRequest | undefined): Promise<BooleanMainResponse> {
    let url_ = this.baseUrl + "/api/Flight/save";
    url_ = url_.replace(/[?&]$/, "");

    const content_ = JSON.stringify(body);

    let options_: RequestInit = {
      body: content_,
      method: "POST",
      headers: {
        "Content-Type": "application/json-patch+json",
        Accept: "text/plain",
      },
    };

    return this.http.fetch(url_, options_).then((_response: Response) => {
      return this.processSave(_response);
    });
  }

  protected processSave(response: Response): Promise<BooleanMainResponse> {
    const status = response.status;
    let _headers: any = {};
    if (response.headers && response.headers.forEach) {
      response.headers.forEach((v: any, k: any) => (_headers[k] = v));
    }
    if (status === 200) {
      return response.text().then((_responseText) => {
        let result200: any = null;
        result200 =
          _responseText === ""
            ? null
            : (JSON.parse(
                _responseText,
                this.jsonParseReviver
              ) as BooleanMainResponse);
        return result200;
      });
    } else if (status !== 200 && status !== 204) {
      return response.text().then((_responseText) => {
        return throwException(
          "An unexpected server error occurred.",
          status,
          _responseText,
          _headers
        );
      });
    }
    return Promise.resolve<BooleanMainResponse>(null as any);
  }

  /**
   * @param body (optional)
   * @return Success
   */
  findAll(
    body?: FlightRequest | undefined
  ): Promise<FlightResponseMainResponse> {
    let url_ = this.baseUrl + "/api/Flight/findAll";
    url_ = url_.replace(/[?&]$/, "");

    const content_ = JSON.stringify(body);

    let options_: RequestInit = {
      body: content_,
      method: "POST",
      headers: {
        "Content-Type": "application/json-patch+json",
        Accept: "text/plain",
      },
    };

    return this.http.fetch(url_, options_).then((_response: Response) => {
      return this.processFindAll(_response);
    });
  }

  protected processFindAll(
    response: Response
  ): Promise<FlightResponseMainResponse> {
    const status = response.status;
    let _headers: any = {};
    if (response.headers && response.headers.forEach) {
      response.headers.forEach((v: any, k: any) => (_headers[k] = v));
    }
    if (status === 200) {
      return response.text().then((_responseText) => {
        let result200: any = null;
        result200 =
          _responseText === ""
            ? null
            : (JSON.parse(
                _responseText,
                this.jsonParseReviver
              ) as FlightResponseMainResponse);
        return result200;
      });
    } else if (status !== 200 && status !== 204) {
      return response.text().then((_responseText) => {
        return throwException(
          "An unexpected server error occurred.",
          status,
          _responseText,
          _headers
        );
      });
    }
    return Promise.resolve<FlightResponseMainResponse>(null as any);
  }
}

export interface BooleanMainResponse {
  response?: boolean;
  message?: string;
  error?: boolean;
}
export class CityResponse implements ICityResponse {
  value: string = "";
  label: string = "";

  constructor(data?: ICityResponse) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init(_data?: any) {
    if (_data) {
      this.value = _data["value"];
      this.label = _data["label"];
    }
  }

  static fromJS(data: any): CityResponse {
    data = typeof data === "object" ? data : {};
    let result = new CityResponse();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === "object" ? data : {};
    data["value"] = this.value;
    data["label"] = this.label;
    return data;
  }
}

export interface ICityResponse {
  value: string;
  label: string;
}

export class CityResponseIEnumerableMainResponse
  implements ICityResponseIEnumerableMainResponse
{
  response?: Array<CityResponse> | undefined;

  constructor(data?: ICityResponseIEnumerableMainResponse) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init(_data?: any) {
    if (_data) {
      if (Array.isArray(_data["response"])) {
        this.response = [] as any;
        for (let item of _data["response"])
          this.response!.push(CityResponse.fromJS(item));
      }
    }
  }

  static fromJS(data: any): CityResponseIEnumerableMainResponse {
    data = typeof data === "object" ? data : {};
    let result = new CityResponseIEnumerableMainResponse();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === "object" ? data : {};
    if (Array.isArray(this.response)) {
      data["response"] = [];
      for (let item of this.response) data["response"].push(item.toJSON());
    }
    return data;
  }
}

export interface ICityResponseIEnumerableMainResponse {
  response?: CityResponse[] | undefined;
}

export interface FlightRequest {
  customersCount?: number;
  departureCity?: string | undefined;
  arrivalCity?: string | undefined;
  departureDate?: Date;
  arrivalDate?: Date;
  pageNumber?: number;
  pageSize?: number;
}

export interface FlightResponse {
  id?: number;
  airline?: string | undefined;
  flightNumber?: string | undefined;
  departureCity?: string | undefined;
  arrivalCity?: string | undefined;
  departureDate?: Date;
  arrivalDate?: Date;
  price?: number;
  numberOfAvailableSeats?: number;
  plan?: PlanResponse;
  reservations?: ReservationResponse[] | undefined;
}

export interface FlightResponseMainResponse {
  response?: PaginationResponse<FlightResponse>;
  message?: string;
  error?: boolean;
}

export interface PassengerRequest {
  lastName?: string | undefined;
  firstName?: string | undefined;
  email?: string | undefined;
  flightId?: number;
  [key: string]: string | number | undefined; // Add an index signature
}

export interface PassengerResponse {
  lastName?: string | undefined;
  firstName?: string | undefined;
  email?: string | undefined;
  numberSeat?: string | undefined;
  reservationId?: number;
}

export interface PlanResponse {
  id?: number;
  modelName?: string | undefined;
  flights?: FlightResponse[] | undefined;
}

export interface ReservationRequest {
  flightId?: number;
  passengers?: PassengerRequest[] | undefined;
}

export interface ReservationResponse {
  id?: number;
  flightId?: number;
  numberOfPassengers?: number;
  passengerName?: string | undefined;
  seatNumber?: string | undefined;
  dateReservation?: Date;
  passengers?: PassengerResponse[] | undefined;
}

export class ApiException extends Error {
  override message: string;
  status: number;
  response: string;
  headers: { [key: string]: any };
  result: any;

  constructor(
    message: string,
    status: number,
    response: string,
    headers: { [key: string]: any },
    result: any
  ) {
    super();

    this.message = message;
    this.status = status;
    this.response = response;
    this.headers = headers;
    this.result = result;
  }

  protected isApiException = true;

  static isApiException(obj: any): obj is ApiException {
    return obj.isApiException === true;
  }
}

function throwException(
  message: string,
  status: number,
  response: string,
  headers: { [key: string]: any },
  result?: any
): any {
  if (result !== null && result !== undefined) throw result;
  else throw new ApiException(message, status, response, headers, null);
}
