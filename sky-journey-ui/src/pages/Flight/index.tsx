import React, { useEffect } from "react";
import FlightComponent from "../../features/FlightReservation/Flight/Flight";
import Navbar from "../../components/NavBar";
import ListFlights from "../../features/FlightReservation/ListFlights";
import ResponsiveDialog from "../../components/Dialogue";
import Notification from "../../components/Notification";

const Flight = () => {
  useEffect(() => {
    
  }, []);
  return(
    <>
    <Notification/>
    <Navbar />
    <FlightComponent />
    <ListFlights />
    <ResponsiveDialog />
    </>
  );
};

export default Flight;
