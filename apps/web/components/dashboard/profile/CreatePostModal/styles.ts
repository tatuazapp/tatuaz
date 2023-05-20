import { down } from "styled-breakpoints"
import styled from "styled-components"

export const UploadedPhotosWrapper = styled.div`
  overflow: auto;
  display: grid;
  grid-gap: ${({ theme }) => theme.space.small};
  grid-template-columns: repeat(auto-fill, minmax(180px, 1fr));

  max-height: 460px;
  ${down("lg")} {
    padding-right: ${({ theme }) => theme.space.small};
    padding-left: ${({ theme }) => theme.space.small};
  }
`
