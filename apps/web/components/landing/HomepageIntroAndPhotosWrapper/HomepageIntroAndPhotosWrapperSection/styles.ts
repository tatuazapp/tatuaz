import { down } from "styled-breakpoints"
import styled from "styled-components"

export const HomepageIntroAndPhotosSection = styled.div`
  display: flex;
  justify-content: center;
  width: 50%;

  ${down("md")} {
    width: 100%;
  }
`
