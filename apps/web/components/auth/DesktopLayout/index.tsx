import {
  Drawer,
  DrawerOverlay,
  DrawerContent,
  useDisclosure,
} from "@chakra-ui/react"
import { FunctionComponent } from "react"
import useIsDesktop from "../../../utils/hooks/useIsDesktop"
import useIsMobile from "../../../utils/hooks/useIsMobile"
import Menu from "./Menu"
import MobileMenu from "./MobileMenu"
import NarrowMenu from "./NarrowMenu "
import {
  DesktopLayoutContainer,
  GreenWrapper,
  MobileLayoutContainer,
  MobileLayoutHeader,
  MobileMenuIcon,
  WordmarkWrapper,
} from "./styles"

type DesktopLayoutProps = {
  children?: React.ReactNode
}

const DesktopLayout: FunctionComponent<DesktopLayoutProps> = ({ children }) => {
  const { isOpen, onOpen, onClose } = useDisclosure()

  const showDrawer = useIsDesktop()
  const mobileVersion = useIsMobile()

  return mobileVersion ? (
    <MobileLayoutContainer>
      <MobileLayoutHeader>
        <WordmarkWrapper>
          Tatuaz<GreenWrapper>App</GreenWrapper>
        </WordmarkWrapper>
        <MobileMenuIcon onClick={onOpen} />
      </MobileLayoutHeader>
      <Drawer isOpen={isOpen} placement="right" size="full" onClose={onClose}>
        <DrawerOverlay />
        <DrawerContent display="flex" justifyContent="center">
          <MobileMenu onClose={onClose} />
        </DrawerContent>
      </Drawer>
      <div>{children}</div>
    </MobileLayoutContainer>
  ) : (
    <DesktopLayoutContainer>
      {showDrawer && <Menu />}
      {!showDrawer && (
        <>
          <NarrowMenu onOpen={onOpen} />
          <Drawer isOpen={isOpen} placement="left" onClose={onClose}>
            <DrawerOverlay />
            <DrawerContent maxWidth="184px">
              <Menu />
            </DrawerContent>
          </Drawer>
        </>
      )}
      <div>{children}</div>
    </DesktopLayoutContainer>
  )
}
export default DesktopLayout
