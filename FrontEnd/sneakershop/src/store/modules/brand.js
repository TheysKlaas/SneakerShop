const APIENDPOINT = 'https://sneakershopapi.azurewebsites.net/graphql';
export default {
	state: {
		brands: null
	},
	getters: {
		brands: state => state.brands
	},
	mutations: {
		setBrands: function(state, data) {
			state.brands = data;
		}
	},
	actions: {
		getBrands: async function({ commit }) {
			const payload = `{"query" : 
            "
            query suppliers{suppliers{companyName product{productName unitPrice}}}            
            ",            
            "operationName": "suppliers"
            }`;
			const request = await fetch(APIENDPOINT, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: payload
			});

			const data = await request.json();
			commit('setBrands', data.data.suppliers);
		}
	}
};
