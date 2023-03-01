import { useState } from "react"
import {
  NarrowMenuList,
  NarrowMenuListItem,
  HomeIcon,
  NarrowMenuWrapper,
  SearchIcon,
  DashboardIcon,
  ProfileIcon,
  MenuIcon,
  MenuContainer,
} from "./styles"

interface NarrowMenuProps {
  onOpen: any
}

const NarrowMenu: React.FC<NarrowMenuProps> = (props) => {
  const { onOpen } = props
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
    <NarrowMenuWrapper>
      <MenuContainer>
        <MenuIcon onClick={onOpen} />
      </MenuContainer>

      <NarrowMenuList>
        {tabs.map((tab) => (
          <NarrowMenuListItem
            key={tab}
            isSelected={activeTab === tab}
            onClick={() => {
              setActiveTab(tab)
            }}
          >
            {renderIcon(tab)}
          </NarrowMenuListItem>
        ))}
      </NarrowMenuList>
    </NarrowMenuWrapper>
  )
}

export default NarrowMenu
