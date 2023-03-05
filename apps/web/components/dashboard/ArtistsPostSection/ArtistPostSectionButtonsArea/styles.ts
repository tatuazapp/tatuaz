import { down } from "styled-breakpoints"
import styled from "styled-components"

export const ArtistsPostSectionButtonAreaWrapper = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;

  width: 100%;
  height: 100px;
`

export const LeftContainer = styled.div`
  display: flex;
  justify-content: space-between;
`

export const TypeButtonsContainer = styled.div`
  display: flex;
  justify-content: space-between;
`

export const TypeButton = styled.button<{
  isSelected: boolean
}>`
  margin-left: ${({ theme }) => theme.sizes.xlarge};
  padding-top: ${({ theme }) => theme.sizes.xxsmall};
  padding-right: ${({ theme }) => theme.sizes.large};
  padding-bottom: ${({ theme }) => theme.sizes.xxsmall};
  padding-left: ${({ theme }) => theme.sizes.large};

  font-size: ${({ theme }) => theme.sizes.small};
  font-weight: 500;
  color: ${({ theme }) => theme.colors.secondary};
  color: ${({ isSelected, theme }) =>
    isSelected ? theme.colors.background1 : theme.colors.secondary};

  background-color: ${({ isSelected, theme }) =>
    isSelected ? theme.colors.primary : theme.colors.background1};
  border: 1px solid;
  border-color: ${({ isSelected, theme }) =>
    isSelected ? theme.colors.primary : theme.colors.secondary};
  border-radius: ${({ theme }) => theme.radius.small};

  ${down("sm")} {
    margin-left: ${({ theme }) => theme.sizes.small};
    padding-right: ${({ theme }) => theme.sizes.small};
    padding-left: ${({ theme }) => theme.sizes.small};
  }
`
