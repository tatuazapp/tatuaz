import { down } from "styled-breakpoints"
import styled from "styled-components"

export const UploadedPhotosWrapper = styled.div`
  overflow: auto;
  max-height: 160px;
  ${down("lg")} {
    padding-right: ${({ theme }) => theme.space.small};
    padding-left: ${({ theme }) => theme.space.small};
  }
`
