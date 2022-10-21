import type { Config } from "prismic-ts-codegen"

const config: Config = {
  output: "./types.generated.d.ts",
  models: ["../../customtypes/**/index.json", "./slices/**/model.json"],
}

export default config
