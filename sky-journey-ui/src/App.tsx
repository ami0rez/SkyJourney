import React, { useEffect, useReducer } from "react";
import "./App.css";
import { Env, initialisationEnv } from "./config/environment";
import Router from "./Router";
// import { getCities } from "./services/cities";
// import { CityResponseIEnumerableMainResponse } from "./services/nswag";

const App: React.FC = () => {
  const [items, dispatch] = useReducer(() => false, true);
  
  async function isLoaded() {
    await initialisationEnv();
    dispatch();
  }
  useEffect(() => {
    if (!Env.apiEnv) {
      isLoaded();
    }
  }, [items]);

  return <Router />;
};

export default App;
