import { Avatar } from "@chakra-ui/react"
import { down } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../../../styles/utils"

export const BackgroundAndAvatarContainer = styled.div`
  position: relative;
  height: min-content;
`

const DESKTOP_BACKGROUND_HEIGHT = 156
const MOBILE_BACKGROUND_HEIGHT = 136

export const BackgroundPhotoContainer = styled.div<{
  imageUrl?: string | null
}>`
  cursor: pointer;

  width: ${rem(783)};
  height: ${rem(DESKTOP_BACKGROUND_HEIGHT)};

  background: rgba(100, 100, 100, 0.2);
  background-image: url(${({ imageUrl }) => imageUrl});
  background-position: center;
  background-size: cover;
  border-radius: 0 0 ${({ theme }) => theme.radius.medium}
    ${({ theme }) => theme.radius.medium};

  ${down("md")} {
    flex: 1;
    width: 100vw;
    height: ${rem(MOBILE_BACKGROUND_HEIGHT)};
  }

  :hover {
    filter: brightness(0.8);
    transition: filter 0.2s ease-in-out;
  }
`

const DESKTOP_AVATAR_SIZE = 124
const MOBILE_AVATAR_SIZE = 110

export const AvatarContainer = styled(Avatar)`
  cursor: pointer;

  position: absolute;
  z-index: 1;
  bottom: ${({ theme }) => theme.space.xxxlarge};
  left: ${({ theme }) => theme.space.xlarge};

  overflow: hidden;

  width: ${rem(DESKTOP_AVATAR_SIZE)} !important;
  height: ${rem(DESKTOP_AVATAR_SIZE)} !important;

  border: 3px solid white !important;

  ${down("md")} {
    width: ${rem(MOBILE_AVATAR_SIZE)} !important;
    height: ${rem(MOBILE_AVATAR_SIZE)} !important;
  }

  :hover {
    filter: brightness(0.8);
    transition: filter 0.2s ease-in-out;
  }
`

export const UserInfoContainer = styled.div`
  position: absolute;
  z-index: 1;
  top: calc(${({ theme }) => theme.space.large} + ${rem(124)} + 1rem);
  left: calc(${({ theme }) => theme.space.xlarge} + ${rem(124)} + 1rem);

  display: flex;
  flex-direction: column;

  ${down("md")} {
    top: calc(
      ${rem(MOBILE_BACKGROUND_HEIGHT)} + ${({ theme }) => theme.space.xxsmall}
    );
    left: calc(
      ${({ theme }) => theme.space.xxlarge} + ${rem(MOBILE_AVATAR_SIZE)}
    );
  }

  ${down("sm")} {
    top: calc(
      ${rem(MOBILE_BACKGROUND_HEIGHT)} + ${({ theme }) => theme.space.xxsmall}
    );
    left: calc(
      ${({ theme }) => theme.space.xxlarge} + ${rem(MOBILE_AVATAR_SIZE)}
    );
  }
`
