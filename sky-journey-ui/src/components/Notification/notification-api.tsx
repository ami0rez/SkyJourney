import { useContext } from "react";
import { MyContext } from "../../context";
import { AlertColor } from "@mui/material";

const useNotification = () => {
  const { handleAlert } = useContext(MyContext);

  const showMessage = (message: string, severity: AlertColor) => {
    if (handleAlert) {
      handleAlert({ open: true, message, severity });
    }
  };

  const showError = (message: string) => {
    showMessage(message, "error");
  };

  const showSuccess = (message: string) => {
    showMessage(message, "success");
  };

  return {
    showMessage,
    showError,
    showSuccess,
  };
};

export default useNotification;
