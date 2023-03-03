import { Data as Dashboard } from "@styled-icons/boxicons-regular/Data"
import { Home } from "@styled-icons/boxicons-regular/Home"
import { User as Profile } from "@styled-icons/boxicons-regular/User"
import { CloseOutline } from "@styled-icons/evaicons-outline/CloseOutline"
import { Search } from "@styled-icons/evaicons-solid/Search"
import styled from "styled-components"

export const MobileMenuBackgroundContainer = styled.div`
  width: 100%;
  height: 100vh;
  display: flex;
  justify-content: center;

  background-color: ${({ theme }) => theme.colors.background2};
`

export const MobileMenuWrapper = styled.div`
  position: relative;

  box-sizing: content-box;
  max-width: 735px;
  width: 100%;
  height: 100vh;
  padding-right: ${({ theme }) => theme.space.small};
  padding-left: ${({ theme }) => theme.space.small};

  background-color: ${({ theme }) => theme.colors.background2};
`

export const TopMobileMenuWrapper = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
`

export const CloseMenuIcon = styled(CloseOutline)`
  height: ${({ theme }) => theme.sizes.xlarge};
  color: ${({ theme }) => theme.colors.secondary};
`

export const WordmarkWrapper = styled.div`
  display: flex;
  justify-content: center;

  height: 116px;
  padding-top: ${({ theme }) => theme.space.xlarge};
  padding-bottom: ${({ theme }) => theme.space.xlarge};

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

export const MobileMenuList = styled.div`
  display: flex;
  flex-direction: column;
`

export const MobileMenuListItem = styled.div<{
  isSelected: boolean
}>`
  display: flex;
  align-items: center;

  width: 100%;
  height: 50px;
  margin-bottom: ${({ theme }) => theme.space.medium};
  padding-right: ${({ theme }) => theme.space.small};
  padding-left: ${({ theme }) => theme.space.xxxsmall};

  font-size: ${({ theme }) => theme.sizes.medium};
  font-weight: 500;
  color: ${({ isSelected, theme }) =>
    isSelected ? theme.colors.primary : theme.colors.secondary};
  :hover {
    cursor: pointer;
    background-color: ${({ theme }) => theme.colors.background3};
  }

  /* position: relative;
  animation-name: example;
  animation-duration: 4s;

  @keyframes example {
    0% {
      right: 0px;
    }
    100% {
      left: 200px;
    }
  } */
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

export const SignOutButton = styled.button`
  position: absolute;
  right: 0;
  bottom: 100px;
  left: 0;

  max-width: 150px;
  margin-right: auto;
  margin-left: auto;
  padding: ${({ theme }) => theme.space.xsmall};

  font-size: ${({ theme }) => theme.sizes.medium};
  font-weight: 600;
  color: ${({ theme }) => theme.colors.background1};
  text-align: center;

  background-color: ${({ theme }) => theme.colors.primary};
  border-radius: ${({ theme }) => theme.radius.medium};
`
