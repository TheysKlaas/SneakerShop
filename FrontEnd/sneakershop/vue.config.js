// Inside vue.config.js
module.exports = {
	// ...other vue-cli plugin options...
	pwa: {
		name: 'Sneakerz',
		themeColor: '#779ecb',
		msTileColor: '#FFFFFF',
		appleMobileWebAppCapable: 'yes',
		appleMobileWebAppStatusBarStyle: 'black',

		// configure the workbox plugin
		workboxPluginMode: 'GenerateSW',
		iconPaths: {
			favicon32: 'img/icons/favicon-32x32.png',
			favicon16: 'img/icons/favicon-16x16.png',
			appleTouchIcon: 'img/icons/apple-touch-icon-152x152.png',
			maskIcon: 'img/icons/safari-pinned-tab.svg',
			msTileImage: 'img/icons/msapplication-icon-144x144.png'
		}
		//   workboxOptions: {
		//     // swSrc is required in InjectManifest mode.
		//     swSrc: 'dev/sw.js',
		//     // ...other Workbox options...
		//   }
	}
};
