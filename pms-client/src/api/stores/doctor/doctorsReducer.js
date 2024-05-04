import {
  SET_DOCTORS,
  SET_LOADING,
  UPDATE_DOCTOR,
  DELETE_DOCTOR,
  GET_DOCTOR_DETAILS,
} from "./doctorActionsTypes";

const initialState = {
  doctors: [],
  isLoading: true,
  doctorDetails: null,
};

export const doctorsReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_DOCTORS:
      return {
        ...state,
        doctors: action.payload.doctors,
        pagination: action.payload.pagination,
        isLoading: false,
      };
    case SET_LOADING:
      return { ...state, isLoading: action.payload };
    case UPDATE_DOCTOR:
      return {
        ...state,
        doctors: state.doctors.map((doctor) =>
          doctor.doctorId === action.payload.doctorId ? action.payload : doctor
        ),
      };
    case DELETE_DOCTOR:
      return {
        ...state,
        doctors: state.doctors.filter(
          (doctor) => doctor.doctorId !== action.payload
        ),
      };
    case GET_DOCTOR_DETAILS:
      return { ...state, doctorDetails: action.payload };
    default:
      return state;
  }
};

export default doctorsReducer;
