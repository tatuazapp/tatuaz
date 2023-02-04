import { TopBar } from "@tatuaz/ui"
import { useRouter } from "next/router"
import { FunctionComponent } from "react"
import { GreenWrapper, NavItemsWrapper, WordmarkWrapper } from "./styles"
import UserAction from "./UserAction"

type HeaderProps = {
  children?: React.ReactNode
}

const Header: FunctionComponent<HeaderProps> = () => {
  const router = useRouter()

  return (
    <TopBar>
      <WordmarkWrapper>
        Tatuaz<GreenWrapper>App</GreenWrapper>
      </WordmarkWrapper>
      <NavItemsWrapper>
        <UserAction />
      </NavItemsWrapper>
    </TopBar>
  )
}

export default Header
