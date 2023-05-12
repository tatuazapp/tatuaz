import { extendTheme } from "@chakra-ui/react"
import { modalTheme } from "../components/common/modals/chakraModals"
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
  config: {
    initialColorMode: "dark",
    useSystemColorMode: false,
  },
  styles: {
    global: {
      body: {
        bg: theme.colors.background1,
        minHeight: "var(--vh, 100vh)",
      },
    },
  },

  fonts: {
    heading: `Inter, sans-serif`,
    body: `Inter, sans-serif`,
  },

  components: { Modal: modalTheme },

  colors: {
    black: makeAllSameColor("#000000"),
    primary: makeAllSameColor(theme.colors.primary),
    secondary: theme.colors.secondary,
    background1: makeAllSameColor(theme.colors.background1),
    background2: makeAllSameColor(theme.colors.background2),
    background3: makeAllSameColor(theme.colors.background3),
    background4: makeAllSameColor(theme.colors.background4),
  },
})

export default chakraTheme
