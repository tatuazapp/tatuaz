import { down } from "styled-breakpoints"
import styled from "styled-components"

export const HomepageIntroAndPhotosWrapper = styled.div`
  display: flex;
  justify-content: center;
  padding-bottom: ${({ theme }) => theme.sizes.xlarge};
  margin-top: ${({ theme }) => theme.sizes.xxxxlarge};

  ${down("md")} {
    flex-direction: column-reverse;
    padding-bottom: ${({ theme }) => theme.sizes.xlarge};
    margin-top: ${({ theme }) => theme.sizes.medium};
  }

  ${down("sm")} {
    flex-direction: column-reverse;
    padding-bottom: ${({ theme }) => theme.sizes.medium};
  }
`
