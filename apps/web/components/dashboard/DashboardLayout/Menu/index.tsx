import { Paragraph2 } from "@tatuaz/ui"
import Link from "next/link"
import { useRouter } from "next/router"
import { FormattedMessage } from "react-intl"
import { Tabs } from "../../../../types/tabs"
import { SignOutButton } from "../../../auth/SignoutButton"
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
  // TODO: This panel does not maked sense for now
  // eslint-disable-next-line etc/no-commented-out-code
  // {
  //   name: <FormattedMessage defaultMessage="Panel" id="hIxZtX" />,
  //   href: "/dashboard/dashboard",
  //   id: "dashboard",
  // },
  {
    name: <FormattedMessage defaultMessage="Profil" id="n7oiI/" />,
    href: "/dashboard/profile",
    id: "profile",
  },
] satisfies {
  id: Tabs
  name: JSX.Element
  href: string
}[]

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
          <Paragraph2>{tabInfo.name}</Paragraph2>
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
        <Link href="/">
          <WordmarkWrapper>
            Tatuaz<GreenWrapper>App</GreenWrapper>
          </WordmarkWrapper>
        </Link>
        <MenuList>
          {tabs.map((tab) => (
            <MenuListItem
              key={tab.id}
              isSelected={currentTab === tab.id}
              onClick={() => handleTabClick(tab.id)}
            >
              {render(tab.id)}
            </MenuListItem>
          ))}
        </MenuList>
      </div>
      <SignOutButtonWrapper>
        <SignOutButton />
      </SignOutButtonWrapper>
    </MenuWrapper>
  )
}

export default Menu
