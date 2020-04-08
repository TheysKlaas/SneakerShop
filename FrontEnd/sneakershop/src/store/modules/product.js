const APIENDPOINT = 'https://sneakershopapi.azurewebsites.net/graphql';
export default {
	state: {
		products: null,
		filterdProducts: null,
		basketProducts: null
	},
	getters: {
		products: state => state.products,
		filterdProducts: state => state.filterdProducts,
		basketProducts: state => state.basketProducts
	},
	mutations: {
		setProducts: function(state, data) {
			state.products = data;
		},
		setBasketProducts: function(state, data) {
			state.basketProducts = data;
		},
		setFilterdProducts: function(state, data) {
			state.filterdProducts = data;
		},
		addProductToBasket: (state, productForBasket) => {
			if (localStorage.getItem('basket')) {
				const existingProducts = localStorage.getItem('basket');
				const updatedProducts = JSON.parse(existingProducts);
				updatedProducts.push(productForBasket);
				localStorage.setItem('basket', JSON.stringify(updatedProducts));
			} else {
				localStorage.setItem(
					'basket',
					JSON.stringify([productForBasket])
				);
			}
		}
	},
	actions: {
		getProducts: async function({ commit }) {
			const payload = `{"query" : 
            "
            query products{products{productId productName unitPrice supplier{companyName}}}
            
            "
            }`;
			const request = await fetch(APIENDPOINT, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: payload
			});

			const data = await request.json();
			commit('setProducts', data.data.products);
		},
		getNewProducts: async function({ commit }) {
			const payload = `{"query" : 
            "
            query products{products{productId productName unitPrice supplier{companyName}}}
            
            "
            }`;
			const request = await fetch(APIENDPOINT, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: payload
			});

			const data = await request.json();
			commit('setProducts', data.data.products.slice(0, 4));
		},
		getProduct: async function({ commit }, id) {
			const payload = `{"query" : 
			"
			query productbyid($productId: ID!){product(id: $productId){productId productName unitPrice supplier{companyName}}}
			",
			"variables":{
				"productId":"${id}"
			}
			}`;
			const request = await fetch(APIENDPOINT, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: payload
			});

			const data = await request.json();
			commit('setProducts', data.data.product);
		},
		getProductsFromBasket: async function({ commit }) {
			var products = [];
			var basket = JSON.parse(localStorage.getItem('basket'));

			// Loop through the object and print the count for each fruit
			for (var product in basket) {
				console.log(
					'product: ' +
						basket[product].productId +
						'\nsize: ' +
						basket[product].size
				);
				const payload = `{"query" :
				"
				query productbyid($productId: ID!){product(id: $productId){productId productName unitPrice supplier{companyName}}}
				",
				"variables":{
					"productId":"${basket[product].productId}"
				}
				}`;
				const request = await fetch(APIENDPOINT, {
					method: 'POST',
					headers: {
						'Content-Type': 'application/json'
					},
					body: payload
				});

				const data = await request.json();
				data.data.product.size = basket[product].size;
				products.push(data.data.product);
			}
			commit('setBasketProducts', products);
		},
		updateProducts: async function({ commit }, data) {
			commit('setFilterdProducts', data);
		}
	}
};
