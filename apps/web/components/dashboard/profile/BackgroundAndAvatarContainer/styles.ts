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

// Yeah, this should be controlled by some wrapping component but whatever, no time for that now
export const USER_CONTENT_WIDTH = 783

export const BackgroundPhotoContainer = styled.div<{
  imageUrl?: string | null
  isEditable?: boolean
}>`
  width: ${rem(USER_CONTENT_WIDTH)};
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

  ${({ isEditable: editable }) =>
    editable &&
    `
  cursor: pointer;

  :hover {
    filter: brightness(0.8);
    transition: filter 0.2s ease-in-out;
  }
  `}
`

const DESKTOP_AVATAR_SIZE = 124
const MOBILE_AVATAR_SIZE = 110

export const AvatarContainer = styled(Avatar)<{
  isEditable?: boolean
}>`
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

  ${({ isEditable: editable }) =>
    editable &&
    `
    cursor: pointer;

  :hover {
    filter: brightness(0.8);
    transition: filter 0.2s ease-in-out;
  }
  `}
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

export const BioContainer = styled.div<{
  isEditable?: boolean
  isEmpty?: boolean
}>`
  margin-top: ${({ theme }) => theme.space.xxxsmall};
  margin-left: -${({ theme }) => theme.space.xsmall};
  padding: ${({ theme }) => theme.space.xxsmall}
    ${({ theme }) => theme.space.xsmall};
  border-radius: ${({ theme }) => theme.radius.xxsmall};

  .clamp-lines__button {
    margin-top: ${({ theme }) => theme.space.xxsmall};

    font-size: ${rem(16)};
    font-weight: 500;
    color: ${({ theme }) => theme.colors.secondary};

    transition: color 0.2s ease-in-out;

    :hover {
      color: ${({ theme }) => theme.colors.primary};
    }
  }

  ${({ isEmpty }) =>
    isEmpty &&
    `
    color: rgba(100, 100, 100);
  `}

  ${({ isEditable }) =>
    isEditable &&
    `
  cursor: pointer;

  :hover {
    background: rgba(100, 100, 100, 0.2);
    transition: background 0.2s ease-in-out;
  }
  `}
`
