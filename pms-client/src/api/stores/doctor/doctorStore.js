// src/store/doctorActions.js
import { SET_DOCTORS, SET_LOADING, UPDATE_DOCTOR, DELETE_DOCTOR, GET_DOCTOR_DETAILS } from './doctorActionsTypes';
import agent from "../../agent";
import toast from "react-hot-toast";

export const setDoctors = (doctors) => ({
  type: SET_DOCTORS,
  payload: doctors,
});

export const setLoading = (isLoading) => ({
  type: SET_LOADING,
  payload: isLoading,
});

export const fetchDoctors = () => async (dispatch) => {
  dispatch(setLoading(true));
  try {
    const response = await agent.Doctors.list();
    const doctors = response && response.$values ? response.$values : [];
    dispatch(setDoctors(doctors));
  } catch (error) {
    console.error("Error fetching doctors data: ", error);
    dispatch(setDoctors([]));
  } finally {
    dispatch(setLoading(false));
  }
};

export const deleteDoctor = (id) => async (dispatch) => {
  try {
    await agent.Doctors.delete(id);
    dispatch({ type: DELETE_DOCTOR, payload: id });
    toast.success("Doctor deleted successfully");
  } catch (error) {
    console.error("Error deleting doctor: ", error);
    toast.error("Failed to delete a doctor");
  }
};

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
    const doctor = await agent.Doctors.get(id);
    dispatch({ type: GET_DOCTOR_DETAILS, payload: doctor });
  } catch (error) {
    console.error("Error getting doctor details: ", error);
  }
};
