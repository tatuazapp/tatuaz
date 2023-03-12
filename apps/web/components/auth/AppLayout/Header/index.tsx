import { TopBar } from "@tatuaz/ui"
import { FunctionComponent } from "react"
import { GreenWrapper, NavItemsWrapper, WordmarkWrapper } from "./styles"
import UserAction from "./UserAction"
import useIsPhone from "../../../../utils/hooks/useIsPhone"

type HeaderProps = {
  children?: React.ReactNode
}

const Header: FunctionComponent<HeaderProps> = () => {
  const isPhone = useIsPhone()
  return (
    <TopBar padding={isPhone ? 3 : 1}>
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
