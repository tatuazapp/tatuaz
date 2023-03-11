import { down } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../../styles/utils"

export const ArtistCardAreaWrapper = styled.div`
  overflow-y: auto;
  display: flex;
  flex-wrap: wrap;
  justify-content: space-around;

  height: calc(100vh - ${rem(232)});

  ${down("lg")} {
    height: calc(100vh - ${rem(348)});
  }

  ${down("sm")} {
    height: calc(100vh - ${rem(300)});
  }
`
