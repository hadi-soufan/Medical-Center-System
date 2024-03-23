import axios from 'axios';

axios.defaults.baseURL = 'http://localhost:5000/api';

const responseBody = (response) => response.data;

const requests = {
    get: (url) => axios.get(url).then(responseBody),
    post: (url, body) => axios.post(url, body).then(responseBody),
    put: (url, body) => axios.put(url, body).then(responseBody),
    del: (url) => axios.del(url).then(responseBody),
}

const Doctors = {
    list: () => requests.get('/doctor/all-doctors')
}

const agent = {
    Doctors
}

export default agent;