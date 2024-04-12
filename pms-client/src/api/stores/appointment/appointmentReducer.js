import * as appointmentActions from "./appointmentActionsTypes";

// Initial state
const initialState = {
  appointments: [],
  appointmentDetails: null,
  loading: false,
  error: null,
  success: null,
};

// Reducer
export default function appointmentReducer(state = initialState, action) {
  switch (action.type) {
    case appointmentActions.GET_ALL_APPOINTMENTS_SUCCESS:
      return {
        ...state,
        appointments: action.payload || [],
        loading: false,
        error: null,
      };

    case appointmentActions.GET_ALL_APPOINTMENTS_FAIL:
      return {
        ...state,
        loading: false,
        error: action.payload,
      };
    case appointmentActions.CREATE_APPOINTMENT_SUCCESS:
      return {
        ...state,
        appointments: [...state.appointments, action.payload],
        loading: false,
        success: "Appointment created successfully!",
        error: null,
      };
    case appointmentActions.CREATE_APPOINTMENT_FAIL:
      return {
        ...state,
        loading: false,
        error: action.payload,
      };
    case appointmentActions.UPDATE_APPOINTMENT_SUCCESS:
      return {
        ...state,
        appointments: state.appointments.map((appointment) =>
          appointment.id === action.payload.id
            ? { ...appointment, ...action.payload }
            : appointment
        ),
        loading: false,
        error: null,
        success: "Appointment updated successfully!",
      };

    case appointmentActions.UPDATE_APPOINTMENT_FAIL:
      return {
        ...state,
        loading: false,
        error: action.payload,
        success: null,
      };
    case appointmentActions.DELETE_APPOINTMENT_SUCCESS:
      return {
        ...state,
        appointments: state.appointments.filter(
          (appointment) => appointment.id !== action.payload
        ),
        loading: false,
      };

    case appointmentActions.DELETE_APPOINTMENT_FAIL:
      return {
        ...state,
        loading: false,
        error: action.payload,
      };
    case appointmentActions.SET_LOADING:
      return {
        ...state,
        loading: action.payload,
      };
    case appointmentActions.SET_ERROR:
      return {
        ...state,
        error: action.payload,
        loading: false,
      };
    case appointmentActions.CLEAR_ERROR:
      return {
        ...state,
        error: null,
      };
    case appointmentActions.SET_SUCCESS:
      return {
        ...state,
        success: action.payload,
      };
    case appointmentActions.CLEAR_SUCCESS:
      return {
        ...state,
        success: null,
      };
    default:
      return state;
  }
}
