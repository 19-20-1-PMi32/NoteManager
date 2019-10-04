const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

let isProduction =
  process.argv.indexOf('-p') >= 0 || process.env.NODE_ENV === 'production';

const WebpackCleanupPlugin = require('webpack-cleanup-plugin');

module.exports = {
  entry: './src/index.tsx',
  node: {
    fs: 'empty'
  },
  output: {
    filename: '[name].[hash].bundle.js',
    chunkFilename: '[chunkhash].js',
    path: path.resolve(__dirname, 'dist'),
  },
  devtool: !isProduction ? 'source-map' : false,
  resolve: {
    extensions: ['.js', '.json', '.ts', '.tsx'],
  },
  stats: {
    warnings: false,
    modules: false,
    colors: true,
    entrypoints: false,
    version: false,
  },
  module: {
    rules: [
      {
        test: /\.(ts|tsx)$/,
        use: ['babel-loader', 'awesome-typescript-loader'],
        exclude: /node_modules/,
      },
      {
        test: /\.css$/,
        sideEffects: true,
        use: [
          {
            loader: 'style-loader',
          },
          {
            loader: 'css-loader',
          },
        ],
      },
      { test: /\.html$/, use: 'html-loader' },
      { test: /\.(a?png|svg)$/, use: 'url-loader?limit=10000' },
      {
        test: /\.(jpe?g|gif|bmp|mp3|mp4|ogg|wav|eot|ttf|woff|woff2)$/,
        use: 'file-loader',
      },
    ],
  },
  optimization: {
    splitChunks: {
      name: true,
      cacheGroups: {
        commons: {
          chunks: 'initial',
          minChunks: 2,
        },
        vendors: {
          test: /[\\/]node_modules[\\/]/,
          chunks: 'all',
          priority: -10,
        },
      },
    },
    runtimeChunk: true,
  },
  plugins: [
    new WebpackCleanupPlugin(),
    new HtmlWebpackPlugin({
      inject: 'body',
      template: '../Views/Shared/_Layout_Template.cshtml',
      filename: '../../Views/Shared/_Layout.cshtml',
    }),
  ],
};
