import { FunctionComponent, useState } from "react"
import useIsMobile from "../../../utils/hooks/useIsMobile"
import DesktopHeader from "./Header/DesktopHeader"
import MobileHeader from "./Header/MobileHeader"
import MobileMenu from "./MobileMenu"
import { Boxunder } from "./styles"

type AppLayoutProps = {
  children?: React.ReactNode
}

const AppLayout: FunctionComponent<AppLayoutProps> = ({ children }) => {
  const { isMobile } = useIsMobile()

  const [isMenuOpen, setIsMenuOpen] = useState<boolean>(false)

  return (
    <>
      <Boxunder>
        {!isMobile && <DesktopHeader />}
        {isMobile && <MobileHeader setIsMenuOpen={setIsMenuOpen} />}
        {children}
      </Boxunder>
      {isMenuOpen && <MobileMenu setIsMenuOpen={setIsMenuOpen} />}
    </>
  )
}

export default AppLayout
