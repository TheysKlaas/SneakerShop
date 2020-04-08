const APIENDPOINT =
	'https://sneakershopapi.azurewebsites.net/graphql/restricted';
const APIENDPOINTADMIN =
	'https://sneakershopapi.azurewebsites.net/graphql/admin';
export default {
	state: { orders: null, allOrders: null, order: null },
	getters: {
		orders: state => state.orders,
		allOrders: state => state.allOrders,
		order: state => state.order
	},
	mutations: {
		setOrders: function(state, data) {
			state.orders = data;
		},
		setOrder: function(state, data) {
			state.order = data;
		},
		setAllOrders: function(state, data) {
			state.allOrders = data;
		}
	},
	actions: {
		placeOrder: async function({ commit }) {
			var totalPrice = 0;
			var basket = JSON.parse(localStorage.getItem('basket'));
			for (var product in basket) {
				totalPrice += basket[product].price;
			}

			var today = new Date();
			var date =
				today.getFullYear() +
				'-' +
				(today.getMonth() + 1) +
				'-' +
				today.getDate();
			var time =
				today.getHours() +
				':' +
				today.getMinutes() +
				':' +
				today.getSeconds();
			var dateTime = date + ' ' + time;
			const payload = `{"query" : 
            "
            mutation createOrder($order: orderInput!) {createOrder(order: $order) {orderId userID timestamp}}
            ",
            
            "variables":{
            "order": {
              "userID": "${localStorage.userId}",
			  "timestamp": "${dateTime}",
			  "totalPrice" : ${totalPrice}
                }
            }
            }`;
			const request = await fetch(APIENDPOINT, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
					Authorization: 'bearer ' + localStorage.bearer
				},
				body: payload
			});

			const data = await request.json();

			for (var product in basket) {
				const payload = `{"query" : 
                 "
                 mutation createOrderItem($orderItem: orderItemInput!) {createOrderItem(orderItem: $orderItem) {orderID productID size product {productName unitPrice}}}
                 ",
                 
                 "variables":{
                     "orderItem": {
                   "orderID": "${data.data.createOrder.orderId}",
                   "productID": "${basket[product].productId}",
                   "size" : ${basket[product].size}
                     }
                 }
                 }`;
				const request = await fetch(APIENDPOINT, {
					method: 'POST',
					headers: {
						'Content-Type': 'application/json',
						Authorization: 'bearer ' + localStorage.bearer
					},
					body: payload
				});
				const dataorderitem = await request.json();
				localStorage.setItem('basket', '');
			}
		},
		getOrderForUser: async function({ commit }, userId) {
			const payload = `{"query" : 
			"
			query ordersByUser($userId: ID!)
			{ordersByUserId(id: $userId){orderId user{userName} timestamp totalPrice products{productID product{productName unitPrice supplier{companyName}}}}}
			
			",
			
			"operationName": "ordersByUser",
			"variables":{
				"userId":"${userId}"
			}
			}`;
			const request = await fetch(APIENDPOINT, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
					Authorization: 'bearer ' + localStorage.bearer
				},
				body: payload
			});

			const data = await request.json();
			commit('setOrders', data.data.ordersByUserId);
		},
		getallorders: async function({ commit }) {
			const payload = `{"query" : 
			"
			query all {orders{orderId timestamp user{firstName lastName} totalPrice }}
			",
			
			"operationName": "all"
			}`;
			const request = await fetch(APIENDPOINTADMIN, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
					Authorization: 'bearer ' + localStorage.bearer
				},
				body: payload
			});

			const data = await request.json();
			commit('setAllOrders', data.data.orders);
		},
		getOrder: async function({ commit }, orderId) {
			const payload = `{"query" : 
			"
			query orderById($orderId: ID!)
			{order(id: $orderId){orderId timestamp user{firstName lastName} totalPrice products{ product{supplier{companyName} productId productName unitPrice} size}}}
			
			",
			
			"operationName": "orderById",
			"variables":{
				"orderId": "${orderId}"
			}
			}
			
			`;
			const request = await fetch(APIENDPOINTADMIN, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
					Authorization: 'bearer ' + localStorage.bearer
				},
				body: payload
			});

			const data = await request.json();
			commit('setOrder', data.data.order);
		}
	}
};
