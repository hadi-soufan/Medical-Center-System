import * as doctorActions from './doctorActionsTypes';
import agent from "../../agent";
import toast from "react-hot-toast";
import axios from 'axios';

export const setDoctors = (doctors) => ({
  type: doctorActions.SET_DOCTORS,
  payload: { doctors },
});

export const setLoading = (isLoading) => ({
  type: doctorActions.SET_LOADING,
  payload: isLoading,
});



export const fetchDoctors = (page = 1, pageSize = 10) => async (dispatch) => {
  dispatch(setLoading(true));
  try {
    const params = new URLSearchParams({ pageNumber: page.toString(), pageSize: pageSize.toString() });
    const response = await axios.get(`http://localhost:5000/api/doctor/all-doctors`, { params });
    const doctors = response && response.data && response.data.$values ? response.data.$values : [];
    dispatch(setDoctors(doctors));
  } catch (error) {
    dispatch(setDoctors([]));
  } finally {
    dispatch(setLoading(false));
  }
};




export const deleteDoctor = (id) => async (dispatch) => {
  try {
    await agent.Doctors.delete(id);
    dispatch({ type: doctorActions.DELETE_DOCTOR, payload: id });
    toast.success("Doctor deleted successfully");
  } catch (error) {
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
    dispatch({ type: doctorActions.UPDATE_DOCTOR, payload: updatedDoctor });
    toast.success("Doctor updated successfully");
  } catch (error) {
    console.error("Error updating doctor: ", error);
    toast.error("Failed to update doctor");
  }
};

export const getDoctorDetails = (id) => async (dispatch) => {
  try {
    const doctor = await agent.Doctors.get(id);
    dispatch({ type: doctorActions.GET_DOCTOR_DETAILS, payload: doctor });
  } catch (error) {
    console.error("Error getting doctor details: ", error);
  }
};
