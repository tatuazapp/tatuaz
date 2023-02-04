import styled from "styled-components"
import { rem } from "../../../utils/utils"

export const TopBar = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;

  min-height: ${rem(110)};
  padding: ${({ theme }) => `${theme.space.xlarge} ${theme.space.xxxlarge}`};

  color: black;

  background-color: transparent;
`
