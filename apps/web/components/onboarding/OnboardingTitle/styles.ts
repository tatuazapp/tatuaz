import { h1, h4, heading } from "@tatuaz/ui"
import { down } from "styled-breakpoints"
import styled from "styled-components"

export const OnboardingTitle = styled.div`
  color: ${({ theme }) => theme.colors.secondary};
  text-align: center;

  ${h1}
  ${heading}
  ${down("md")} {
    ${h4}
  }
`
