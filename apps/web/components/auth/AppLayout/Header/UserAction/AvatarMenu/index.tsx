import { User } from "@auth0/auth0-react"
import { Avatar, Menu, MenuButton, MenuList } from "@chakra-ui/react"
import { FunctionComponent } from "react"
import LogoutMenuItem from "./LogoutMenuItem"

type AvatarMenuProps = {
  user: User
}

const AvatarMenu: FunctionComponent<AvatarMenuProps> = ({ user }) => (
  <Menu>
    <MenuButton
      as={Avatar}
      cursor="pointer"
      name={user.name}
      src={user.picture}
    />
    <MenuList>
      <LogoutMenuItem />
    </MenuList>
  </Menu>
)

export default AvatarMenu
