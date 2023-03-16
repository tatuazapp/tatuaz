import { down } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../../styles/utils"

export const SearchContentWrapper = styled.div`
  display: flex;
  flex-direction: column;
  margin-right: ${({ theme }) => theme.space.xxxxlarge};

  ${down("lg")} {
    min-width: ${rem(250)};
    max-width: ${rem(735)};
    margin-right: 0;
  }
`
