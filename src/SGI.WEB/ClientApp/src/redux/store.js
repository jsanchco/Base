import { createStore, applyMiddleware, compose } from 'redux';
import logger from "redux-logger";
import reducers from './reducers';

const store = createStore(reducers, applyMiddleware(logger), window.REDUX_INITIAL_DATA);

export default store;