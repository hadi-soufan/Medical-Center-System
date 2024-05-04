import * as patientPhotosActions from "./patientPhotoActionTypes";
import agent from "../../agent";
import toast from "react-hot-toast";
import axios from "axios";

export const getPatientPhotos = () => async (dispatch) => {
    try {
      dispatch({ type: patientPhotosActions.SET_LOADING });
      const response = await agent.PatientPhotos.list('/photo');
      dispatch({ type: patientPhotosActions.SET_PATIENT_PHOTOS, payload: response.data });
      console.log("photos: ", response);
    } catch (error) {
      console.error('Error getting patient photos:', error);
    }
  };
  