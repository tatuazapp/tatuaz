import { Paragraph, Paragraph1 } from "@tatuaz/ui"
import { useState } from "react"
import { FormattedMessage } from "react-intl"
import { theme } from "../../../../styles/theme"
import {
  GreenWrapper,
  MenuList,
  MenuListItem,
  HomeIcon,
  MenuListItemText,
  MenuWrapper,
  WordmarkWrapper,
  SearchIcon,
  DashboardIcon,
  ProfileIcon,
  SignOutButton,
} from "./styles"

type tabsType = "Home" | "Search" | "Dashboard" | "Profile"
const tabs: tabsType[] = ["Home", "Search", "Dashboard", "Profile"]

const Menu = () => {
  const [activeTab, setActiveTab] = useState<
    "Home" | "Search" | "Dashboard" | "Profile"
  >("Home")

  const renderIcon = (tab: string) => {
    switch (tab) {
      case "Home":
        return <HomeIcon />
      case "Search":
        return <SearchIcon />
      case "Dashboard":
        return <DashboardIcon />
      case "Profile":
        return <ProfileIcon />
      default:
        return "Error"
    }
  }
  const renderTabName = (tab: string) => {
    switch (tab) {
      case "Home":
        return <FormattedMessage defaultMessage="Strona główna" id="8GchbR" />
      case "Search":
        return <FormattedMessage defaultMessage="Wyszukiwanie" id="QEcCbg" />
      case "Dashboard":
        return <FormattedMessage defaultMessage="Panel" id="hIxZtX" />
      case "Profile":
        return <FormattedMessage defaultMessage="Profil" id="n7oiI/" />
      default:
        return "Error"
    }
  }

  return (
    <MenuWrapper>
      <div>
        <WordmarkWrapper>
          Tatuaz<GreenWrapper>App</GreenWrapper>
        </WordmarkWrapper>
        <MenuList>
          {tabs.map((tab) => (
            <MenuListItem
              key={tab}
              isSelected={activeTab === tab}
              onClick={() => {
                setActiveTab(tab)
              }}
            >
              {renderIcon(tab)}
              <MenuListItemText>
                <Paragraph1>{renderTabName(tab)}</Paragraph1>
              </MenuListItemText>
            </MenuListItem>
          ))}
        </MenuList>
      </div>
      <SignOutButton>
        <Paragraph color={theme.colors.background1} level={1}>
          <FormattedMessage defaultMessage="Wyloguj" id="UqV7Od" />
        </Paragraph>
      </SignOutButton>
    </MenuWrapper>
  )
}

export default Menu
