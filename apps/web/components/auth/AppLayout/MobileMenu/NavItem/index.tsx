import { FunctionComponent } from "react"
import { ButtonWrapper } from "./styles"

type NavItemProps = {
  title: JSX.Element
  active: boolean
}

const NavItem: FunctionComponent<NavItemProps> = ({ title, active }) => (
  <ButtonWrapper active={active} disabled={active}>
    {title}
  </ButtonWrapper>
)

export default NavItem
