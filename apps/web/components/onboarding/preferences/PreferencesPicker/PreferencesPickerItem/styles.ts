import { Check2 } from "@styled-icons/bootstrap/Check2"
import { down } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../../../../styles/utils"

export const IconWrapper = styled.div<{
  imageUrl: string
  checked?: boolean
}>`
  cursor: pointer;

  min-width: ${rem(160)};
  min-height: ${rem(160)};

  background: url(${({ imageUrl }) => imageUrl}) no-repeat center center;
  background-size: cover;
  border-radius: 50%;

  transition: all 0.3s ease-in-out;

  &:hover {
    transform: scale(1.1);
  }

  ${down("sm")} {
    min-width: ${rem(100)};
    min-height: ${rem(100)};
  }

  ${({ checked, imageUrl }) =>
    checked &&
    `
    background: linear-gradient(
      rgba(0, 0, 0, 0.5), 
      rgba(0, 0, 0, 0.5)
    ),
    url(${imageUrl}) no-repeat center center;
    background-size: cover;
    background-blend-mode: darken;

    position: relative;
    `}
`

export const PreferencesPickerItemWrapper = styled.div`
  display: flex;
  flex-direction: column;
  gap: ${({ theme }) => theme.space.xsmall};
  align-items: center;
  justify-content: center;
`
export const CheckIcon = styled(Check2)`
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);

  height: 70%;

  color: ${({ theme }) => theme.colors.primary};
`
