import * as doctorActions from './doctorActionsTypes';
import agent from "../../agent";
import toast from "react-hot-toast";

import { Pagination, PagingParams, PaginatedResult } from '../../../utils/pagination'
export const setDoctors = (doctors, pagination) => ({
  type: doctorActions.SET_DOCTORS,
  payload: { doctors, pagination },
});

export const setLoading = (isLoading) => ({
  type: doctorActions.SET_LOADING,
  payload: isLoading,
});


const axiosParams = (pagingParams) => {
  const params = new URLSearchParams();
  params.append('pageNumber', pagingParams.pageNumber.toString());
  params.append('pageSize', pagingParams.pageSize.toString());
  return params;
};

export const fetchDoctors = (page = 1, pageSize = 2) => async (dispatch) => {
  dispatch(setLoading(true));
  try {
    const response = await agent.Doctors.list(page, pageSize);
    console.log(response);
    const doctors = response && response.data && response.data.$values ? response.data.$values : [];
    console.log("docotrs:" , doctors);
    const pagination = new Pagination(
      response.pagination.currentPage,
      response.pagination.itemsPerPage,
      response.pagination.totalItems,
      response.pagination.totalPages
    );
    console.log("pagination: ", pagination);
    const paginatedResult = new PaginatedResult(doctors, pagination);
    dispatch(setDoctors(doctors, pagination));
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
    dispatch({ type: doctorActions.DELETE_DOCTOR, payload: id });
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
