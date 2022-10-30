import * as pl from "./pl.json"

export type PlTranslationsKeys = keyof typeof pl

export const messages = {
  pl: pl,
}

export const currentLocale = "pl"
export const defaultLocale = "pl"
