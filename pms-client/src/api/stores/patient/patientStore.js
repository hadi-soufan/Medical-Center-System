import agent from "../../agent.js";
import {GET_PATIENT_DETAILS, DELETE_PATIENT, SET_PATIENTS, SET_LOADING} from './patientActionsTypes.js';


/**
 * Sets the patients in the store.
 *
 * @param {Array} patients - The array of patients to set in the store.
 * @returns {Object} - The action object with type and payload.
 */
export const setPatients = (patients) => ({
  type: SET_PATIENTS,
  payload: patients,
});

/**
 * Sets the loading state of the application.
 *
 * @param {boolean} isLoading - The loading state to set.
 * @returns {object} - The action object with type and payload properties.
 */
export const setLoading = (isLoading) => ({
  type: SET_LOADING,
  payload: isLoading,
});

// Action creators
export const fetchPatients = () => async (dispatch) => {
  dispatch(setLoading(true));
  try {
    const response = await agent.Patients.list();
    const patients = response && response.$values ? response.$values : [];
    dispatch(setPatients(patients));
  } catch (error) {
    console.error("Error getting all patients: ", error);
  }
};

export const getPatientDetails = (id) => async (dispatch) => {
  try {
    const patient = await agent.Patients.get(id);
    dispatch({ type: GET_PATIENT_DETAILS, payload: patient });
  } catch (error) {
    console.error("Error getting patient details: ", error);
  }
};

export const deletePatient = (id) => async (dispatch) => {
  try {
    await agent.Patients.delete(id);
    dispatch({ type: DELETE_PATIENT, payload: id });
  } catch (error) {
    console.error("Error deleting patient: ", error);
  }
};

