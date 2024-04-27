import * as medicalHistoryActions from "./medicalHistoryActionsTypes";
import agent from "../../agent";
import toast from "react-hot-toast";
import axios from "axios";

export const setMedicalHistories = (medicalHistory) => ({
  type: medicalHistoryActions.SET_MEDICAL_HISTORIES,
  payload: medicalHistory,
});

export const setLoading = (isLoading) => ({
  type: medicalHistoryActions.SET_LOADING,
  payload: isLoading,
});

export const getMedicalHistorySuccess = (medicalHistory) => ({
  type: medicalHistoryActions.GET_MEDICAL_HISTORY_SUCCESS,
  payload: medicalHistory,
});

export const getMedicalHistoryFailure = (error) => ({
  type: medicalHistoryActions.GET_MEDICAL_HISTORY_FAILURE,
  payload: error,
});

export const deleteMedicalHistoryRequest = () => ({
  type: medicalHistoryActions.DELETE_MEDICAL_HISTORY_REQUEST,
});

export const deleteMedicalHistorySuccess = (id) => ({
  type: medicalHistoryActions.DELETE_MEDICAL_HISTORY_SUCCESS,
  payload: id,
});

export const deleteMedicalHistoryFailure = (error) => ({
  type: medicalHistoryActions.DELETE_MEDICAL_HISTORY_FAILURE,
  payload: error,
});

export const getMedicalHistoryRequest = () => ({
  type: medicalHistoryActions.GET_MEDICAL_HISTORY_REQUEST,
});

export const createMedicalHistoryRequest = () => ({
  type: medicalHistoryActions.CREATE_MEDICAL_HISTORY_REQUEST,
});

export const createMedicalHistorySuccess = (medicalHistory) => ({
  type: medicalHistoryActions.CREATE_MEDICAL_HISTORY_SUCCESS,
  payload: medicalHistory,
});

export const createMedicalHistoryFailure = (error) => ({
  type: medicalHistoryActions.CREATE_MEDICAL_HISTORY_FAILURE,
  payload: error,
});

export const updateMedicalHistoryRequest = () => ({
  type: medicalHistoryActions.UPDATE_MEDICAL_HISTORY_REQUEST,
});

export const updateMedicalHistorySuccess = (medicalHistory) => ({
  type: medicalHistoryActions.UPDATE_MEDICAL_HISTORY_SUCCESS,
  payload: medicalHistory,
});

export const updateMedicalHistoryFailure = (error) => ({
  type: medicalHistoryActions.UPDATE_MEDICAL_HISTORY_FAILURE,
  payload: error,
});


export const createMedicalHistory = (medicalHistoryData) => async (dispatch) => {
    dispatch(createMedicalHistoryRequest());
    try {
      const response = await axios.post('http://localhost:5000/api/medicalhistory/create-new-medical-history', medicalHistoryData);
      console.log("response: ", response.data );
      dispatch(createMedicalHistorySuccess(response.data));
      console.log("medicalHistoryData: ", medicalHistoryData );
      toast.success("Medical History created successfully");
    } catch (error) {
      console.log("medicalHistoryData: ", medicalHistoryData );
      console.error("Error creating medical history: ", error);
      toast.error("Failed to create medical history");
      dispatch(createMedicalHistoryFailure(error));
    }
  };

export const getMedicalHistories = () => async (dispatch) => {
  dispatch({ type: medicalHistoryActions.GET_MEDICAL_HISTORIES_REQUEST });
  try {
    const response = await agent.MedicalHistory.list();
    const medicalHistories =
      response && response.$values ? response.$values : [];
    dispatch(setMedicalHistories(medicalHistories));
  } catch (error) {
    console.error("Error fetching Medical Histories data: ", error);
    dispatch(setMedicalHistories([]));
  } finally {
    dispatch(setLoading(false));
  }
};

export const getMedicalHistoryDetails = (id) => async (dispatch) => {
  dispatch(setLoading(true));
  dispatch(getMedicalHistoryRequest());
  try {
    const response = await axios.get(`/medicalhistory/${id}`);
    dispatch(getMedicalHistorySuccess(response.data));
  } catch (error) {
    console.error("Error fetching Single Medical History data: ", error);
    dispatch(getMedicalHistoryFailure(error));
  } finally {
    dispatch(setLoading(false));
  }
};

export const updateMedicalHistory =
  (id, medicalHistoryData) => async (dispatch) => {
    dispatch(updateMedicalHistoryRequest());
    try {
      const response = await agent.MedicalHistory.update(id, medicalHistoryData);
      dispatch(updateMedicalHistorySuccess(response.data));
      toast.success("Medical History updated successfully");
    } catch (error) {
      console.error("Error updating medical history: ", error);
      toast.error("Failed to update medical history");
      dispatch(updateMedicalHistoryFailure(error));
    }
  };

export const deleteMedicalHistory = (id) => async (dispatch) => {
  console.log("deleteMedicalHistory action called");
  dispatch(deleteMedicalHistoryRequest());
  try {
    await agent.MedicalHistory.delete(id);
    dispatch(deleteMedicalHistorySuccess(id));
    toast.success("Medical History deleted successfully");
  } catch (error) {
    console.error("Error deleting medical history: ", error);
    toast.error("Failed to delete medical history");
    dispatch(deleteMedicalHistoryFailure(error));
  }
};
