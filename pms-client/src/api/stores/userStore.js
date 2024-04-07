import agent from "../agent";
import toast from "react-hot-toast";

const SET_LOADING = "SET_LOADING";
const LOGIN_REQUEST = "LOGIN_REQUEST";
const LOGIN_SUCCESS = "LOGIN_SUCCESS";
const LOGIN_FAILURE = "LOGIN_FAILURE";
const GET_CURRENT_USER_REQUEST = "GET_CURRENT_USER_REQUEST";
const GET_CURRENT_USER_SUCCESS = "GET_CURRENT_USER_SUCCESS";
const GET_CURRENT_USER_FAILURE = "GET_CURRENT_USER_FAILURE";
const REGISTER_USER_REQUEST = "REGISTER_USER_REQUEST";
const REGISTER_USER_SUCCESS = "REGISTER_USER_SUCCESS";
const REGISTER_USER_FAILURE = "REGISTER_USER_FAILURE";
const LOGOUT = "LOGOUT";

const getCurrentUserRequest = () => ({ type: GET_CURRENT_USER_REQUEST });

const getCurrentUserSuccess = (user) => ({
  type: GET_CURRENT_USER_SUCCESS,
  payload: user,
});

const getCurrentUserFailure = (error) => ({
  type: GET_CURRENT_USER_FAILURE,
  payload: error,
});

const registerUserRequest = () => ({ type: REGISTER_USER_REQUEST });
const registerUserSuccess = (user) => ({
  type: REGISTER_USER_SUCCESS,
  payload: user,
});
const registerUserFailure = (error) => ({
  type: REGISTER_USER_FAILURE,
  payload: error,
});

export const logout = () => ({
  type: LOGOUT,
});

/**
 * Sets the loading state of the application.
 *
 * @param {boolean} isLoading - The loading state to set.
 * @returns {object} - The action object with type and payload properties.
 */
export const setLoading = (isLoading) => ({
  type: SET_LOADING,
  payload: isLoading,
});

// Define the login action creator
export const loginAction = (username, password) => {
  return async (dispatch) => {
    try {
      dispatch({ type: "LOGIN_REQUEST" });

      const user = await agent.Account.login(username, password);
      dispatch({ type: "LOGIN_SUCCESS", payload: user });
      localStorage.setItem("user", JSON.stringify(user));
      toast.success(`Welcome back, ${user.username}!`);
      console.log("userloggedin", user);
    } catch (error) {
      console.log("Error:", error);
      dispatch({ type: "LOGIN_FAILURE", payload: error.message });
      toast.error("Bad Credentials! Please try again.");
    }
  };
};

export const getCurrentUser = () => async (dispatch) => {
  dispatch(getCurrentUserRequest());
  try {
    const user = await agent.Account.current();

    dispatch(getCurrentUserSuccess(user));

    console.log("currentUser", user.username);
  } catch (error) {
    dispatch(getCurrentUserFailure(error.toString()));
  }
};

export const registerUser = (userDetails) => async (dispatch) => {
  dispatch(registerUserRequest());
  try {
    const user = await agent.Account.register(userDetails);

    dispatch(registerUserSuccess(user));
  } catch (error) {
    dispatch(registerUserFailure(error.toString()));
    console.log(error);
  }
};

const initialState = {
  user: null,
  isLoading: false,
};

// Define the reducer function
export const usersReducer = (state = initialState, action) => {
  switch (action.type) {
    case LOGIN_REQUEST:
      return { ...state, isLoading: true, error: null };
    case LOGIN_SUCCESS:
      return { ...state, user: action.payload, isLoading: false, error: null };
    case LOGIN_FAILURE:
      return { ...state, isLoading: false, error: action.payload };
    case GET_CURRENT_USER_REQUEST:
      return { ...state, isLoading: true };
    case GET_CURRENT_USER_SUCCESS:
      return { ...state, user: action.payload, isLoading: false };
    case GET_CURRENT_USER_FAILURE:
      return { ...state, error: action.payload, isLoading: false };
    case REGISTER_USER_REQUEST:
      return { ...state, isLoading: true };
    case REGISTER_USER_SUCCESS:
      return { ...state, user: action.payload, isLoading: false };
    case REGISTER_USER_FAILURE:
      return { ...state, error: action.payload, isLoading: false };
    case LOGOUT:
      return { ...state, user: null };
    default:
      return state;
  }
};
