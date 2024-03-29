import { Data as Dashboard } from "@styled-icons/boxicons-regular/Data"
import { Home } from "@styled-icons/boxicons-regular/Home"
import { User as Profile } from "@styled-icons/boxicons-regular/User"
import { CloseOutline } from "@styled-icons/evaicons-outline/CloseOutline"
import { Search } from "@styled-icons/evaicons-solid/Search"
import Link from "next/link"
import styled, { css } from "styled-components"
import { rem } from "../../../../styles/utils"

const incommingEffect = css`
  position: relative;
  animation-name: incommingEffect;
  animation-duration: ${({ theme }) => theme.animationTime.medium};

  @keyframes incommingEffect {
    0% {
      left: 100%;
      opacity: 0;
    }

    100% {
      left: 0%;
      opacity: 1;
    }
  }
`

export const MobileMenuBackgroundContainer = styled.div`
  display: flex;
  justify-content: center;
  height: 100%;
  background-color: ${({ theme }) => theme.colors.background2};
`

export const MobileMenuWrapper = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-between;

  box-sizing: content-box;
  width: 100%;
  max-width: 735px;
  height: 100dvh;
  padding-right: ${({ theme }) => theme.space.small};
  padding-left: ${({ theme }) => theme.space.small};

  background-color: ${({ theme }) => theme.colors.background2};
`

export const TopMobileMenuWrapper = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
`

export const CloseMenuIcon = styled(CloseOutline)`
  cursor: pointer;
  height: ${({ theme }) => theme.sizes.xlarge};
  color: ${({ theme }) => theme.colors.secondary};
`

export const WordmarkWrapper = styled.div`
  display: flex;
  justify-content: center;

  height: ${rem(116)};
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

export const MobileMenuListItem = styled(Link)<{
  isSelected: boolean
}>`
  cursor: pointer;

  display: flex;
  align-items: center;

  padding-top: ${({ theme }) => theme.space.small};
  padding-right: ${({ theme }) => theme.space.small};
  padding-bottom: ${({ theme }) => theme.space.small};
  padding-left: ${({ theme }) => theme.space.xxxsmall};

  font-size: ${({ theme }) => theme.sizes.medium};
  font-weight: 500;
  color: ${({ isSelected, theme }) =>
    isSelected ? theme.colors.primary : theme.colors.secondary};

  transition: padding-left ${({ theme }) => theme.animationTime.fast},
    background-color ${({ theme }) => theme.animationTime.xfast};
  :hover {
    padding-left: ${({ theme }) => theme.space.small};
    background-color: ${({ theme }) => theme.colors.background3};
  }
`

export const MenuListItemText = styled.p`
  padding-left: ${({ theme }) => theme.space.xxsmall};
  font-size: ${({ theme }) => theme.sizes.medium};

  ${incommingEffect}
`

export const HomeIcon = styled(Home)`
  height: ${({ theme }) => theme.sizes.xlarge};
  ${incommingEffect}
`

export const SearchIcon = styled(Search)`
  height: ${({ theme }) => theme.sizes.xlarge};
  ${incommingEffect}
`
export const DashboardIcon = styled(Dashboard)`
  height: ${({ theme }) => theme.sizes.xlarge};
  ${incommingEffect}
`

export const ProfileIcon = styled(Profile)`
  height: ${({ theme }) => theme.sizes.xlarge};
  ${incommingEffect}
`

export const SignOutButtonWrapper = styled.div`
  margin-right: auto;
  margin-bottom: ${({ theme }) => theme.space.xxxxlarge};
  margin-left: auto;
`

export const LinkNoStyle = styled(Link)`
  display: flex;
  align-items: center;
  text-decoration: none;
`
