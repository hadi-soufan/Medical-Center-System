import agent from "../../agent";
import toast from "react-hot-toast";
import * as userActions from './userActionsTypes';




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

const loadUserFromLocalStorage = () => {
  const user = JSON.parse(localStorage.getItem('user'));
  return {
    type: userActions.LOAD_USER_FROM_LOCAL_STORAGE,
    payload: user,
  };
};

export const loadUser = () => (dispatch) => {
  dispatch(loadUserFromLocalStorage());
};

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



export const registerUser = (userDetails) => async (dispatch) => {
  console.log("Registering user with details:", userDetails);
  dispatch(registerUserRequest());
  try {
    const user = await agent.Account.register(userDetails);
    dispatch(registerUserSuccess(user));
    toast.success("User registered successfully!");
  } catch (error) {
    const errorMessage = error.response?.data ?? error.message;
    dispatch(registerUserFailure(errorMessage));
    toast.error(`${errorMessage}`);
  }
};

