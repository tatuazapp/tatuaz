import { h1, h3, heading } from "@tatuaz/ui"
import { up } from "styled-breakpoints"
import styled from "styled-components"

export const PreferencesTitles = styled.div`
  ${heading}

  color: ${({ theme }) => theme.colors.secondary};

  ${h3}

  ${up("md")} {
    ${h1}
  }
`
