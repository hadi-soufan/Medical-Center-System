import { createStore, applyMiddleware } from 'redux';
import {thunk} from 'redux-thunk';
import { doctorsReducer } from './doctorStore';

/**
 * The Redux store for managing the application state.
 *
 * @type {Object}
 */
const store = createStore(doctorsReducer, applyMiddleware(thunk));

export default store;