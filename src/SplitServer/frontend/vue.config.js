const path = require("path");
const fs = require("fs");
const CompressionPlugin = require("compression-webpack-plugin");

function resolve(dir) {
  return path.join(__dirname, dir);
}

// vue.config.js
let configObj = {
  runtimeCompiler: process.env.NODE_ENV == "development",
  /*
      Vue-cli3:
      Crashed when using Webpack `import()` #2463
      https://github.com/vuejs/vue-cli/issues/2463
     */
  // 如果你不需要生产环境的 source map，可以将其设置为 false 以加速生产环境构建。。
  productionSourceMap: process.env.NODE_ENV != "production",
  configureWebpack: (config) => {
    config.devtool = "source-map";
    //生产环境取消 console.log
    if (process.env.NODE_ENV === "production") {
      config.optimization.minimizer[0].options.terserOptions.compress.drop_console = true;
    }
    // config.resolve = {
    //     alias: {
    //         '@': resolve('src')
    //     }
    // }
  },
  chainWebpack: (config) => {
    // config.resolve.alias
    //     .set('@$', path.resolve(__dirname, 'src'))
    // .set('@api', resolve('src/api'))
    // .set('@assets', resolve('src/assets'))
    // .set('@comp', resolve('src/components'))
    // // .set('@store', resolve('src/store'))
    // .set('@views', resolve('src/views'))

    //这使得在项目中引用子目录中的文件时，可以直接使用相对路径，例如 @MyComponent 等效于src/MyComponent
    let srcComponents = fs.readdirSync(path.resolve(__dirname, "src"), {
      withFileTypes: true,
    });
    srcComponents.forEach((dirent) => {
      //console.log(dirent + "  " + typeof (dirent));
      if (dirent.isDirectory()) {
        config.resolve.alias.set(
          `@${dirent.name}`,
          path.resolve(__dirname, `src/${dirent.name}`)
        );
      }
    });

    //生产环境，开启js\css压缩
    // if (process.env.NODE_ENV === 'production') {
    //     config.plugin('compressionPlugin').use(new CompressionPlugin({
    //         test: /\.(js|css|less)$/, // 匹配文件名
    //         threshold: 10240, // 对超过10k的数据压缩
    //         deleteOriginalAssets: false // 不删除源文件
    //     }))
    // }

    // it can improve the speed of the first screen, it is recommended to turn on preload
    config.plugin("preload").tap(() => [
      {
        rel: "preload",
        // to ignore runtime.js
        // https://github.com/vuejs/vue-cli/blob/dev/packages/@vue/cli-service/lib/config/app.js#L171
        fileBlacklist: [/\.map$/, /hot-update\.js$/, /runtime\..*\.js$/],
        include: "initial",
      },
    ]);

    // when there are many pages, it will cause too many meaningless requests
    config.plugins.delete("prefetch");

    // set svg-sprite-loader
    config.module
      .rule("svg")
      .exclude.add(resolve("src/icons/svg"))
      .add(resolve("./node_modules/@element-plus/icons-svg"))
      .end();
    config.module
      .rule("icons")
      .test(/\.svg$/)
      .include.add(resolve("src/icons/svg"))
      .add(resolve("./node_modules/@element-plus/icons-svg"))
      .end()
      .use("svg-sprite-loader")
      .loader("svg-sprite-loader")
      .options({
        symbolId: "icon-[name]",
      })
      .end();

    // 配置 webpack 识别 markdown 为普通的文件
    config.module
      .rule("markdown")
      .test(/\.md$/)
      .use()
      .loader("file-loader")
      .end();

    // 编译vxe-table包里的es6代码，解决IE11兼容问题
    config.module
      .rule("vxe")
      .test(/\.js$/)
      .include.end()
      .exclude.add(() => ["node_modules/core-js"])
      .end()
      .use()
      .loader("babel-loader")
      .end();

    config.when(process.env.NODE_ENV !== "development", (config) => {
      config
        .plugin("ScriptExtHtmlWebpackPlugin")
        .after("html")
        .use("script-ext-html-webpack-plugin", [
          {
            // `runtime` must same as runtimeChunk name. default is `runtime`
            inline: /runtime\..*\.js$/,
          },
        ])
        .end();
      config.optimization.splitChunks({
        chunks: "all",
        cacheGroups: {          
          libs: {
            name: "chunk-libs",
            test: /[\\/]node_modules[\\/]/,
            priority: 10,
            chunks: "initial", // only package third parties that are initially dependent
          },
          elementUI: {
            name: "chunk-elementUI", // split elementUI into a single package
            priority: 20, // the weight needs to be larger than libs and app or it will be packaged into libs or app
            test: /[\\/]node_modules[\\/]_?element-plus(.*)/, // in order to adapt to cnpm
          },
          commons: {
            name: "chunk-commons",
            test: resolve("src/components"), // can customize your rules
            minChunks: 3, //  minimum common number
            priority: 5,
            reuseExistingChunk: true,
          },
        },
      });
      // https:// webpack.js.org/configuration/optimization/#optimizationruntimechunk
      config.optimization.runtimeChunk("single");
    });
  },
  devServer: {
    port: 3300,
  },

  lintOnSave: undefined,
};
if (process.env.NODE_ENV === "production") {
  if (process.env.VUE_APP_PUBLIC_PATH) {
    configObj.publicPath = `/${process.env.VUE_APP_PUBLIC_PATH}/`;
  }
}
module.exports = configObj;
