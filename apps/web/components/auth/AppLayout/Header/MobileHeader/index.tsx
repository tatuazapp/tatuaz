import { TopBar } from "@tatuaz/ui"
import { useRouter } from "next/router"
import { FunctionComponent } from "react"
import MenuIcon from "./MenuIcon"
import { NavItemsWrapper, WordmarkWrapper } from "./styles"

type MobileHeaderProps = {
  children?: React.ReactNode
  setIsMenuOpen: React.Dispatch<React.SetStateAction<boolean>>
}

const MobileHeader: FunctionComponent<MobileHeaderProps> = ({
  setIsMenuOpen,
}) => {
  const router = useRouter()

  return (
    <TopBar>
      <WordmarkWrapper>Bookink</WordmarkWrapper>
      <NavItemsWrapper>
        <MenuIcon setIsMenuOpen={setIsMenuOpen} />
      </NavItemsWrapper>
    </TopBar>
  )
}

export default MobileHeader
