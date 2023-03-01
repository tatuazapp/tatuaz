import { Data as Dashboard } from "@styled-icons/boxicons-regular/Data"
import { Home } from "@styled-icons/boxicons-regular/Home"
import { User as Profile } from "@styled-icons/boxicons-regular/User"
import { Search } from "@styled-icons/evaicons-solid/Search"
import { down } from "styled-breakpoints"
import styled from "styled-components"

export const MenuWrapper = styled.div`
  box-sizing: content-box;
  min-width: 200px;
  max-width: 200px;
  height: 100vh;
  margin-right: ${({ theme }) => theme.space.xxxxlarge};

  background-color: ${({ theme }) => theme.colors.background2};

  /* ${down("md")} {
    padding-right: ${({ theme }) => theme.space.xlarge};
    padding-left: ${({ theme }) => theme.space.xlarge};
  }

  ${down("sm")} {
    flex-direction: column;
    justify-content: center;
    padding-top: ${({ theme }) => theme.space.xxxlarge};
    padding-bottom: ${({ theme }) => theme.space.xxxlarge};
  } */
`

export const WordmarkWrapper = styled.div`
  display: flex;
  justify-content: center;
  height: 116px;

  padding-top: ${({ theme }) => theme.space.xlarge};
  padding-bottom: ${({ theme }) => theme.space.xxlarge};

  font-size: ${({ theme }) => theme.sizes.large};
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

export const MenuList = styled.div`
  display: flex;
  flex-direction: column;
`

export const MenuListItem = styled.div<{
  isSelected: boolean
}>`
  display: flex;
  align-items: center;

  width: 100%;
  height: 50px;
  padding-right: ${({ theme }) => theme.space.small};
  padding-left: ${({ theme }) => theme.space.small};
  margin-bottom: ${({ theme }) => theme.space.medium};

  color: ${({ isSelected, theme }) =>
    isSelected ? theme.colors.primary : theme.colors.secondary};

  font-size: ${({ theme }) => theme.sizes.medium};
  font-weight: 500;
  :hover {
    background-color: ${({ theme }) => theme.colors.background3};
    cursor: pointer;
  }
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

export const MenuListItemText = styled.p`
  padding-left: ${({ theme }) => theme.space.xxsmall};
  font-size: ${({ theme }) => theme.sizes.medium};
`
