import "styled-components"

declare module "styled-components" {
  type Colors = {
    primary: string
    secondary: string
    background1: string
    background2: string
  }

  type Radius = {
    xxxsmall: string
    xxsmall: string
    xsmall: string
    small: string
    medium: string
    large: string
    xlarge: string
    xxlarge: string
    xxxlarge: string
  }

  type Space = {
    xxxsmall: string
    xxsmall: string
    xsmall: string
    small: string
    medium: string
    large: string
    xlarge: string
    xxlarge: string
    xxxlarge: string
  }

  type Sizes = {
    xxxsmall: string
    xxsmall: string
    xsmall: string
    small: string
    medium: string
    large: string
    xlarge: string
    xxlarge: string
    xxxlarge: string
  }

  type Gradients = {
    card: string
  }

  export interface DefaultTheme {
    colors: Colors
    space: Space
    sizes: Sizes
    gradients: Gradients
    radius: Radius
  }
}
