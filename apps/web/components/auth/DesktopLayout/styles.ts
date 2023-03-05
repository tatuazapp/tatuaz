import { MenuApp } from "@styled-icons/bootstrap/MenuApp"
import { down } from "styled-breakpoints"
import styled from "styled-components"

export const DesktopLayoutContainer = styled.div`
  display: flex;
  width: 100%;
`

export const DrawerViewContainer = styled.div`
  width: 50px;
`

export const MenuIcon = styled(MenuApp)`
  height: ${({ theme }) => theme.space.large};
  color: ${({ theme }) => theme.colors.primary};

  :hover {
    cursor: pointer;
  }
`

export const MobileLayoutContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 100%;

  ${down("lg")} {
    padding-right: ${({ theme }) => theme.space.small};
    padding-left: ${({ theme }) => theme.space.small};
  }
`

export const MobileLayoutHeader = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  /* margin-top: ${({ theme }) => theme.space.large}; */
  width: 100%;
  min-width: 250px;
  max-width: 735px;
`

export const WordmarkWrapper = styled.div`
  display: flex;
  justify-content: start;
  align-items: center;
  height: 116px;

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
  height: ${({ theme }) => theme.space.xlarge};
  color: ${({ theme }) => theme.colors.primary};

  :hover {
    cursor: pointer;
  }
`
