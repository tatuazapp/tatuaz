import { Paragraph1 } from "@tatuaz/ui"
import Link from "next/link"
import { useRouter } from "next/router"
import { useState } from "react"
import { FormattedMessage } from "react-intl"
import { SignOutButton } from "../../../auth/SignoutButton"
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
  CloseMenuIcon,
  TopMobileMenuWrapper,
  MobileMenuBackgroundContainer,
} from "./styles"

type MobileMenuProps = {
  onClose: () => void
}

const tabs = [
  {
    name: <FormattedMessage defaultMessage="Strona główna" id="8GchbR" />,
    href: "/dashboard/",
    id: "home",
  },
  {
    name: <FormattedMessage defaultMessage="Wyszukiwanie" id="QEcCbg" />,
    href: "/dashboard/search",
    id: "search",
  },
  {
    name: <FormattedMessage defaultMessage="Panel" id="hIxZtX" />,
    href: "/dashboard/dashboard",
    id: "dashboard",
  },
  {
    name: <FormattedMessage defaultMessage="Profil" id="n7oiI/" />,
    href: "/dashboard/profile",
    id: "profile",
  },
]

const MobileMenu: React.FunctionComponent<MobileMenuProps> = ({ onClose }) => {
  const router = useRouter()
  const currentTab = router.pathname.split("/")[2] || "home"
  const [activeTab, setActiveTab] = useState(currentTab)

  const render = (tab: { name: JSX.Element; href: string; id: string }) => {
    const tabInfo = tabs.find((t) => t.id === tab.id)
    if (!tabInfo) return "Error"

    const Icon = () => {
      switch (tab.id) {
        case "home":
          return <HomeIcon />
        case "search":
          return <SearchIcon />
        case "dashboard":
          return <DashboardIcon />
        case "profile":
          return <ProfileIcon />
        default:
          return null
      }
    }

    return (
      <MobileMenuListItem
        key={tab.id}
        href={tabInfo.href}
        isSelected={activeTab === tab.id}
        onClick={() => {
          setActiveTab(tab.id)
          onClose()
        }}
      >
        <Icon />
        <MenuListItemText>
          <Paragraph1>{tabInfo.name}</Paragraph1>
        </MenuListItemText>
      </MobileMenuListItem>
    )
  }

  return (
    <MobileMenuBackgroundContainer>
      <MobileMenuWrapper>
        <div>
          <TopMobileMenuWrapper>
            <Link href="/">
              <WordmarkWrapper>
                Tatuaz<GreenWrapper>App</GreenWrapper>
              </WordmarkWrapper>
            </Link>
            <CloseMenuIcon onClick={onClose} />
          </TopMobileMenuWrapper>
          <MobileMenuList>{tabs.map((tab) => render(tab))}</MobileMenuList>
        </div>
        <SignOutButton mb={10} />
      </MobileMenuWrapper>
    </MobileMenuBackgroundContainer>
  )
}

export default MobileMenu
