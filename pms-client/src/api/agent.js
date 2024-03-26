import axios from 'axios';

axios.defaults.baseURL = 'http://localhost:5000/api';

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
}

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
    list: () => requests.get('/doctor/all-doctors'),

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
    delete: (id) => requests.del(`/doctor/${id}`)
}

const agent = {
    Doctors
}

export default agent;