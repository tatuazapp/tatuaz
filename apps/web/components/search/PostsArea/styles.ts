import { down } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../../styles/utils"

export const PostCardAreaWrapper = styled.div`
  overflow-y: auto;
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: ${({ theme }) => theme.space.medium};

  min-width: 60vw;
  height: calc(var(--vh, 100vh) - ${rem(232)});

  ${down("lg")} {
    grid-template-columns: repeat(2, 1fr);
    height: calc(var(--vh, 100vh) - ${rem(348)});
  }

  ${down("sm")} {
    grid-template-columns: 1fr;
    height: calc(var(--vh, 100vh) - ${rem(300)});
  }
`
