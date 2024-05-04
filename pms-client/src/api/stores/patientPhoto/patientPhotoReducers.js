import * as patientPhotosActions from "./patientPhotoActionTypes";

// Define initial state
const initialState = {
  patientPhotos: [],
  loading: false,
};

const patientPhotoReducers = (state = initialState, action) => {
  switch (action.type) {
    case patientPhotosActions.SET_PATIENT_PHOTOS:
      return {
        ...state,
        patientPhotos: action.payload,
        loading: false,
      };
    case patientPhotosActions.SET_LOADING:
      return {
        ...state,
        loading: true,
      };
    default:
      return state;
  }
};

export default patientPhotoReducers;
