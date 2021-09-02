var _ = require('lodash');

//https://github.com/facebook/create-react-app/blob/master/packages/react-scripts/config/webpack.config.js
const serverSettings = {
    devtool: 'none',
    entry: './src/server.js',
    output: {
        filename: 'server/server.js',
        library: 'lorem',
        publicPath: '/',
        devtoolNamespace: ''
    },
    optimization: {
        runtimeChunk: false
    }
}

module.exports = {
    style: {
    postcss: {
            plugins: [
                require('tailwindcss'),
                require('autoprefixer'),
            ]
        },
    },
    webpack: {
       
        configure: (config, { env, paths }) => {

            if(process.env.BUILD_MODE === 'server') {

                console.log('building for server');

                let modified = Object.assign({}, config);
                delete modified.entry;
                delete modified.optimization.splitChunks;
    
                _.merge(modified, serverSettings);
    
                return modified;
            }

            return config;
        }
    }
}