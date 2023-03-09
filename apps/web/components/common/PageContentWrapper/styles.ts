import { down, up } from "styled-breakpoints"
import styled from "styled-components"

export const PageContentWrapper = styled.div`
  margin-right: ${({ theme }) => theme.space.xxxxlarge};
  margin-left: ${({ theme }) => theme.space.xxxxlarge};

  ${up("sm") && down("md")} {
    margin-right: ${({ theme }) => theme.space.xxlarge};
    margin-left: ${({ theme }) => theme.space.xxlarge};
  }

  ${down("sm")} {
    margin-right: ${({ theme }) => theme.space.small};
    margin-left: ${({ theme }) => theme.space.small};
  }
`
