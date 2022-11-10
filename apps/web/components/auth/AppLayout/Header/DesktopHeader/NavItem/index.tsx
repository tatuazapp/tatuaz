import { FunctionComponent } from "react"
import { NavItemWrapper } from "./styles"

type NavItemProps = {
  children: React.ReactNode
  href: string
  active?: boolean
}

const NavItem: FunctionComponent<NavItemProps> = ({
  href,
  children: title,
  active = false,
}) => (
  <NavItemWrapper active={active} href={href}>
    {title}
  </NavItemWrapper>
)

export default NavItem
