import { useRouter } from "next/router"
import { FunctionComponent } from "react"
import { FormattedMessage } from "react-intl"
import CloseMenuIcon from "./CloseIcon"
import LoginItem from "./LoginItem"
import NavItem from "./NavItem"
// import NavItem from "./NavItem"
import { ActionContainer, MobileMenuContainer } from "./styles"

type MobileMenuProps = {
  setIsMenuOpen: React.Dispatch<React.SetStateAction<boolean>>
}

// const navItems = [
//   {
//     title: <FormattedMessage defaultMessage="Strona główna" id="8GchbR" />,
//     href: "/",
//   },
//   {
//     title: <FormattedMessage defaultMessage="Przeglądaj" id="wk+vkw" />,
//     href: "/about",
//   },
//   {
//     title: <FormattedMessage defaultMessage="Forum" id="6slkqp" />,
//     href: "/forum",
//   },
// ]

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

const MobileMenu: FunctionComponent<MobileMenuProps> = ({ setIsMenuOpen }) => {
  const router = useRouter()

  return (
    <MobileMenuContainer>
      <ActionContainer>
        {navItems.map((item) => (
          <a key={item.href} href={item.href}>
            <NavItem
              active={router.pathname === item.href}
              title={item.title}
            />
          </a>
        ))}

        <LoginItem />
      </ActionContainer>
      <CloseMenuIcon setIsMenuOpen={setIsMenuOpen} />
    </MobileMenuContainer>
  )
}

export default MobileMenu
