import i18n from "i18next";
import { initReactI18next } from "react-i18next";

import LanguageDetector from "i18next-browser-languagedetector";

import translationFr from "./assets/i18n/fr.json";
import translationEn from "./assets/i18n/en.json";

const resources = {
  en: {
    translation: translationEn,
  },
  fr: {
    translation: translationFr,
  },
};

i18n
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    resources,
    lng: "en",
    interpolation: {
      escapeValue: false,
    },
    react:{
      useSuspense: false
    }
  });

export default i18n;
