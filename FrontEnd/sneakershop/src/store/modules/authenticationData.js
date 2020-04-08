const APIENDPOINT = 'https://sneakershopapi.azurewebsites.net/graphql';
const APIENDPOINTADMIN =
	'https://sneakershopapi.azurewebsites.net/graphql/admin';

export default {
	state: {
		isAuth: false,
		isAdmin: false,

		userId: String,
		allUsers: null,
		user: null
	},

	getters: {
		isAuth: state => state.isAuth,
		isAdmin: state => state.isAdmin,

		userId: state => state.userId,

		allUsers: state => state.allUsers,
		user: state => state.user
	},

	mutations: {
		setAuth: async function(state, data) {
			if (data.token != null) {
				// 1. Set state property 'isAuth' on true
				state.isAuth = true;

				// 1. Set bearer token to localStorage
				localStorage.setItem('bearer', data.token);

				// 2. SetRole
				if (data.role == 'Admin') {
					state.isAdmin = true;
					localStorage.setItem('isAdmin', true);
				} else {
					state.isAdmin = false;
				}

				// 4. Set state properties UserId
				state.userId = data.currentUser;
				localStorage.setItem('userId', data.currentUser);
				localStorage.setItem('isAuth', true);
			} else {
				// 1. Set state property 'isAuth' on false
				state.isAuth = false;
			}
		},
		setAllUsers: async function(state, data) {
			state.allUsers = data;
		},
		setUser: async function(state, data) {
			state.user = data;
		}
	},

	actions: {
		login: async function({ commit }, credentials) {
			return new Promise(async function(resolve, reject) {
				try {
					const payload = `{"query" : 
                    "
                    mutation login($login: loginInput!) {login(login: $login)}
                    ",
                    
                    "operationName": "login",
                    
                    "variables":{
                        "login": 
                        {
                            "userName" : "${credentials.userName}",
                            "password" : "${credentials.password}"
                        }
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

					// 4. Commit result to setAuth
					commit('setAuth', data.data.login);
					resolve(data.data.login);
				} catch {
					reject(data.data.login);
				}
			});
		},

		register: async function({ commit }, payload) {
			const request = await fetch(APIENDPOINT + 'register', {
				method: 'POST',
				headers: { 'Content-Type': 'application/json' },
				body: JSON.stringify(payload)
			});
			// const data = await request.text();
			// console.log(data);
		},

		getAllUsers: async function({ commit }) {
			const payload = `{"query" : 
			"
			query allusers {users{id userName firstName lastName city country}}
			
			"
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
			commit('setAllUsers', data.data.users);
		},
		getUser: async function({ commit }, userId) {
			const payload = `{"query" : 
			"
			query userById($userId: ID!)
			{user(id: $userId){id userName firstName lastName city country}}
			",
			"variables":{
				"userId": "${userId}"
			}
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
			commit('setUser', data.data.user);
		}
	}
};
