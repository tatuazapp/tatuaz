import { down } from "styled-breakpoints"
import styled from "styled-components"

export const SearchButtonsWrapper = styled.div`
  display: flex;
  margin-top: ${({ theme }) => theme.sizes.xxxlarge};
  margin-bottom: ${({ theme }) => theme.sizes.xxlarge};

  ${down("lg")} {
    margin-top: ${({ theme }) => theme.sizes.xlarge};
    margin-bottom: ${({ theme }) => theme.sizes.xlarge};
  }

  ${down("sm")} {
    justify-content: space-between;
    margin-top: ${({ theme }) => theme.sizes.xlarge};
    margin-bottom: ${({ theme }) => theme.sizes.xlarge};
  }

  ${down("xs")} {
    flex-direction: column;
  }
`

export const TypeButton = styled.button<{
  isSelected: boolean
}>`
  margin-right: ${({ theme }) => theme.sizes.xlarge};
  padding-top: ${({ theme }) => theme.sizes.xxsmall};
  padding-right: ${({ theme }) => theme.sizes.large};
  padding-bottom: ${({ theme }) => theme.sizes.xxsmall};
  padding-left: ${({ theme }) => theme.sizes.large};

  background-color: ${({ isSelected, theme }) =>
    isSelected ? theme.colors.primary : theme.colors.background1};
  border: 1px solid;
  border-color: ${({ isSelected, theme }) =>
    isSelected ? theme.colors.primary : theme.colors.secondary};
  border-radius: ${({ theme }) => theme.radius.small};

  ${down("sm")} {
    margin-right: 0;
    padding-right: ${({ theme }) => theme.sizes.small};
    padding-left: ${({ theme }) => theme.sizes.small};
  }
`
