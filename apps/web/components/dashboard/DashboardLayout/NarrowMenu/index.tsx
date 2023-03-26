import { useRouter } from "next/router"
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
  NarrowMenuSignOutIcon,
} from "./styles"

type NarrowMenuProps = {
  onOpen: () => void
}

const tabs = [
  {
    name: "home",
    href: "/dashboard/",
  },
  {
    name: "search",
    href: "/dashboard/search",
  },
  {
    name: "dashboard",
    href: "/dashboard/dashboard",
  },
  {
    name: "profile",
    href: "/dashboard/profile",
  },
]

const NarrowMenu: React.FunctionComponent<NarrowMenuProps> = ({ onOpen }) => {
  const router = useRouter()
  const currentTab = router.pathname.split("/")[2] || "home"
  const [activeTab, setActiveTab] = useState(currentTab)

  const renderIcon = (tab: string) => {
    switch (tab) {
      case "home":
        return <HomeIcon />
      case "search":
        return <SearchIcon />
      case "dashboard":
        return <DashboardIcon />
      case "profile":
        return <ProfileIcon />
      default:
        return "Error"
    }
  }

  const handleTabClick = (tab: string) => {
    const tabInfo = tabs.find((t) => t.name === tab)
    if (tabInfo) {
      setActiveTab(tab)
      router.push(tabInfo.href)
    }
  }

  return (
    <NarrowMenuWrapper>
      <div>
        <MenuContainer>
          <MenuIcon onClick={onOpen} />
        </MenuContainer>
        <NarrowMenuList>
          {tabs.map((tab) => (
            <NarrowMenuListItem
              key={tab.name}
              isSelected={activeTab === tab.name}
              onClick={() => handleTabClick(tab.name)}
            >
              {renderIcon(tab.name)}
            </NarrowMenuListItem>
          ))}
        </NarrowMenuList>
      </div>
      <NarrowMenuSignOutIcon />
    </NarrowMenuWrapper>
  )
}

export default NarrowMenu
