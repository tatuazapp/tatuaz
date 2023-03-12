import { Data as Dashboard } from "@styled-icons/boxicons-regular/Data"
import { Home } from "@styled-icons/boxicons-regular/Home"
import { User as Profile } from "@styled-icons/boxicons-regular/User"
import { Search } from "@styled-icons/evaicons-solid/Search"
import { down, up } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../../../styles/utils"

export const MenuWrapper = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-between;

  box-sizing: content-box;
  min-width: 200px;
  max-width: 200px;
  min-height: 100vh;
  margin-right: ${({ theme }) => theme.space.xxxxlarge};

  background-color: ${({ theme }) => theme.colors.background2};
  ${down("xxl")} {
    margin-right: ${({ theme }) => theme.space.xxlarge};
  }

  ${up("xxxl")} {
    position: absolute;
    left: 0;
  }
`

export const WordmarkWrapper = styled.div`
  display: flex;
  justify-content: center;

  height: ${rem(116)};
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

export const SignOutButtonWrapper = styled.div`
  max-width: 150px;
  margin-right: auto;
  margin-bottom: ${({ theme }) => theme.space.xxxxlarge};
  margin-left: auto;
`
