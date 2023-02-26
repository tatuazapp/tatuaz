import { ArrowDownRightCircle } from "@styled-icons/bootstrap/ArrowDownRightCircle"
import { ArrowDownRightCircleFill } from "@styled-icons/bootstrap/ArrowDownRightCircleFill"
import styled from "styled-components"
import { rem } from "../../../styles/utils"

export const QuestionItemWrapper = styled.div<{
  isHovered?: boolean
}>`
  display: flex;
  align-items: flex-start;

  min-height: ${rem(165)};
  padding-top: ${({ theme }) => theme.space.xlarge};
  padding-right: ${({ theme }) => theme.space.xxxxlarge};
  padding-bottom: ${({ theme }) => theme.space.xlarge};
  padding-left: ${({ theme }) => theme.space.xxxxlarge};

  color: ${({ theme }) => theme.colors.background3};

  background-color: ${({ isHovered, theme }) =>
    isHovered ? theme.colors.background2 : theme.colors.background1};
`

export const Divider = styled.div`
  height: 1px;
  margin-right: ${({ theme }) => theme.space.xxxxlarge};
  margin-left: ${({ theme }) => theme.space.xxxxlarge};
  background-color: ${({ theme }) => theme.colors.background3};
`

export const QuestionNumber = styled.div<{
  isHovered?: boolean
}>`
  width: 6%;
  min-width: ${rem(46)};

  font-size: ${({ theme }) => theme.sizes.xlarge};
  font-weight: 600;
  color: ${({ isHovered, theme }) =>
    isHovered ? theme.colors.primary : theme.colors.background3};
`

export const QuestionTitle = styled.div<{
  isHovered?: boolean
}>`
  width: 33%;
  padding-right: ${({ theme }) => theme.space.xxxxlarge};

  font-size: ${({ theme }) => theme.sizes.xlarge};
  font-weight: 900;
  color: ${({ isHovered, theme }) =>
    isHovered ? theme.colors.secondary : theme.colors.background3};
`

export const QuestionContent = styled.div<{
  isHovered?: boolean
}>`
  width: 42%;
  padding-right: ${({ theme }) => theme.space.xxxxlarge};
  padding-left: ${({ theme }) => theme.space.xlarge};

  font-weight: 500;
  color: ${({ isHovered, theme }) =>
    isHovered ? theme.colors.secondary : theme.colors.background3};
  text-align: justify;
`

export const IconNotSelected = styled(ArrowDownRightCircle)`
  height: 101px;
  padding-left: ${({ theme }) => theme.space.xxxxlarge};
`

export const IconSelected = styled(ArrowDownRightCircleFill)`
  height: 101px;
  padding-left: ${({ theme }) => theme.space.xxxxlarge};
  color: ${({ theme }) => theme.colors.primary};
`
