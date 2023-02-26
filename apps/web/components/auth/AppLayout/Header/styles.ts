import styled from "styled-components"
import { rem } from "../../../../styles/utils"

export const WordmarkWrapper = styled.div`
  font-size: ${rem(32)};
  font-weight: 600;
  color: ${({ theme }) => theme.colors.secondary};
`

export const GreenWrapper = styled.span`
  color: ${({ theme }) => theme.colors.primary};
`

export const NavItemsWrapper = styled.div`
  display: flex;
  gap: ${({ theme }) => theme.space.xxxlarge};
`
