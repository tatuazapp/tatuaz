import { up, down } from "styled-breakpoints"
import styled from "styled-components"

export const BackgroundPhotoContainer = styled.div`
  position: absolute;
  top: 0;
  left: 0;

  width: 100%;

  background: #d9d9d9;
  border-radius: 20px;

  ${down("md")} {
    height: 216px;
  }

  ${up("md")} {
    height: 156px;
  }
`
