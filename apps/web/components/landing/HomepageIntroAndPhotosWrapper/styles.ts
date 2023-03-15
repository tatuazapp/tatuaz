import { down } from "styled-breakpoints"
import styled from "styled-components"

export const HomepageIntroAndPhotosWrapper = styled.div`
  display: flex;
  justify-content: center;
  margin-top: ${({ theme }) => theme.sizes.xxxxlarge};
  padding-bottom: ${({ theme }) => theme.sizes.xlarge};

  ${down("md")} {
    flex-direction: column-reverse;
    margin-top: ${({ theme }) => theme.sizes.medium};
    padding-bottom: ${({ theme }) => theme.sizes.xlarge};
  }

  ${down("sm")} {
    padding-bottom: ${({ theme }) => theme.sizes.medium};
  }
`
