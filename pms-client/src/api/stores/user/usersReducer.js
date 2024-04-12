import * as userActions from './userActionsTypes';

const initialState = {
    user: null,
    isLoading: false,
  };
  
  // Define the reducer function
  export default function usersReducer(state = initialState, action) {
    switch (action.type) {
      case userActions.LOGIN_REQUEST:
        return { ...state, isLoading: true, error: null };
      case userActions.LOGIN_SUCCESS:
        return { ...state, user: action.payload, isLoading: false, error: null };
      case userActions.LOGIN_FAILURE:
        return { ...state, isLoading: false, error: action.payload };
      case userActions.GET_CURRENT_USER_REQUEST:
        return { ...state, isLoading: true };
      case userActions.GET_CURRENT_USER_SUCCESS:
        return { ...state, user: action.payload, isLoading: false };
      case userActions.GET_CURRENT_USER_FAILURE:
        return { ...state, error: action.payload, isLoading: false };
      case userActions.REGISTER_USER_REQUEST:
        return { ...state, isLoading: true };
      case userActions.REGISTER_USER_SUCCESS:
        return { ...state, user: action.payload, isLoading: false };
      case userActions.REGISTER_USER_FAILURE:
        return { ...state, error: action.payload, isLoading: false };
      case userActions.LOGOUT:
        return { ...state, user: null };
      default:
        return state;
    }
  };
  