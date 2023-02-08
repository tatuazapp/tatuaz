import { down } from "styled-breakpoints"
import styled from "styled-components"

export const FAQsHeaderWrapper = styled.div`
  display: flex;
  justify-content: center;

  margin-bottom: ${({ theme }) => theme.space.xxsmall};

  font-size: ${({ theme }) => theme.sizes.xxlarge};
  font-weight: 600;
  color: ${({ theme }) => theme.colors.secondary};

  /* ${down("md")} {
    padding-top: ${({ theme }) => theme.sizes.xxlarge};
  } */

  ${down("sm")} {
    font-size: ${({ theme }) => theme.sizes.xlarge};
  }
`

export const LastLetterColorAccent = styled.p`
  color: ${({ theme }) => theme.colors.primary};
`
