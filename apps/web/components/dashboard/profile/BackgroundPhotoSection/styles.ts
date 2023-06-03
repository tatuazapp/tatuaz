import { IconButton } from "@chakra-ui/react"
import { PenFill } from "@styled-icons/bootstrap"
import { down } from "styled-breakpoints"
import styled from "styled-components"

export const SettingsButton = styled(IconButton)`
  position: absolute;
  z-index: 3;
  top: ${({ theme }) => theme.space.large};
  right: 0;

  float: right;

  margin: 0;
  padding: 0;

  ${down("md")} {
    top: ${({ theme }) => theme.space.xsmall};
    right: ${({ theme }) => theme.space.xxxsmall};
  }
`

export const BookingButton = styled(IconButton)`
  position: absolute;
  z-index: 3;
  top: ${({ theme }) => theme.space.large};

  float: right;

  margin: 0;
  padding: 0;

  ${down("md")} {
    top: ${({ theme }) => theme.space.xsmall};
    right: ${({ theme }) => theme.space.xxxsmall};
  }
`

export const ArtistIcon = styled(PenFill)`
  width: ${({ theme }) => theme.space.medium};
  height: ${({ theme }) => theme.space.medium};
  margin-left: ${({ theme }) => theme.space.xxsmall};
  color: ${({ theme }) => theme.colors.primary};
`
