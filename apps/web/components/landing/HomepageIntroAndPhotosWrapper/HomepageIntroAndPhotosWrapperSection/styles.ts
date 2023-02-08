import { down } from "styled-breakpoints"
import styled from "styled-components"

export const HomepageIntroAndPhotosSection = styled.div`
  width: 50%;
  display: flex;
  justify-content: center;

  ${down("md")} {
    width: 100%;
  }
`
