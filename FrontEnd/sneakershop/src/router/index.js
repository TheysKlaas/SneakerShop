import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from '../views/Home.vue';

Vue.use(VueRouter);

const routes = [
	{
		path: '/',
		name: 'home',
		component: () =>
			import(/* webpackChunkName: "home" */ '../views/Home.vue')
	},
	{
		path: '/products',
		name: 'products',
		component: () =>
			import(/* webpackChunkName: "products" */ '../views/Products.vue')
	},
	{
		path: '/product/:id',
		name: 'productDetail',
		component: () =>
			import(
				/* webpackChunkName: 'productDetail' */ '../views/ProductDetail.vue'
			)
	},
	{
		path: '/brands',
		name: 'brands',
		component: () =>
			import(/* webpackChunkName: 'brands' */ '../views/Brands.vue')
	},
	{
		path: '/login',
		name: 'login',
		component: () =>
			import(/* webpackChunkName: 'login' */ '../views/Login.vue')
	},
	{
		path: '/basket',
		name: 'basket',
		component: () =>
			import(/* webpackChunkName: 'basket' */ '../views/Basket.vue')
	},
	{
		path: '/myorders',
		name: 'myorders',
		component: () =>
			import(/* webpackChunkName: 'myorders' */ '../views/MyOrders.vue')
	},
	{
		path: '/allorders',
		name: 'allorders',
		component: () =>
			import(/* webpackChunkName: 'allorders' */ '../views/AllOrders.vue')
	},
	{
		path: '/orderDetail/:id',
		name: 'orderDetail',
		component: () =>
			import(
				/* webpackChunkName: 'orderDetail' */ '../views/OrderDetail.vue'
			)
	},
	{
		path: '/allusers',
		name: 'allusers',
		component: () =>
			import(/* webpackChunkName: 'allusers' */ '../views/AllUsers.vue')
	},
	{
		path: '/user/:id',
		name: 'userDetail',
		component: () =>
			import(
				/* webpackChunkName: 'userDetail' */ '../views/UserDetail.vue'
			)
	}
];

const router = new VueRouter({
	routes
});

export default router;
