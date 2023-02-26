import { TopBar } from "@tatuaz/ui"
import { FunctionComponent } from "react"
import { GreenWrapper, NavItemsWrapper, WordmarkWrapper } from "./styles"
import UserAction from "./UserAction"

type HeaderProps = {
  children?: React.ReactNode
}

const Header: FunctionComponent<HeaderProps> = () => (
  <TopBar>
    <WordmarkWrapper>
      Tatuaz<GreenWrapper>App</GreenWrapper>
    </WordmarkWrapper>
    <NavItemsWrapper>
      <UserAction />
    </NavItemsWrapper>
  </TopBar>
)

export default Header
