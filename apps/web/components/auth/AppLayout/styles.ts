import styled from "styled-components"
import { rem } from "../../../styles/utils"

export const WordmarkWrapper = styled.div`
  font-size: ${rem(34)};
  font-weight: 700;
`
export const NavItemsWrapper = styled.div`
  display: flex;
  gap: ${({ theme }) => theme.space.xxxlarge};
`
