import { TopBar } from "@tatuaz/ui"
import { FunctionComponent } from "react"
import { FormattedMessage } from "react-intl"
import NavItem from "./NavItem"
import { NavItemsWrapper, WordmarkWrapper } from "./styles"
import UserAction from "./UserAction"

type HeaderProps = {
  children?: React.ReactNode
}

const Header: FunctionComponent<HeaderProps> = () => (
  <TopBar>
    <WordmarkWrapper>Bookink</WordmarkWrapper>
    <NavItemsWrapper>
      <NavItem href="/">
        <FormattedMessage defaultMessage="Strona główna" id="8GchbR" />
      </NavItem>
      <NavItem href="/about">
        <FormattedMessage defaultMessage="Przeglądaj" id="wk+vkw" />
      </NavItem>
      <NavItem href="/about">
        <FormattedMessage defaultMessage="Forum" id="6slkqp" />
      </NavItem>
      <UserAction />
    </NavItemsWrapper>
  </TopBar>
)

export default Header
