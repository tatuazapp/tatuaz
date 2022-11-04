import { TopBar } from "@tatuaz/ui"
import { useRouter } from "next/router"
import { FunctionComponent } from "react"
import { FormattedMessage } from "react-intl"
import NavItem from "./NavItem"
import { NavItemsWrapper, WordmarkWrapper } from "./styles"
import UserAction from "./UserAction"

type HeaderProps = {
  children?: React.ReactNode
}

const navItems = [
  {
    title: <FormattedMessage defaultMessage="Strona główna" id="8GchbR" />,
    href: "/",
  },
  {
    title: <FormattedMessage defaultMessage="Przeglądaj" id="wk+vkw" />,
    href: "/about",
  },
  {
    title: <FormattedMessage defaultMessage="Forum" id="6slkqp" />,
    href: "/forum",
  },
]

const Header: FunctionComponent<HeaderProps> = () => {
  const router = useRouter()

  return (
    <TopBar>
      <WordmarkWrapper>Bookink</WordmarkWrapper>
      <NavItemsWrapper>
        {navItems.map((item) => (
          <NavItem
            key={item.href}
            active={router.pathname === item.href}
            href={item.href}
          >
            {item.title}
          </NavItem>
        ))}
        <UserAction />
      </NavItemsWrapper>
    </TopBar>
  )
}

export default Header
