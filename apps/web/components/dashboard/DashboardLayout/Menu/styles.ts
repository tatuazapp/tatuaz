import { CalendarDate } from "@styled-icons/bootstrap"
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
  border-radius: 0 12px 12px 0;

  ${down("xxl")} {
    margin-right: ${({ theme }) => theme.space.xxlarge};
  }

  ${down("xl")} {
    display: none;
  }

  ${up("xxxl")} {
    position: fixed;
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
  gap: ${({ theme }) => theme.space.xsmall};
`

export const MenuListItem = styled.div<{
  isSelected: boolean
}>`
  cursor: pointer;

  display: flex;
  align-items: center;

  margin: 0 ${({ theme }) => theme.space.xsmall};
  padding: ${({ theme }) => theme.space.xsmall}
    ${({ theme }) => theme.space.xsmall};

  color: ${({ isSelected, theme }) =>
    isSelected ? theme.colors.primary : theme.colors.secondary};

  background-color: ${({ isSelected, theme }) =>
    isSelected ? theme.colors.background3 : "transparent"};
  border-radius: 8px;

  transition: background-color 0.3s;

  :hover {
    background-color: ${({ theme }) => theme.colors.background3};
  }
`

export const HomeIcon = styled(Home)`
  width: ${rem(28)};
  height: ${rem(28)};
`

export const SearchIcon = styled(Search)`
  width: ${rem(28)};
  height: ${rem(28)};
`

export const DashboardIcon = styled(Dashboard)`
  width: ${rem(28)};
  height: ${rem(28)};
`

export const ProfileIcon = styled(Profile)`
  width: ${rem(28)};
  height: ${rem(28)};
`

export const CalendarIcon = styled(CalendarDate)`
  width: ${rem(28)};
  height: ${rem(28)};
  margin-right: ${({ theme }) => theme.space.xxxxsmall};
  padding: ${({ theme }) => theme.space.xxxxsmall};
`

export const MenuListItemText = styled.p`
  padding-left: ${({ theme }) => theme.space.xxsmall};
`

export const SignOutButtonWrapper = styled.div`
  max-width: 150px;
  margin-right: auto;
  margin-bottom: ${({ theme }) => theme.space.xxxxlarge};
  margin-left: auto;
`
