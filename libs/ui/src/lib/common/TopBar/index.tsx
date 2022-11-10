import styled from "styled-components"
import { rem } from "../../../utils/utils"

export const TopBar = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;

  min-height: ${rem(70)};
  padding: ${({ theme }) => theme.space.small};

  color: black;

  background-color: ${({ theme }) => theme.colors.primary};
`
