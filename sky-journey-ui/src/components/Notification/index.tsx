import * as React from "react";
import Snackbar from "@mui/material/Snackbar";
import { Alert } from "@mui/material";
import { useContext } from "react";
import { MyContext } from "../../context";
import useNotification from "./notification-api";

export default function Notification() {
  const vertical = "top";
  const horizontal = "right";

  const { alert, handleAlert } = useContext(MyContext);
  const handleClose = (
    event: React.SyntheticEvent | Event,
    reason?: string
  ) => {
    if (reason === "clickaway") {
      return;
    }
    if (handleAlert != null) {
      handleAlert({ open: false });
    }
  };

  return (
    <div>
      <Snackbar
        anchorOrigin={{ vertical, horizontal }}
        open={alert?.open}
        autoHideDuration={1500}
        onClose={handleClose}
      >
        <Alert
          onClose={handleClose}
          severity={alert?.severity}
          sx={{ width: "100%" }}
        >
          {alert?.message}
        </Alert>
      </Snackbar>
    </div>
  );
}

export { useNotification };