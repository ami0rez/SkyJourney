import {
  Autocomplete,
  Box,
  Button,
  Card,
  CardContent,
  FilterOptionsState,
  Grid,
  MenuItem,
  Select,
  Skeleton,
  TextField,
} from "@mui/material";
import React, { useContext, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { MyContext } from "../../../context";
import { getCities } from "../../../services/cities";
import { flightFindAll } from "../../../services/flights";
import {
  CityResponse,
  FlightRequest,
  FlightResponseMainResponse,
} from "../../../services/nswag";
import { IErrorMsg, IFlightObject } from "../../../services/nswag/types";
import { getCurrentDate, validationForm } from "../../../utils";
import { flightMapper } from "../../../utils/mapper/flightMapper";
import { useNotification } from "../../../components/Notification";

const FlightComponent = () => {
  const [flights, setFlights] = useState<FlightRequest | undefined>({});
  const [datas, setDatas] = useState<Array<CityResponse> | undefined>(
    undefined
  );
  const [numberPassenger, setNumberPassenger] = useState<number>(1);
  const { t } = useTranslation();
  const {
    handleFlightList,
    pageNumber,
    pageSize,
    handleLoading,
    handleSearchFlights,
  } = useContext(MyContext);

  const { showError } = useNotification();
  const [errorMsg, setErrorMsg] = useState<IErrorMsg | undefined>();

  useEffect(() => {
    const fetchCities = async () => {
      try {
        handleLoading(true);
        const cities = await getCities();
        setDatas(cities.response);
        handleLoading(false);
      } catch (error) {
        handleLoading(false);
      }
    };

    fetchCities();
  }, []);
  const filterOptions = (
    data: CityResponse[],
    { inputValue }: FilterOptionsState<CityResponse>
  ) => {
    return data.filter((option) =>
      option.label.toLowerCase().startsWith(inputValue.toLowerCase())
    );
  };

  const handleSubmit = async (event: any) => {
    event.preventDefault();
    const obj: FlightRequest = {
      ...flights,
      pageNumber: pageNumber,
      pageSize: pageSize,
      customersCount: numberPassenger,
    };
    setErrorMsg({ city: false, time: false });
    const validation = validationForm(obj);
    if (!validation.isValid) {
      setErrorMsg(validation.msg);
    } else {
      try {
        handleLoading(true);
        const res: FlightResponseMainResponse = await flightFindAll(obj);
        if(!res.response?.length){
          showError('No flights found for the search parameters')
        }
        const mapper = flightMapper(res);
        handleFlightList(mapper);
        const flightObji: IFlightObject = {
          ...obj,
          nbPassenger: numberPassenger,
        };
        handleSearchFlights(flightObji);
      } catch (error) {
        showError('Error while getting data')
      }
    }
    handleLoading(false);
  };
  
  const handleChangeDate = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    let dateValue = new Date(e.target.value).getTime();
    let currentData = new Date().getTime();
    if (currentData <= dateValue) {
      setFlights((prevPerson) => ({
        ...prevPerson,
        [name]: value,
      }));
    }
    if (currentData > dateValue) {
      setFlights((prevPerson) => ({
        ...prevPerson,
        [name]: getCurrentDate(),
      }));
    }
    if (
      flights?.departureDate &&
      flights?.arrivalDate &&
      flights?.departureDate > flights?.arrivalDate
    ) {
      setFlights((prevPerson) => ({
        ...prevPerson,
        arrivalDate: flights?.departureDate,
      }));
    }
  };
  const champErrorCity = t("searchFormular.errorMessage.City");

  const champErrorTime = t("searchFormular.errorMessage.time");
  return (
    <form onSubmit={handleSubmit}>
      {datas ? (
        <Box sx={{ padding: 2 }}>
          <Card
            variant="outlined"
            sx={{ marginBottom: 2, border: "1px solid DodgerBlue" }}
          >
            <CardContent>
              <Grid container spacing={2}>
                <Grid item xs={2}>
                  <Autocomplete
                    sx={{ width: 150 }}
                    filterSelectedOptions
                    filterOptions={filterOptions}
                    options={datas}
                    onChange={(e, value) => {
                      setFlights((pre) => ({
                        ...pre,
                        departureCity: value?.value,
                      }));
                    }}
                    renderInput={(params) => (
                      <TextField
                        {...params}
                        label={t("searchFormular.from")}
                        name="departureCity"
                        helperText={errorMsg?.city && champErrorCity}
                        error={errorMsg?.city}
                        fullWidth
                      />
                    )}
                  />
                </Grid>
                <Grid item xs={2}>
                  <Autocomplete
                    sx={{ width: 150 }}
                    filterSelectedOptions
                    filterOptions={filterOptions}
                    options={datas}
                    onChange={(e, value) => {
                      setFlights((pre) => ({
                        ...pre,
                        arrivalCity: value?.value,
                      }));
                    }}
                    renderInput={(params) => (
                      <TextField
                        {...params}
                        label={t("searchFormular.to")}
                        helperText={errorMsg?.city && champErrorCity}
                        error={errorMsg?.city}
                        name="arrivalCity"
                        fullWidth
                      />
                    )}
                  />
                </Grid>
                <Grid item xs={2}>
                  <TextField
                    sx={{ width: 150 }}
                    label={t("searchFormular.timetDep")}
                    type="date"
                    value={flights?.departureDate}
                    error={errorMsg?.time}
                    name="departureDate"
                    InputLabelProps={{ shrink: true }}
                    onChange={handleChangeDate}
                    helperText={errorMsg?.time && champErrorTime}
                  />
                </Grid>
                <Grid item xs={2}>
                  <TextField
                    sx={{ width: 150 }}
                    label={t("searchFormular.timetRet")}
                    type="date"
                    value={flights?.arrivalDate}
                    error={errorMsg?.time}
                    name="arrivalDate"
                    InputLabelProps={{ shrink: true }}
                    onChange={handleChangeDate}
                    helperText={errorMsg?.time && champErrorTime}
                  />
                </Grid>
                <Grid item xs={2}>
                  <Select
                    value={numberPassenger}
                    onChange={(e) => {
                      setNumberPassenger(Number(e.target.value));
                    }}
                    displayEmpty
                    inputProps={{ "aria-label": "Passenger Count" }}
                  >
                    {[...Array(9)].map((x, i) => (
                      <MenuItem value={i + 1} key={i}>
                        {i + 1} {t("passenger.name")}
                        {i + 1 > 1 && "s"}
                      </MenuItem>
                    ))}
                  </Select>
                </Grid>
                <Grid
                  item
                  xs={1}
                  sx={{
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                  }}
                >
                  <Button title="submit" variant="contained" type="submit">
                    {t("search")}
                  </Button>
                </Grid>
              </Grid>
            </CardContent>
          </Card>
        </Box>
      ) : (
        <Box sx={{ padding: 2, height: 150 }}>
          <Skeleton variant="rectangular" width="100%">
            <Card
              variant="outlined"
              sx={{ marginBottom: 2, border: "1px solid DodgerBlue" }}
            >
              <CardContent></CardContent>
            </Card>
          </Skeleton>
        </Box>
      )}
    </form>
  );
};

export default FlightComponent;
