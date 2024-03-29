import React, { ReactNode, createContext, useState } from "react";
import { IFlightListPages, IFlightObject } from "../services/nswag/types";
import {
  CityResponse,
  FlightRequest,
  FlightResponse,
  PassengerRequest,
} from "../services/nswag";
import { Alert } from "../common/types/alert";

// Define the shape of your context
export interface IMyContext {
  cities?: Array<CityResponse> | undefined;
  initialEnv?: boolean;
  flightList: Array<IFlightListPages>;
  pageNumber: number;
  pageSize: number;
  searchFlight?: IFlightObject;
  flightReserve?: FlightResponse;
  isLoading?: boolean;
  reservations?: PassengerRequest[];
  alert?: Alert;
  handleInitiaEnv?: () => void;
  handleChangeCities: (newCities: Array<CityResponse> | undefined) => void;
  handleFlightList: (listFlight: Array<FlightResponse> | undefined) => void;
  handlePageNumber: (page: number) => void;
  handlePageSize: (size: number) => void;
  handleSearchFlights: (flightSearch: IFlightObject | undefined) => void;
  handleFlightReserve: (reserveFlight?: FlightResponse) => void;
  handleReservations: (reservations?: PassengerRequest) => void;
  handleLoading: (data: boolean) => void;
  handleAlert?: (alert: Alert) => void;
}

interface IMyContextProvider {
  children: ReactNode;
}

// Create the context
export const MyContext = createContext<IMyContext>({
  flightList: [],
  pageNumber: 1,
  pageSize: 2,
  handleChangeCities: () => {},
  handleFlightList: () => {},
  handlePageNumber: (page: number) => {},
  handlePageSize: (size: number) => {},
  handleSearchFlights: (flightSearch: IFlightObject | undefined) => {},
  handleFlightReserve: (reserveFlight?: FlightResponse) => {},
  handleReservations: (reservations?: PassengerRequest) => {},
  handleLoading: (data: boolean) => {},
});

// Create a provider component
export const MyContextProvider = ({ children }: IMyContextProvider) => {
  const [initialEnv, isUpdated] = useState<boolean>(false);
  const [cities, setCities] = useState<Array<CityResponse>>();
  const [pageNumber, setPageNumber] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(2);
  const [flightList, setFlights] = useState<Array<IFlightListPages>>([]);
  const [searchFlight, setSearchFlight] = useState<FlightRequest | undefined>();
  const [flightReserve, setFlightReserve] = useState<
    FlightResponse | undefined
  >();
  const [reservations, setReservations] = useState<PassengerRequest[]>([]);
  const [alert, setAlert] = useState<Alert>();
  const [isLoading, setLoading] = useState(false);

  const handleLoading = (data: boolean) => {
    setLoading(data);
  };

  const handleReservations = (reservation?: PassengerRequest) => {
    if (reservation) {
      setReservations((pre) => [...pre, reservation]);
    }
  };

  const handleFlightReserve = (reserveFlight?: FlightResponse) => {
    setFlightReserve(reserveFlight);
  };

  const handleSearchFlights = (flightSearch: IFlightObject | undefined) => {
    setSearchFlight(flightSearch);
  };

  const handlePageSize = (size: number) => {
    setPageSize(size);
  };

  const handlePageNumber = (page: number) => {
    setPageNumber(page);
  };

  const handleFlightList = (listFlight: Array<FlightResponse> | undefined) => {
    const findFlightCheck = flightList?.find(
      (item) => item.page === pageNumber
    );
    if (!findFlightCheck && flightList) {
      setFlights([...flightList, { page: pageNumber, list: listFlight }]);
    }
  };

  const handleInitiaEnv = () => {
    isUpdated(true);
  };
  const handleChangeCities = (newCities: Array<CityResponse> | undefined) => {
    setCities(newCities);
  };

  const handleAlert = (alert: Alert) => {
    setAlert(alert);
  };

  // Provide the context value to the children components
  return (
    <MyContext.Provider
      value={{
        cities,
        initialEnv,
        flightList,
        pageNumber,
        pageSize,
        searchFlight,
        flightReserve,
        reservations,
        isLoading,
        alert,
        handleInitiaEnv,
        handleChangeCities,
        handleFlightList,
        handlePageNumber,
        handlePageSize,
        handleSearchFlights,
        handleFlightReserve,
        handleReservations,
        handleLoading,
        handleAlert
      }}
    >
      {children}
    </MyContext.Provider>
  );
};
