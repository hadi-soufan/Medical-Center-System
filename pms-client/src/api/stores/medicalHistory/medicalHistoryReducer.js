import * as medicalHistoryActions from "./medicalHistoryActionsTypes";

const initialState = {
  medicalHistories: [],
  medicalHistoryDetails: null,
  loading: false,
  error: null,
};

const medicalHistoryReducer = (state = initialState, action) => {
  switch (action.type) {
    case medicalHistoryActions.GET_MEDICAL_HISTORIES_REQUEST:
      return { ...state, loading: true };

    case medicalHistoryActions.GET_MEDICAL_HISTORIES_SUCCESS:
      return { ...state, medicalHistories: action.payload, loading: false };

    case medicalHistoryActions.GET_MEDICAL_HISTORIES_FAILURE:
      return { ...state, error: action.payload, loading: false };

    case medicalHistoryActions.SET_LOADING:
      return {
        ...state,
        isLoading: action.payload,
      };

    case medicalHistoryActions.SET_MEDICAL_HISTORIES:
      return {
        ...state,
        medicalHistories: action.payload,
      };

    case medicalHistoryActions.GET_MEDICAL_HISTORY_REQUEST:
      return {
        ...state,
        isLoading: true,
        error: null,
      };
    case medicalHistoryActions.GET_MEDICAL_HISTORY_SUCCESS:
      return {
        ...state,
        medicalHistoryDetails: action.payload,
        isLoading: false,
      };
    case medicalHistoryActions.GET_MEDICAL_HISTORY_FAILURE:
      return {
        ...state,
        error: action.payload,
        isLoading: false,
      };

    case medicalHistoryActions.CREATE_MEDICAL_HISTORY_REQUEST:
      return {
        ...state,
        isLoading: true,
        error: null,
      };
    case medicalHistoryActions.CREATE_MEDICAL_HISTORY_SUCCESS:
      return {
        ...state,
        isLoading: false,
        medicalHistories: [...state.medicalHistories, action.payload],
      };
    case medicalHistoryActions.CREATE_MEDICAL_HISTORY_FAILURE:
      return {
        ...state,
        isLoading: false,
        error: action.payload,
      };

    case medicalHistoryActions.UPDATE_MEDICAL_HISTORY_REQUEST:
      return {
        ...state,
        isLoading: true,
        error: null,
      };
    case medicalHistoryActions.UPDATE_MEDICAL_HISTORY_SUCCESS:
      return {
        ...state,
        isLoading: false,
        medicalHistoryDetails: action.payload,
      };
    case medicalHistoryActions.UPDATE_MEDICAL_HISTORY_FAILURE:
      return {
        ...state,
        isLoading: false,
        error: action.payload,
      };

    case medicalHistoryActions.DELETE_MEDICAL_HISTORY_REQUEST:
      return {
        ...state,
        isLoading: true,
        error: null,
      };
    case medicalHistoryActions.DELETE_MEDICAL_HISTORY_SUCCESS:
      return {
        ...state,
        isLoading: false,
        medicalHistories: state.medicalHistories.filter(
          (history) => history.medicalHistoryId !== action.payload
        ),
      };
    case medicalHistoryActions.DELETE_MEDICAL_HISTORY_FAILURE:
      return {
        ...state,
        isLoading: false,
        error: action.payload,
      };

    default:
      return state;
  }
};

export default medicalHistoryReducer;
