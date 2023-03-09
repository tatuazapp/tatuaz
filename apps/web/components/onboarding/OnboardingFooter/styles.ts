import { down } from "styled-breakpoints"
import styled from "styled-components"

export const OnboardingFooterWrapper = styled.div<{
  justifyContent?: "space-between" | "flex-end" | "stretch"
}>`
  display: flex;
  align-items: center;
  justify-content: ${({ justifyContent }) => justifyContent || "space-between"};
  margin-top: auto;

  ${down("md")} {
    gap: ${({ theme }) => theme.space.xlarge};
  }
`
