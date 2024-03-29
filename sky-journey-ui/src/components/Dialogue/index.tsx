import React, { useContext, useEffect, useState } from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import { MyContext } from "../../context";
import { Box, Grid, TextField } from "@mui/material";
import { useTranslation } from "react-i18next";
import {
  BooleanMainResponse,
  PassengerRequest,
  ReservationRequest,
} from "../../services/nswag";
import { reservationService } from "../../services/reserveations";
import { validatePassengers } from "../../utils";
import { useNotification } from "../Notification";

const ResponsiveDialog = () => {
  const { flightReserve, searchFlight, reservations, handleFlightReserve } =
    useContext(MyContext);
    
  const { showError, showSuccess } = useNotification();
  const [open, setOpen] = useState(false);
  const { t } = useTranslation();
  const [passengers, setPassengers] = useState<PassengerRequest[]>([
    {
      firstName: "",
      lastName: "",
      email: "",
      index: 0,
    },
  ]);
  const [errors, setErrors] = useState<{ [key: string]: string }[]>([]);
  useEffect(() => {
    setOpen(flightReserve && searchFlight ? true : false);
  }, [flightReserve, searchFlight, reservations]);
  const myCloseModal = (
    event: {},
    reason: "backdropClick" | "escapeKeyDown"
  ) => {
    if (reason && reason === "backdropClick" && "escapeKeyDown") return;
    handleClose();
  };

  const handleClose = () => {
    handleFlightReserve(undefined);
    setPassengers([
      {
        firstName: "",
        lastName: "",
        email: "",
        phoneNumber: "",
        index: 0,
      },
    ]);
    setOpen(false);
  };

  const addPassenger = () => {
    let newPassengers = [...passengers];

    if (searchFlight?.nbPassenger) {
      for (let i = passengers.length; i < searchFlight.nbPassenger; i++) {
        newPassengers.push({
          firstName: "",
          lastName: "",
          email: "",
          phoneNumber: "",
          index: i,
        });
      }
      setPassengers(newPassengers);
    }
    return newPassengers;
  };
  const handleInputChange = (event: any, index: number) => {
    const fieldName = event.target.name;
    const parts = fieldName.split(".");
    const extractedFieldName = parts[parts.length - 1];
    const { value } = event.target;
    const updatedPassengers = [...passengers];
    if (!updatedPassengers[index]) {
      updatedPassengers[index] = {};
    }
    updatedPassengers[index] = {
      ...updatedPassengers[index],
      [extractedFieldName]: value,
      index: index,
    };
    setPassengers(updatedPassengers);
  };
  const handleAccept = async () => {
    const tempPassenger = addPassenger();
    const validationErrors = validatePassengers(
      addPassenger(),
      champsErrorFirstName,
      champsErrorLastName,
      champsErrorEmail,
      champsErrorPhoneNumber
    );
    if (validationErrors.length === 0) {
      try {
        const reservationObj: ReservationRequest = {
          flightId: flightReserve?.id,
          passengers: tempPassenger,
        };
        try {
          const res: BooleanMainResponse = await reservationService(reservationObj);
          if(res.response){
            showSuccess('Your reservation has passed successfully');
          }else{
            showError('Error while performing reservation, please try again later');
          }
        } catch (error) {
          showError('Error while performing reservation, please try again later');
        }

        handleClose();
      } catch (error) {}
    } else {
      setErrors(validationErrors);
    }
  };
  const getError = (index: number) => {
    const res = errors.find((error) => Number(error["index"]) === index);
    return res;
  };
  const champLastName = t("passenger.lastName");

  const champFirstName = t("passenger.firstName");

  const champEmail = t("passenger.email");

  const champPhoneNumber = t("passenger.phoneNumber");

  const champsErrorLastName = t("passengerError.lastName");

  const champsErrorFirstName = t("passengerError.firstName");

  const champsErrorEmail = t("passengerError.email");

  const champsErrorPhoneNumber = t("passengerError.phoneNumber");

  return (
    <div>
      <>
        {flightReserve && (
          <Dialog
            sx={{ width: "100%", height: "100%" }}
            fullWidth={true}
            open={open}
            onClose={myCloseModal}
            aria-labelledby="responsive-dialog-title"
          >
            <Box>
              <DialogContent>
                {searchFlight?.nbPassenger &&
                  [...Array(searchFlight.nbPassenger)].map((item, index) => (
                    <Box key={index}>
                      <h3 className="titlePassenger">
                        {t("passenger.name")}
                        {index + 1}
                      </h3>
                      <Box sx={{ margin: 2 }}>
                        <Grid sx={{ marginY: 1 }}>
                          <TextField
                            name={`passengers[${index}].firstName`}
                            label={champFirstName}
                            value={passengers[index]?.firstName ?? ""}
                            onChange={(e) => handleInputChange(e, index)}
                            error={Boolean(getError(index)?.firstName)}
                            helperText={getError(index)?.firstName}
                          />
                        </Grid>
                        <Grid sx={{ marginY: 1 }}>
                          <TextField
                            name={`passengers[${index}].lastName`}
                            label={champLastName}
                            value={passengers[index]?.lastName ?? ""}
                            onChange={(e) => handleInputChange(e, index)}
                            error={Boolean(getError(index)?.lastName)}
                            helperText={getError(index)?.lastName}
                          />
                        </Grid>
                        <Grid sx={{ marginY: 1 }}>
                          <TextField
                            name={`passengers[${index}].email`}
                            label={champEmail}
                            value={passengers[index]?.email ?? ""}
                            onChange={(e) => handleInputChange(e, index)}
                            error={Boolean(getError(index)?.email)}
                            helperText={getError(index)?.email}
                          />
                        </Grid>
                        <Grid sx={{ marginY: 1 }}>
                          <TextField
                            name={`passengers[${index}].phoneNumber`}
                            label={champPhoneNumber}
                            value={passengers[index]?.phoneNumber ?? ""}
                            onChange={(e) => handleInputChange(e, index)}
                            error={Boolean(getError(index)?.phoneNumber)}
                            helperText={getError(index)?.phoneNumber}
                          />
                        </Grid>
                      </Box>
                    </Box>
                  ))}
              </DialogContent>
              <DialogActions>
                <Button type="submit" color="primary" onClick={handleClose}>
                  {t("passenger.cancel")}
                </Button>
                <Button
                  className="save"
                  type="submit"
                  color="primary"
                  onClick={handleAccept}
                >
                  {t("passenger.submit")}
                </Button>
              </DialogActions>
            </Box>
          </Dialog>
        )}
      </>
    </div>
  );
};

export default ResponsiveDialog;
