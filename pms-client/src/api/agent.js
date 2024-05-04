import axios from "axios";
import { PaginatedResult } from "../utils/pagination";

axios.defaults.baseURL = "http://localhost:5000/api";

axios.interceptors.response.use(
  response => {
    const paginationHeader = response.headers['pagination'];
    if (paginationHeader) {
      const pagination = JSON.parse(paginationHeader);
      response.data = new PaginatedResult(response.data, pagination);
    }
    return response;
  },
  error => {
    return Promise.reject(error);
  }
);




const responseBody = (response) => response.data;

/**
 * Object containing various HTTP request methods.
 * @typedef {Object} Requests
 * @property {function} get - Function to make a GET request.
 * @property {function} post - Function to make a POST request.
 * @property {function} put - Function to make a PUT request.
 * @property {function} del - Function to make a DELETE request.
 */

/**
 * Object containing various HTTP request methods.
 * @type {Requests}
 */
const requests = {
  get: (url) => axios.get(url).then(responseBody),
  post: (url, body) => axios.post(url, body).then(responseBody),
  put: (url, body) => axios.put(url, body).then(responseBody),
  del: (url) => axios.del(url).then(responseBody),
};

/**
 * Object representing doctors.
 * @namespace Doctors
 */
const Doctors = {
  /**
   * Get a list of all doctors.
   * @memberof Doctors
   * @function list
   * @returns {Promise} A promise that resolves to the list of doctors.
   */
  list: (params) => requests.get(`/doctor/all-doctors`, {params}).then(responseBody),

  get: (id) => requests.get(`/doctor/${id}`),
  /**
   * Update a doctor.
   * @memberof Doctors
   * @function update
   * @param {string} id - The ID of the doctor to update.
   * @param {Object} data - The updated data for the doctor.
   * @returns {Promise} A promise that resolves when the doctor is updated.
   */
  update: (id, data) => requests.put(`/doctor/${id}`, data),

  /**
   * Delete a doctor.
   * @memberof Doctors
   * @function delete
   * @param {string} id - The ID of the doctor to delete.
   * @returns {Promise} A promise that resolves when the doctor is deleted.
   */
  delete: (id) => axios.delete(`/doctor/${id}`),
};

/**
 * Object representing patients.
 * @namespace Patients
 */
const Patients = {
  /**
   * Get a list of all patients.
   * @memberof Patients
   * @function list
   * @returns {Promise} A promise that resolves to the list of patients.
   */
  list: () => requests.get("/patient/all-patients"),

  /**
   * Get a single patient.
   * @memberof Patients
   * @function get
   * @param {string} id - The ID of the patient to get.
   * @returns {Promise} A promise that resolves to the patient.
   */
  get: (id) => requests.get(`/patient/${id}`),

  /**
   * Delete a patient.
   * @memberof Patients
   * @function delete
   * @param {string} id - The ID of the patient to delete.
   * @returns {Promise} A promise that resolves when the patient is deleted.
   */
  delete: (id) => requests.del(`/patient/${id}`),
};

/**
 * Object containing methods for interacting with 'Account' API.
 */
const Account = {
  /**
   * Login with username and password.
   * @memberof Account
   * @function login
   * @param {string} username - The username.
   * @param {string} password - The password.
   * @returns {Promise} A promise that resolves when the login is successful.
   */
  login: (username, password) =>
    requests.post("/account/login", { username, password }),

  /**
   * Register a new user.
   * @memberof Account
   * @function register
   * @param {Object} userDetails - The user details for registration.
   * @returns {Promise} A promise that resolves when the registration is successful.
   */
  register: (userDetails) => requests.post("/account/register", userDetails),

  current: () => requests.get("/account"),
};

const Appointments = {
  list: () => requests.get("/appointments/all-appointments"),
  get: (id) => requests.get(`/appointments/${id}`),
  create: (data) => requests.post('/appointments/create-new-appointment', data),
  update: (id, data) => requests.put(`/appointments/${id}`, data),
  delete: (id) => axios.delete(`/appointments/${id}`),
};

const MedicalHistory = {
  /**
   * Get a list of all medical histories.
   * @memberof MedicalHistory
   * @function list
   * @returns {Promise} A promise that resolves to the list of medical histories.
   */
  list: () => requests.get("/medicalhistory/all-medical-histories"),

  /**
   * Get a single medical history.
   * @memberof MedicalHistory
   * @function get
   * @param {string} id - The ID of the medical history to get.
   * @returns {Promise} A promise that resolves to the medical history.
   */
  get: (id) => requests.get(`/medicalhistory/${id}`),

  /**
   * Create a new medical history.
   * @memberof MedicalHistory
   * @function create
   * @param {Object} data - The data for the new medical history.
   * @returns {Promise} A promise that resolves when the medical history is created.
   */
  create: (data) => requests.post('/medicalhistory/create-new-medical-history', data),

  /**
   * Update a medical history.
   * @memberof MedicalHistory
   * @function update
   * @param {string} id - The ID of the medical history to update.
   * @param {Object} data - The updated data for the medical history.
   * @returns {Promise} A promise that resolves when the medical history is updated.
   */
  update: (id, data) => requests.put(`/medicalhistory/${id}`, data),

  /**
   * Delete a medical history.
   * @memberof MedicalHistory
   * @function delete
   * @param {string} id - The ID of the medical history to delete.
   * @returns {Promise} A promise that resolves when the medical history is deleted.
   */
  delete: (id) => axios.delete(`/medicalhistory/${id}`),
};

const PatientPhotos = {
  list: () => requests.get("/photo"),
  get: (id) => requests.get(`/photo/${id}`),
  create: (data) => requests.post('/photo', data),
  delete: (id) => axios.delete(`/photo/${id}`),
}

const agent = {
  Doctors,
  Patients,
  Account,
  Appointments,
  MedicalHistory,
  PatientPhotos,
};

export default agent;
