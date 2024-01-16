import { AlertColor } from "@mui/material";

export interface Alert{
  severity?: AlertColor;
  message?: string;
  open?: boolean;
}