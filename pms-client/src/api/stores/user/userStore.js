import agent from "../../agent";
import toast from "react-hot-toast";
import * as userActions from './userActionsTypes';


const getCurrentUserRequest = () => ({ type: userActions.GET_CURRENT_USER_REQUEST });

const getCurrentUserSuccess = (user) => ({
  type: userActions.GET_CURRENT_USER_SUCCESS,
  payload: user,
});

const getCurrentUserFailure = (error) => ({
  type: userActions.GET_CURRENT_USER_FAILURE,
  payload: error,
});

const registerUserRequest = () => ({ type: userActions.REGISTER_USER_REQUEST });
const registerUserSuccess = (user) => ({
  type: userActions.REGISTER_USER_SUCCESS,
  payload: user,
});
const registerUserFailure = (error) => ({
  type: userActions.REGISTER_USER_FAILURE,
  payload: error,
});

export const logout = () => ({
  type: userActions.LOGOUT,
});

/**
 * Sets the loading state of the application.
 *
 * @param {boolean} isLoading - The loading state to set.
 * @returns {object} - The action object with type and payload properties.
 */
export const setLoading = (isLoading) => ({
  type: userActions.SET_LOADING,
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
      console.log("Login Error:", error);
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
  console.log("Registering user with details:", userDetails);
  dispatch(registerUserRequest());
  try {
    const user = await agent.Account.register(userDetails);

    dispatch(registerUserSuccess(user));
  } catch (error) {
    console.log("Registration error:", error.response.data.errors);
    dispatch(registerUserFailure(error.toString()));
    console.log(error);
  }
};

