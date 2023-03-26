import { Paragraph, Paragraph1 } from "@tatuaz/ui"
import Link from "next/link"
import { useRouter } from "next/router"
import { FormattedMessage } from "react-intl"
import { theme } from "../../../../styles/theme"
import { Tabs } from "../../../../types/tabs"
import Button from "../../../common/buttons/Button"
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
  SignOutButtonWrapper,
} from "./styles"

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

const render = (tab: Tabs) => {
  const tabInfo = tabs.find((t) => t.id === tab.toLowerCase())
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
        <Link href={tabInfo.href}>
          <Paragraph1>{tabInfo.name}</Paragraph1>
        </Link>
      </MenuListItemText>
    </>
  )
}

const Menu = () => {
  const router = useRouter()

  const currentTab =
    router.pathname.split("/")[2] === undefined
      ? "home"
      : router.pathname.split("/")[2]

  const handleTabClick = (tab: Tabs) => {
    router.push(tabs.find((t) => t.id === tab)?.href || "/")
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
              key={tab.id}
              isSelected={currentTab === tab.id}
              onClick={() => handleTabClick(tab.id as Tabs)}
            >
              {render(tab.id as Tabs)}
            </MenuListItem>
          ))}
        </MenuList>
      </div>
      <SignOutButtonWrapper>
        <Button kind="primary">
          <Paragraph color={theme.colors.background1} level={1}>
            <FormattedMessage defaultMessage="Wyloguj" id="UqV7Od" />
          </Paragraph>
        </Button>
      </SignOutButtonWrapper>
    </MenuWrapper>
  )
}

export default Menu
