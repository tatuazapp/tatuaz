import styled from "styled-components"
import { rem } from "../../../utils/utils"

export const TopBar = styled.div<{
  padding?: 1 | 2 | 3 | 4 | 5
}>`
  display: flex;
  align-items: center;
  justify-content: space-between;

  min-height: ${rem(110)};
  padding: ${({ padding, theme }) => {
    switch (padding) {
      case 1:
        return `${theme.space.xlarge} ${theme.space.xxxlarge}`
      case 2:
        return `${theme.space.large} ${theme.space.xxlarge}`
      case 3:
        return `${theme.space.small} ${theme.space.xlarge}`
      case 4:
        return `${theme.space.xsmall} ${theme.space.large}`
      case 5:
        return `${theme.space.xxsmall} ${theme.space.medium}`
      default:
        return `${theme.space.xlarge} ${theme.space.xxxlarge}`
    }
  }};

  color: black;

  background-color: transparent;
`
