import { down } from "styled-breakpoints"
import styled from "styled-components"

export const OnboardingPageWrapper = styled.div`
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  padding: ${({ theme }) =>
    `150px ${theme.space.xxxlarge} ${theme.space.xxlarge}`};

  ${down("sm")} {
    padding: ${({ theme }) =>
      ` ${theme.space.xxlarge} ${theme.space.medium} ${theme.space.xxlarge}`};
  }
`
