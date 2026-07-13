import terser from "@rollup/plugin-terser";
import esbuild from "rollup-plugin-esbuild";
import nodeResolve from "@rollup/plugin-node-resolve";
import litcss from 'rollup-plugin-lit-css';

const plugins = [
    esbuild({
        tsconfig: './tsconfig.json',
        exclude: ["**/*.bench.*", "**/*.spec.*"],
        loaders: {
            '.ts': 'ts',
            '.tsx': 'tsx'
        }
    }),
    litcss(),
    nodeResolve()
];

export default [
    {
        input: "src/index.rollup.ts",
        output: [
            {
                file: "../PhotoDb.Editor/wwwroot/js/main.js",
                format: "esm",
                sourcemap: true,
                inlineDynamicImports: true,
            },
            {
                file: "../PhotoDb.Editor/wwwroot/js/main.min.js",
                format: "esm",
                sourcemap: true,
                inlineDynamicImports: true,
                plugins: [terser()],
            },
        ],
        plugins,
    },
];
