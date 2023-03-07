import { Paragraph, Paragraph1 } from "@tatuaz/ui"
import { useState } from "react"
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

const Menu = () => {
  const [activeTab, setActiveTab] = useState("Home")

  const tabs = ["Home", "Search", "Dashboard", "Profile"]

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
                <Paragraph1>{tab}</Paragraph1>
              </MenuListItemText>
            </MenuListItem>
          ))}
        </MenuList>
      </div>
      <SignOutButton>
        <Paragraph color={theme.colors.background1} level={1}>
          Sign out
        </Paragraph>
      </SignOutButton>
    </MenuWrapper>
  )
}

export default Menu
