// import { down } from "styled-breakpoints"
import { ArrowDownRightCircle } from "@styled-icons/bootstrap/ArrowDownRightCircle"
import styled from "styled-components"

export const QuestionItemWrapper = styled.div`
  display: flex;
  align-items: flex-start;
  justify-content: space-between;

  width: 100%;
  padding-bottom: ${({ theme }) => theme.space.large};
  /* padding-top: ${({ theme }) => theme.space.xxsmall}; */
  color: ${({ theme }) => theme.colors.secondary};
`

export const QuestionTitleWrapper = styled.div`
  display: flex;
`

export const QuestionNumber = styled.div<{
  isSelected: boolean
  isHovered: boolean
}>`
  /* width: 15%; */
  min-width: ${({ theme }) => theme.sizes.xxlarge};
  font-size: ${({ theme }) => theme.sizes.large};
  font-weight: 600;
  color: ${({ isSelected, isHovered, theme }) =>
    isSelected || isHovered ? theme.colors.primary : theme.colors.secondary};
`

export const QuestionTitle = styled.div<{
  isSelected: boolean
  isHovered: boolean
}>`
  /* width: 60%; */
  padding-right: ${({ theme }) => theme.space.xlarge};

  font-size: ${({ theme }) => theme.sizes.large};
  font-weight: 900;
  color: ${({ isSelected, isHovered, theme }) =>
    isSelected || isHovered ? theme.colors.secondary : theme.colors.secondary};
  text-align: left;
`

export const QuestionContent = styled.div<{
  isSelected: boolean
  isHovered: boolean
}>`
  padding-bottom: ${({ theme }) => theme.space.large};
  padding-left: ${({ theme }) => theme.space.xxlarge};
  padding-right: ${({ theme }) => theme.space.xxsmall};

  font-weight: 500;
  color: ${({ isSelected, isHovered, theme }) =>
    isSelected || isHovered ? theme.colors.secondary : theme.colors.secondary};
  text-align: justify;
`

export const IconNotSelected = styled(ArrowDownRightCircle)`
  align-self: center;
  height: 60px;
  padding-left: ${({ theme }) => theme.space.xxxxlarge};
`
export const Divider = styled.div<{
  noMargin: boolean
}>`
  /* height: ${({ theme }) => theme.sizes.xxxxsmall}; */
  height: 1px;
  visibility: ${({ noMargin }) => (noMargin ? "hidden" : "visible")};
  background-color: ${({ theme }) => theme.colors.background3};
`
