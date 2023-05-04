import { up, down } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../../../styles/utils"

export const BackgroundPhotoContainer = styled.div<{
  imageUrl?: string | null
}>`
  cursor: pointer;

  width: ${rem(783)};
  height: ${rem(156)};

  background: rgba(100, 100, 100, 0.2);
  background-image: url(${({ imageUrl }) => imageUrl});
  background-position: center;
  background-size: cover;
  border-radius: 0 0 ${({ theme }) => theme.radius.small}
    ${({ theme }) => theme.radius.small};

  ${down("md")} {
    height: 216px;
  }

  ${up("md")} {
    height: 156px;
  }
`
