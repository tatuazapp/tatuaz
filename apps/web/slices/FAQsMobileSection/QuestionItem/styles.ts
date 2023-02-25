// import { down } from "styled-breakpoints"
import { ArrowDownRightCircle } from "@styled-icons/bootstrap/ArrowDownRightCircle"
import styled from "styled-components"

export const MobileQuestionItemWrapper = styled.div<{
  // isSelected: boolean
  isHovered?: boolean
}>`
  display: flex;
  align-items: flex-start;
  justify-content: space-between;

  width: 100%;
  padding-top: ${({ theme }) => theme.space.xxsmall};
  padding-right: ${({ theme }) => theme.space.small};
  padding-bottom: ${({ theme }) => theme.space.large};
  padding-left: ${({ theme }) => theme.space.small};

  color: ${({ theme }) => theme.colors.secondary};

  background-color: ${({ isHovered, theme }) =>
    isHovered ? theme.colors.background2 : theme.colors.background1};
`

export const MobileQuestionTitleWrapper = styled.div`
  display: flex;
`

export const MobileQuestionNumber = styled.div<{
  // isSelected: boolean
  isHovered: boolean
}>`
  /* width: 15%; */
  min-width: ${({ theme }) => theme.sizes.xxlarge};
  font-size: ${({ theme }) => theme.sizes.large};
  font-weight: 600;
  color: ${({ isHovered, theme }) =>
    isHovered ? theme.colors.primary : theme.colors.background3};
`

export const MobileQuestionTitle = styled.div<{
  // isSelected: boolean
  isHovered: boolean
}>`
  /* width: 60%; */
  padding-right: ${({ theme }) => theme.space.xlarge};

  font-size: ${({ theme }) => theme.sizes.large};
  font-weight: 900;
  color: ${({ isHovered, theme }) =>
    isHovered ? theme.colors.secondary : theme.colors.background3};
  text-align: left;
`

export const MobileQuestionContent = styled.div<{
  // isSelected: boolean
  isHovered: boolean
}>`
  padding-right: ${({ theme }) => theme.space.xxsmall};
  padding-bottom: ${({ theme }) => theme.space.large};
  padding-left: ${({ theme }) => theme.space.xxlarge};

  font-weight: 500;
  color: ${({ isHovered, theme }) =>
    isHovered ? theme.colors.secondary : theme.colors.background3};
  text-align: justify;

  background-color: ${({ isHovered, theme }) =>
    isHovered ? theme.colors.background2 : theme.colors.background1};
`

export const MobileIconNotSelected = styled(ArrowDownRightCircle)`
  align-self: center;
  height: 60px;
  padding-left: ${({ theme }) => theme.space.xxxxlarge};
  color: ${({ theme }) => theme.colors.background3};
`

export const MobileIconSelected = styled(ArrowDownRightCircle)`
  align-self: center;
  height: 60px;
  padding-left: ${({ theme }) => theme.space.xxxxlarge};
  color: ${({ theme }) => theme.colors.primary};
`

export const MobileDivider = styled.div<{
  noMargin: boolean
}>`
  /* height: ${({ theme }) => theme.sizes.xxxxsmall}; */
  height: 1px;
  margin-right: ${({ theme }) => theme.space.small};
  margin-left: ${({ theme }) => theme.space.small};

  visibility: ${({ noMargin }) => (noMargin ? "hidden" : "visible")};
  background-color: ${({ theme }) => theme.colors.background3};
`
