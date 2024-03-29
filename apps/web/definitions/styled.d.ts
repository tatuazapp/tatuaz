import "styled-components"

declare module "styled-components" {
  type Colors = {
    primary: string
    secondary: string
    background1: string
    background2: string
    background3: string
    background4: string
  }

  type animationTime = {
    xxxxslow: string
    xxxslow: string
    xxslow: string
    xslow: string
    slow: string
    medium: string
    fast: string
    xfast: string
    xxfast: string
    xxxfast: string
    xxxxfast: string
  }

  type Radius = {
    xxxxsmall: string
    xxxsmall: string
    xxsmall: string
    xsmall: string
    small: string
    medium: string
    large: string
    xlarge: string
    xxlarge: string
    xxxlarge: string
    xxxxlarge: string
  }

  type Space = {
    xxxxsmall: string
    xxxsmall: string
    xxsmall: string
    xsmall: string
    small: string
    medium: string
    large: string
    xlarge: string
    xxlarge: string
    xxxlarge: string
    xxxxlarge: string
  }

  type Sizes = {
    xxxxsmall: string
    xxxsmall: string
    xxsmall: string
    xsmall: string
    small: string
    medium: string
    large: string
    xlarge: string
    xxlarge: string
    xxxlarge: string
    xxxxlarge: string
  }

  type Gradients = {
    card: string
  }

  export interface DefaultTheme {
    animationTime: animationTime
    colors: Colors
    space: Space
    sizes: Sizes
    gradients: Gradients
    radius: Radius
  }
}
