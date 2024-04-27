import { createStore, applyMiddleware } from "redux";
import { thunk } from "redux-thunk";
import { combineReducers } from "redux";
import doctorsReducer from "./doctor/doctorsReducer";
import usersReducer  from "./user/usersReducer";
import appointmentReducer  from './appointment/appointmentReducer';
import patientReducer from "./patient/patientReducer";
import medicalHistoryReducer from "./medicalHistory/medicalHistoryReducer";

const rootReducer = combineReducers({
  doctors: doctorsReducer,
  patients: patientReducer,
  users: usersReducer,
  appointments: appointmentReducer,
  medicalHistories: medicalHistoryReducer,
});

/**
 * The Redux store for managing the application state.
 *
 * @type {Object}
 */
const store = createStore(rootReducer, applyMiddleware(thunk));

export default store;
