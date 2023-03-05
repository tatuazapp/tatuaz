import { useState } from "react"
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
              <MenuListItemText>{tab}</MenuListItemText>
            </MenuListItem>
          ))}
        </MenuList>
      </div>
      <SignOutButton>Sign out</SignOutButton>
    </MenuWrapper>
  )
}

export default Menu
