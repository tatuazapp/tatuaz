import { extendTheme } from "@chakra-ui/react"
import { theme } from "./theme"

const makeAllSameColor = (color: string) => ({
  50: color,
  100: color,
  200: color,
  300: color,
  400: color,
  500: color,
  600: color,
  700: color,
  800: color,
  900: color,
})

const chakraTheme = extendTheme({
  initialColorMode: "dark",
  styles: {
    global: {
      body: {
        bg: theme.colors.background1,
        minHeight: "100vh",
      },
    },
  },

  fonts: {
    heading: `Inter, sans-serif`,
    body: `Inter, sans-serif`,
  },

  colors: {
    black: makeAllSameColor("#000000"),
    primary: makeAllSameColor(theme.colors.primary),
    secondary: theme.colors.secondary,
  },
})

export default chakraTheme
