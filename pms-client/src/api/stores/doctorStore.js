import axios from "axios";
import agent from "../agent";
import toast from "react-hot-toast";

const SET_DOCTORS = "SET_DOCTORS";
const SET_LOADING = "SET_LOADING";
const UPDATE_DOCTOR = "UPDATE_DOCTOR";
const DELETE_DOCTOR = "DELETE_DOCTOR";
const GET_DOCTOR_DETAILS = "GET_DOCTOR_DETAILS";

/**
 * Sets the doctors in the store.
 *
 * @param {Array} doctors - The array of doctors to set in the store.
 * @returns {Object} - The action object with type and payload.
 */
export const setDoctors = (doctors) => ({
  type: SET_DOCTORS,
  payload: doctors,
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

/**
 * Fetches doctors data from the server.
 * @returns {Function} An async function that dispatches actions to update the store.
 */
export const fetchDoctors = () => async (dispatch) => {
  dispatch(setLoading(true));
  try {
    const response = await agent.Doctors.list();
    const doctors = response && response.$values ? response.$values : [];
    dispatch(setDoctors(doctors));
  } catch (error) {
    console.log("Error fetching doctors data: ", error);
    dispatch(setDoctors([]));
  } finally {
    dispatch(setLoading(false));
  }
};

/**
 * Deletes a doctor with the specified ID.
 * @param {string} id - The ID of the doctor to delete.
 * @returns {Promise<void>} - A promise that resolves when the doctor is deleted.
 */
export const deleteDoctor = (id) => async (dispatch) => {
  try {
    await axios.delete(`http://localhost:5000/api/doctor/${id}`);
    dispatch({ type: DELETE_DOCTOR, payload: id });
    toast.success("Doctor deleted successfully");
  } catch (error) {
    console.log("Error deleting doctor: ", error);
    toast.error("Failed to delete a doctor");
  }
};

/**
 * Updates a doctor in the system.
 * @param {Object} updatedDoctor - The updated doctor object.
 * @returns {Function} - An async function that dispatches an action to update the doctor.
 */
export const updateDoctor = (updatedDoctor) => async (dispatch) => {
  if (!updatedDoctor || !updatedDoctor.doctorId) {
    console.error("Invalid doctor object: ", updatedDoctor);
    return;
  }

  try {
    await agent.Doctors.update(updatedDoctor.doctorId, updatedDoctor);
    dispatch({ type: UPDATE_DOCTOR, payload: updatedDoctor });
    toast.success("Doctor updated successfully");
  } catch (error) {
    console.error("Error updating doctor: ", error);
    toast.error("Failed to update doctor");
  }
};

export const getDoctorDetails = (id) => async (dispatch) => {
  try {
    const doctor = await axios.get(`http://localhost:5000/api/doctor/${id}`);
    dispatch({ type: GET_DOCTOR_DETAILS, payload: doctor });
  } catch (error) {
    console.log("Error getting doctor details: ", error);
  }
};

const initialState = {
  doctors: [],
  isLoading: true,
};

/**
 * Reducer function for managing the state of doctors in the application.
 *
 * @param {Object} state - The current state of the doctors.
 * @param {Object} action - The action object that contains the type and payload.
 * @returns {Object} - The new state of the doctors.
 */
export const doctorsReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_DOCTORS:
      return { ...state, doctors: action.payload, isLoading: false };
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
      return {
        ...state,
        doctorDetails: action.payload,
      };
    default:
      return state;
  }
};
