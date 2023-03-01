import { MenuApp } from "@styled-icons/bootstrap/MenuApp"
import styled from "styled-components"

export const DesktopLayoutContainer = styled.div`
  width: 100%;
  display: flex;
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
