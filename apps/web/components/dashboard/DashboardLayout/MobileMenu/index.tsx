import { Paragraph, Paragraph1 } from "@tatuaz/ui"
import Link from "next/link"
import { useRouter } from "next/router"
import { useState } from "react"
import { FormattedMessage } from "react-intl"
import { theme } from "../../../../styles/theme"
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

  const render = (tab: string) => {
    const tabInfo = tabs.find((t) => t.id === tab)
    if (!tabInfo) return "Error"

    const Icon = () => {
      switch (tab) {
        case "home":
          return <HomeIcon />
        case "search":
          return <SearchIcon />
        case "dashboard":
          return <DashboardIcon />
        case "profile":
          return <ProfileIcon />
      }
    }

    return (
      <>
        <Icon />
        <MenuListItemText>
          <Link href={tabInfo.href} onClick={onClose}>
            <Paragraph1>{tabInfo.name}</Paragraph1>
          </Link>
        </MenuListItemText>
      </>
    )
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
                key={tab.id}
                isSelected={activeTab === tab.id}
                onClick={() => {
                  setActiveTab(tab.id)
                }}
              >
                {render(tab.id)}
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
