import { createStore, applyMiddleware } from 'redux';
import {thunk}  from 'redux-thunk';
import { combineReducers } from 'redux';
import { doctorsReducer } from './doctorStore';
import { usersReducer } from './userStore';
import patientReducer from './patientStore';

const rootReducer = combineReducers({
  doctors: doctorsReducer,
  patients: patientReducer,
  users: usersReducer,
});

/**
 * The Redux store for managing the application state.
 *
 * @type {Object}
 */
const store = createStore(rootReducer, applyMiddleware(thunk));

export default store;