import { Paragraph, Paragraph1 } from "@tatuaz/ui"
import { useState } from "react"
import { FormattedMessage } from "react-intl"
import { theme } from "../../../../styles/theme"
import { Tabs } from "../../../../types/tabs"
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

const tabs: Tabs[] = ["Home", "Search", "Dashboard", "Profile"]

const Menu = () => {
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
              {render(tab)}
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
