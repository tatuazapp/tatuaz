import { down } from "styled-breakpoints"
import styled from "styled-components"

export const SearchContentWrapper = styled.div`
  display: flex;
  flex-direction: column;
  width: 100%;
  margin-right: ${({ theme }) => theme.space.xxxxlarge};

  ${down("lg")} {
    width: 100%;
    min-width: 250px;
    max-width: 735px;
    margin-right: 0;
  }
`
