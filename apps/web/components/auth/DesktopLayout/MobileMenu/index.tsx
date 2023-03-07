import { useState } from "react"
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

const MobileMenu: React.FunctionComponent<MobileMenuProps> = ({ onClose }) => {
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
                {renderIcon(tab)}
                <MenuListItemText>{tab}</MenuListItemText>
              </MobileMenuListItem>
            ))}
          </MobileMenuList>
        </div>
        <SignOutButton>Sign out</SignOutButton>
      </MobileMenuWrapper>
    </MobileMenuBackgroundContainer>
  )
}

export default MobileMenu
