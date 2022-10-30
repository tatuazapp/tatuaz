import { FunctionComponent } from "react"
import { NavItemWrapper } from "./styles"

type NavItemProps = {
  children: React.ReactNode
  href: string
}

const NavItem: FunctionComponent<NavItemProps> = ({
  href,
  children: title,
}) => <NavItemWrapper href={href}>{title}</NavItemWrapper>

export default NavItem
