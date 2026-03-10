import {createStore} from 'redux';
import productsReducer from '../reducers/productsReducers';

const store = createStore(productsReducer);

export default store;