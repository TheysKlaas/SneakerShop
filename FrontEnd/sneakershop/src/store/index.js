import Vue from 'vue';
import Vuex from 'vuex';
import { stat } from 'fs';
import { async } from 'q';

import brand from './modules/brand';
import product from './modules/product';
import authenticationData from './modules/authenticationData.js';
import order from './modules/order.js';
Vue.use(Vuex);

export default new Vuex.Store({
	modules: {
		brand,
		product,
		authenticationData,
		order
	}
});
