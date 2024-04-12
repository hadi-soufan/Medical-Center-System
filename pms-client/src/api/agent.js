import axios from "axios";

axios.defaults.baseURL = "http://localhost:5000/api";

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
  list: () => requests.get("/doctor/all-doctors"),

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
  delete: (id) => requests.del(`/doctor/${id}`),
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


const agent = {
  Doctors,
  Patients,
  Account,
  Appointments,
};

export default agent;
