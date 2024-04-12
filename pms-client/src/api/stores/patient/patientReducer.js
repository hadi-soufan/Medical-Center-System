import {
  GET_PATIENT_DETAILS,
  DELETE_PATIENT,
  SET_PATIENTS,
  SET_LOADING,
} from "./patientActionsTypes.js";

// Reducer
const initialState = {
  patients: [],
  patientDetails: null,
};

export default function patientReducer(state = initialState, action) {
  switch (action.type) {
    case SET_PATIENTS:
      return { ...state, patients: action.payload, isLoading: false };

    case SET_LOADING:
      return { ...state, isLoading: action.payload };

    case GET_PATIENT_DETAILS:
      return { ...state, patientDetails: action.payload };
    case DELETE_PATIENT:
      return {
        ...state,
        patients: state.patients.filter(
          (patient) => patient.id !== action.payload
        ),
      };
    default:
      return state;
  }
}
