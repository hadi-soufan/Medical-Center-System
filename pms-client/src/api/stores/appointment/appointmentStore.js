import agent from "../../agent";
import toast from "react-hot-toast";
import * as appointmentActions from "./appointmentActionsTypes";

export const createAppointment = (appointmentData) => async (dispatch) => {
  dispatch({ type: appointmentActions.SET_LOADING, payload: true });
  try {
    const response = await agent.Appointments.create(appointmentData);
    dispatch({
      type: appointmentActions.CREATE_APPOINTMENT_SUCCESS,
      payload: response.data,
    });
    toast.success("Appointment created successfully!");
    dispatch({
      type: appointmentActions.SET_SUCCESS,
      payload: "Appointment created successfully!",
    });
    dispatch({ type: appointmentActions.CLEAR_ERROR });
  } catch (error) {
    console.error("Failed to create appointment:", error);
    dispatch({
      type: appointmentActions.CREATE_APPOINTMENT_FAIL,
      payload: error.response ? error.response.data : "Network error",
    });
    toast.error("Failed to create appointment!");
    dispatch({
      type: appointmentActions.SET_ERROR,
      payload: "Failed to create appointment!",
    });
  } finally {
    dispatch({ type: appointmentActions.SET_LOADING, payload: false });
  }
};

/**
 * Fetches all appointments from the server.
 * @returns {Function} An async function that dispatches actions to the Redux store.
 */
export const getAllAppointments = () => async (dispatch) => {
  dispatch({ type: appointmentActions.SET_LOADING, payload: true });
  try {
    const response = await agent.Appointments.list();
    dispatch({
      type: appointmentActions.GET_ALL_APPOINTMENTS_SUCCESS,
      payload: response.$values,
    });
    dispatch({ type: appointmentActions.CLEAR_ERROR });
  } catch (error) {
    console.error("Error fetching appointments:", error);
    dispatch({
      type: appointmentActions.GET_ALL_APPOINTMENTS_FAIL,
      payload: error.response ? error.response.data : "Network error",
    });
    dispatch({
      type: appointmentActions.SET_ERROR,
      payload: "Failed to fetch appointments",
    });
    toast.error("Failed to fetch appointments");
  } finally {
    dispatch({ type: appointmentActions.SET_LOADING, payload: false });
  }
};

export const updateAppointment = (id, updatedData) => async (dispatch) => {
  dispatch({ type: appointmentActions.SET_LOADING, payload: true });
  try {
    const response = await agent.Appointments.update(id, updatedData);
    dispatch({
      type: appointmentActions.UPDATE_APPOINTMENT_SUCCESS,
      payload: { id, ...response.data },
    });
    toast.success("Appointment updated successfully!");
    dispatch({
      type: appointmentActions.SET_SUCCESS,
      payload: "Appointment updated successfully!",
    });
    dispatch({ type: appointmentActions.CLEAR_ERROR });
  } catch (error) {
    console.error("Failed to update appointment:", error);
    dispatch({
      type: appointmentActions.UPDATE_APPOINTMENT_FAIL,
      payload: error.response ? error.response.data : "Network error",
    });
    toast.error("Failed to update appointment!");
    dispatch({
      type: appointmentActions.SET_ERROR,
      payload: "Failed to update appointment!",
    });
  } finally {
    dispatch({ type: appointmentActions.SET_LOADING, payload: false });
  }
};

export const deleteAppointment = (id) => async (dispatch) => {
  dispatch({ type: appointmentActions.SET_LOADING, payload: true });
  try {
    console.log('Dispatching delete for appointment:', id);
    await agent.Appointments.delete(id);
    dispatch({
      type: appointmentActions.DELETE_APPOINTMENT_SUCCESS,
      payload: id,
    });
    toast.success("Appointment deleted successfully!");
    dispatch({ type: appointmentActions.CLEAR_ERROR });
  } catch (error) {
    console.error("Failed to delete appointment:", error);
    console.error("Appointment ID:", id);
    dispatch({
      type: appointmentActions.DELETE_APPOINTMENT_FAIL,
      payload: error.response ? error.response.data : "Network error",
    });
    toast.error("Failed to delete appointment!");
    dispatch({
      type: appointmentActions.SET_ERROR,
      payload: "Failed to delete appointment!",
    });
  } finally {
    dispatch({ type: appointmentActions.SET_LOADING, payload: false });
  }
};
