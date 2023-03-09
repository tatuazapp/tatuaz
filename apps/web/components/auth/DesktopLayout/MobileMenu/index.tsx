import { Paragraph, Paragraph1 } from "@tatuaz/ui"
import { useState } from "react"
import { FormattedMessage } from "react-intl"
import { theme } from "../../../../styles/theme"
import { Tabs } from "../../../../types/tabs"
import {
  GreenWrapper,
  MobileMenuList,
  MobileMenuListItem,
  HomeIcon,
  MenuListItemText,
  MobileMenuWrapper,
  WordmarkWrapper,
  SearchIcon,
  DashboardIcon,
  ProfileIcon,
  SignOutButton,
  CloseMenuIcon,
  TopMobileMenuWrapper,
  MobileMenuBackgroundContainer,
} from "./styles"

type MobileMenuProps = {
  onClose: () => void
}
const tabs: Tabs[] = ["Home", "Search", "Dashboard", "Profile"]

const MobileMenu: React.FunctionComponent<MobileMenuProps> = ({ onClose }) => {
  const [activeTab, setActiveTab] = useState<Tabs>("Home")

  const render = (tab: Tabs) => {
    switch (tab) {
      case "Home":
        return (
          <>
            <HomeIcon />
            <MenuListItemText>
              <Paragraph1>
                <FormattedMessage defaultMessage="Strona główna" id="8GchbR" />
              </Paragraph1>
            </MenuListItemText>
          </>
        )
      case "Search":
        return (
          <>
            <SearchIcon />
            <MenuListItemText>
              <Paragraph1>
                <FormattedMessage defaultMessage="Wyszukiwanie" id="QEcCbg" />
              </Paragraph1>
            </MenuListItemText>
          </>
        )
      case "Dashboard":
        return (
          <>
            <DashboardIcon />
            <MenuListItemText>
              <Paragraph1>
                <FormattedMessage defaultMessage="Panel" id="hIxZtX" />
              </Paragraph1>
            </MenuListItemText>
          </>
        )
      case "Profile":
        return (
          <>
            <ProfileIcon />
            <MenuListItemText>
              <Paragraph1>
                <FormattedMessage defaultMessage="Profil" id="n7oiI/" />
              </Paragraph1>
            </MenuListItemText>
          </>
        )
      default:
        return "Error"
    }
  }
  return (
    <MobileMenuBackgroundContainer>
      <MobileMenuWrapper>
        <div>
          <TopMobileMenuWrapper>
            <WordmarkWrapper>
              Tatuaz<GreenWrapper>App</GreenWrapper>
            </WordmarkWrapper>
            <CloseMenuIcon onClick={onClose} />
          </TopMobileMenuWrapper>
          <MobileMenuList>
            {tabs.map((tab) => (
              <MobileMenuListItem
                key={tab}
                isSelected={activeTab === tab}
                onClick={() => {
                  setActiveTab(tab)
                }}
              >
                {render(tab)}
              </MobileMenuListItem>
            ))}
          </MobileMenuList>
        </div>
        <SignOutButton>
          <Paragraph color={theme.colors.background1} level={1} strong={true}>
            <FormattedMessage defaultMessage="Wyloguj" id="UqV7Od" />
          </Paragraph>
        </SignOutButton>
      </MobileMenuWrapper>
    </MobileMenuBackgroundContainer>
  )
}

export default MobileMenu
