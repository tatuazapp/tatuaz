import "styled-components"

// Keep in sync with apps/web/declarations/styled.d.ts
declare module "styled-components" {
  type Colors = {
    primary: string
    secondary: string
  }

  type Space = {
    xxxsmall: number
    xxsmall: number
    xsmall: number
    small: number
    medium: number
    large: number
    xlarge: number
    xxlarge: number
    xxxlarge: number
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
  }
}
