import { CloseIcon } from "@chakra-ui/icons"
import { IconButton } from "@chakra-ui/react"
import { FunctionComponent } from "react"

type CloseMenuIconProps = {
  setIsMenuOpen: React.Dispatch<React.SetStateAction<boolean>>
}

const CloseMenuIcon: FunctionComponent<CloseMenuIconProps> = ({
  setIsMenuOpen,
}) => {
  const onClickHandler = () => {
    setIsMenuOpen(false)
  }

  return (
    <IconButton
      _focus={{ backgroundColor: "white" }}
      _hover={{ backgroundColor: "white" }}
      aria-label="Call Segun"
      backgroundColor="white"
      fontSize="30px" //TODO: Change to rem function
      icon={<CloseIcon />}
      onClick={onClickHandler}
    />
  )
}

export default CloseMenuIcon
