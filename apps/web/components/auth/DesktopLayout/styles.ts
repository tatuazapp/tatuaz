import { MenuApp } from "@styled-icons/bootstrap/MenuApp"
import { down } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../../styles/utils"

export const DesktopLayoutContainer = styled.div`
  display: flex;
`

export const MenuIcon = styled(MenuApp)`
  cursor: pointer;
  height: ${({ theme }) => theme.space.large};
  color: ${({ theme }) => theme.colors.primary};
`

export const MobileLayoutContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;

  ${down("lg")} {
    padding-right: ${({ theme }) => theme.space.small};
    padding-left: ${({ theme }) => theme.space.small};
  }
`

export const MobileLayoutHeader = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;

  width: 100%;
  min-width: 250px;
  max-width: 735px;
`

export const WordmarkWrapper = styled.div`
  display: flex;
  align-items: center;
  justify-content: start;

  height: ${rem(116)};

  font-size: ${({ theme }) => theme.sizes.xlarge};
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

export const MobileMenuIcon = styled(MenuApp)`
  cursor: pointer;
  height: ${({ theme }) => theme.space.xlarge};
  color: ${({ theme }) => theme.colors.primary};
`
