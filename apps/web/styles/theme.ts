import { DefaultTheme } from "styled-components"
import { rem } from "./utils"

export const theme: DefaultTheme = {
  colors: {
    primary: "#fff",
    secondary: "#1E1E1E",
  },
  space: {
    xxxsmall: "4px",
    xxsmall: "8px",
    xsmall: "12px",
    small: "16px",
    medium: "20px",
    large: "24px",
    xlarge: "32px",
    xxlarge: "48px",
    xxxlarge: "64px",
  },
  sizes: {
    xxxsmall: rem(4),
    xxsmall: rem(8),
    xsmall: rem(12),
    small: rem(16),
    medium: rem(20),
    large: rem(24),
    xlarge: rem(32),
    xxlarge: rem(48),
    xxxlarge: rem(64),
  },
  gradients: {
    card: "61.62deg, rgba(130, 33, 50, 0.6) 0%, rgba(191, 34, 62, 0.425) 40.63%, rgba(130, 33, 50, 0) 100%",
  },
}
