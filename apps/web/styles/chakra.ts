import { extendTheme } from "@chakra-ui/react"
import { theme } from "./theme"

const chakraTheme = extendTheme({
  initialColorMode: "dark",
  styles: {
    global: {
      body: {
        bg: "black",
      },
    },
  },

  fonts: {
    heading: `Comfortaa, sans-serif`,
    body: `Comfortaa, sans-serif`,
  },

  colors: {
    black: {
      50: "#000",
      100: "#000",
      200: "#000",
      300: "#000",
      400: "#000",
      500: "#000",
      600: "#000",
      700: "#000",
      800: "#000",
      900: "#000",
    },
    primary: theme.colors.primary,
    secondary: theme.colors.secondary,
  },
})

export default chakraTheme
