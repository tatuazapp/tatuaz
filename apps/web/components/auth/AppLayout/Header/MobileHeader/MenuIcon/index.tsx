import { HamburgerIcon } from "@chakra-ui/icons"
import { IconButton } from "@chakra-ui/react"
import { FunctionComponent } from "react"

type MenuIconProps = {
  setIsMenuOpen: React.Dispatch<React.SetStateAction<boolean>>
}

const MenuIcon: FunctionComponent<MenuIconProps> = ({ setIsMenuOpen }) => {
  const onClickHandler = () => {
    setIsMenuOpen((prevCheck) => !prevCheck)
  }

  return (
    <IconButton
      _focus={{ backgroundColor: "white" }}
      _hover={{ backgroundColor: "white" }}
      aria-label="Call Segun"
      backgroundColor="white"
      fontSize="50px" //TODO: Change to rem function
      icon={<HamburgerIcon />}
      onClick={onClickHandler}
    />
  )
}

export default MenuIcon
