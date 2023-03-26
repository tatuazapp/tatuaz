import { Data as Dashboard } from "@styled-icons/boxicons-regular/Data"
import { Home } from "@styled-icons/boxicons-regular/Home"
import { Menu } from "@styled-icons/boxicons-regular/Menu"
import { User as Profile } from "@styled-icons/boxicons-regular/User"
import { Search } from "@styled-icons/evaicons-solid/Search"
import { SignOut } from "@styled-icons/octicons/SignOut"
import { up } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../../../styles/utils"

export const NarrowMenuWrapper = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-between;

  box-sizing: content-box;
  width: ${({ theme }) => theme.sizes.xxxlarge};
  min-height: 100vh;
  margin-right: ${({ theme }) => theme.space.xlarge};

  background-color: ${({ theme }) => theme.colors.background2};

  ${up("xl")} {
    display: none;
  }
`

export const MenuContainer = styled.div`
  display: flex;
  justify-content: center;

  height: ${rem(116)};
  padding-top: ${({ theme }) => theme.space.xlarge};
  padding-bottom: ${({ theme }) => theme.space.xxlarge};

  font-size: ${({ theme }) => theme.sizes.large};
  font-weight: 600;
  color: ${({ theme }) => theme.colors.secondary};
`

export const NarrowMenuList = styled.div`
  display: flex;
  flex-direction: column;
`

export const NarrowMenuListItem = styled.div<{
  isSelected: boolean
}>`
  cursor: pointer;

  display: flex;
  align-items: center;

  padding-top: ${({ theme }) => theme.space.small};
  padding-right: ${({ theme }) => theme.space.small};
  padding-bottom: ${({ theme }) => theme.space.small};
  padding-left: ${({ theme }) => theme.space.small};

  font-size: ${({ theme }) => theme.sizes.medium};
  font-weight: 500;
  color: ${({ isSelected, theme }) =>
    isSelected ? theme.colors.primary : theme.colors.secondary};
  :hover {
    background-color: ${({ theme }) => theme.colors.background3};
  }
`

export const MenuIcon = styled(Menu)`
  cursor: pointer;
  height: ${({ theme }) => theme.sizes.xlarge};
`

export const HomeIcon = styled(Home)`
  height: ${({ theme }) => theme.sizes.xlarge};
`

export const SearchIcon = styled(Search)`
  height: ${({ theme }) => theme.sizes.xlarge};
`
export const DashboardIcon = styled(Dashboard)`
  height: ${({ theme }) => theme.sizes.xlarge};
`

export const ProfileIcon = styled(Profile)`
  height: ${({ theme }) => theme.sizes.xlarge};
`

export const NarrowMenuSignOutIcon = styled(SignOut)`
  cursor: pointer;

  box-sizing: content-box;
  height: ${({ theme }) => theme.sizes.xlarge};
  margin-right: auto;
  margin-bottom: ${({ theme }) => theme.space.xxxxlarge};
  margin-left: auto;
  padding: ${({ theme }) => theme.space.xxsmall};

  color: ${({ theme }) => theme.colors.background1};

  background-color: ${({ theme }) => theme.colors.primary};
  border-radius: ${({ theme }) => theme.radius.medium};
`
