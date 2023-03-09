import { createTheme } from "styled-breakpoints"
import { DefaultTheme } from "styled-components"
import { rem } from "./utils"

export const theme: DefaultTheme = {
  colors: {
    primary: "#D5FF40",
    secondary: "#FFF",
    background1: "#151515",
    background2: "#2B2B2B",
    background3: "#494949",
    background4: "#6B6B6B",
  },

  animationTime: {
    xxxxslow: "1.5s",
    xxxslow: "1s",
    xxslow: "0.9s",
    xslow: "0.8s",
    slow: "0.7s",
    medium: "0.6s",
    fast: "0.5s",
    xfast: "0.4s",
    xxfast: "0.3s",
    xxxfast: "0.2s",
    xxxxfast: "0.1s",
  },

  radius: {
    xxxxsmall: "2px",
    xxxsmall: "4px",
    xxsmall: "8px",
    xsmall: "12px",
    small: "16px",
    medium: "20px",
    large: "24px",
    xlarge: "32px",
    xxlarge: "48px",
    xxxlarge: "64px",
    xxxxlarge: "80px",
  },
  space: {
    xxxxsmall: "2px",
    xxxsmall: "4px",
    xxsmall: "8px",
    xsmall: "12px",
    small: "16px",
    medium: "20px",
    large: "24px",
    xlarge: "32px",
    xxlarge: "48px",
    xxxlarge: "64px",
    xxxxlarge: "80px",
  },
  sizes: {
    xxxxsmall: rem(2),
    xxxsmall: rem(4),
    xxsmall: rem(8),
    xsmall: rem(12),
    small: rem(16),
    medium: rem(20),
    large: rem(24),
    xlarge: rem(32),
    xxlarge: rem(48),
    xxxlarge: rem(64),
    xxxxlarge: rem(80),
  },
  gradients: {
    card: "61.62deg, rgba(130, 33, 50, 0.6) 0%, rgba(191, 34, 62, 0.425) 40.63%, rgba(130, 33, 50, 0) 100%",
  },
}

export const themeWithBreakpoints = createTheme({
  xs: "420px",
  sm: "576px",
  md: "768px",
  lg: "992px",
  xl: "1200px",
  xxl: "1400px",
  xxxl: "1700px",
} as const)
