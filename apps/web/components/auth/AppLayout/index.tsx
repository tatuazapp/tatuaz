import { FunctionComponent, useState } from "react"
import DesktopHeader from "./Header/DesktopHeader"
import MobileHeader from "./Header/MobileHeader"
import MobileMenu from "./MobileMenu"
import { Boxunder } from "./styles"

type AppLayoutProps = {
  children?: React.ReactNode
}

const AppLayout: FunctionComponent<AppLayoutProps> = ({ children }) => {
  const mobile = true

  const [isMenuOpen, setIsMenuOpen] = useState<boolean>(false)

  return (
    <>
      <Boxunder>
        {!mobile && <DesktopHeader />}
        {mobile && <MobileHeader setIsMenuOpen={setIsMenuOpen} />}
        {children}
      </Boxunder>
      {isMenuOpen && <MobileMenu setIsMenuOpen={setIsMenuOpen} />}
    </>
  )
}

export default AppLayout
