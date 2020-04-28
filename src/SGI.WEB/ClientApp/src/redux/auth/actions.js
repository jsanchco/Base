import {
  LOGIN_USER,
  LOGIN_USER_SUCCESS,
  LOGOUT_USER,
  REGISTER_USER,
  REGISTER_USER_SUCCESS,
  LOGIN_USER_ERROR,
  REGISTER_USER_ERROR,
  FORGOT_PASSWORD,
  FORGOT_PASSWORD_SUCCESS,
  FORGOT_PASSWORD_ERROR,
  RESET_PASSWORD,
  RESET_PASSWORD_SUCCESS,
  RESET_PASSWORD_ERROR
} from '../actions';
import { config, AUTHENTICATE } from "../../constants/defaultValues";
import store from "../../redux/store";

export function login(username, password, history) {
  store.dispatch(loginUser(username, history));
  const url = `${config.URL_API}/${AUTHENTICATE}`;
  fetch(url, {
    headers: {
      Accept: "text/plain",
      "Content-Type": "application/json"
    },
    method: "POST",
    body: JSON.stringify({ username, password })
  })
    .then(data => data.json())
    .then(result => {
      if (result.user != null && result.token != null) {
        localStorage.setItem("user", JSON.stringify(result.user));
        localStorage.setItem("jwt", result.token);
        store.dispatch(loginUserSuccess({ user: result.user }));
        history.push("/app");
      } else {
        if (result.message) {
          console.log("error ->", result.message);
          localStorage.removeItem("user");
          localStorage.removeItem("jwt");
          store.dispatch(loginUserError(result.message));
        }
      }
    })
    .catch(error => {
      console.log("error ->", error.toString());
      store.dispatch(loginUserError(error.toString()));
    });
}

export const loginUser = (user, history) => ({
  type: LOGIN_USER,
  payload: { user, history }
});
export const loginUserSuccess = (user) => ({
  type: LOGIN_USER_SUCCESS,
  payload: user
});
export const loginUserError = (message) => ({
  type: LOGIN_USER_ERROR,
  payload: { message }
});

export const forgotPassword = (forgotUserMail, history) => ({
  type: FORGOT_PASSWORD,
  payload: { forgotUserMail, history }
});
export const forgotPasswordSuccess = (forgotUserMail) => ({
  type: FORGOT_PASSWORD_SUCCESS,
  payload: forgotUserMail
});
export const forgotPasswordError = (message) => ({
  type: FORGOT_PASSWORD_ERROR,
  payload: { message }
});

export const resetPassword = ({ resetPasswordCode, newPassword, history }) => ({
  type: RESET_PASSWORD,
  payload: { resetPasswordCode, newPassword, history }
});
export const resetPasswordSuccess = (newPassword) => ({
  type: RESET_PASSWORD_SUCCESS,
  payload: newPassword
});
export const resetPasswordError = (message) => ({
  type: RESET_PASSWORD_ERROR,
  payload: { message }
});



export const registerUser = (user, history) => ({
  type: REGISTER_USER,
  payload: { user, history }
})
export const registerUserSuccess = (user) => ({
  type: REGISTER_USER_SUCCESS,
  payload: user
})
export const registerUserError = (message) => ({
  type: REGISTER_USER_ERROR,
  payload: { message }
})

export const logoutUser = (history) => ({
  type: LOGOUT_USER,
  payload: { history }
});