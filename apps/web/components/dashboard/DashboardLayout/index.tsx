import {
  Drawer,
  DrawerOverlay,
  DrawerContent,
  useDisclosure,
} from "@chakra-ui/react"
import { useRouter } from "next/router"
import { FunctionComponent, useEffect } from "react"
import useMe from "../../../api/hooks/useMe"
import useIsMobile from "../../../utils/hooks/useIsMobile"
import useIsWideDesktop from "../../../utils/hooks/useIsWideDesktop"
import Footer from "../../auth/AppLayout/Footer"
import Menu from "./Menu"
import MobileMenu from "./MobileMenu"
import NarrowMenu from "./NarrowMenu"
import {
  DesktopLayoutContainer,
  GreenWrapper,
  MobileLayoutContainer,
  MobileLayoutHeader,
  MobileMenuIcon,
  WordmarkWrapper,
} from "./styles"

type DashboardLayoutProps = {
  children?: React.ReactNode
}

const DashboardLayout: FunctionComponent<DashboardLayoutProps> = ({
  children,
}) => {
  const { isOpen, onOpen, onClose } = useDisclosure()

  const router = useRouter()

  const showDrawer = useIsWideDesktop()
  const mobileVersion = useIsMobile()

  const data = useMe()

  useEffect(() => {
    if (data && !data?.username) {
      router.push("/")
    }
  }, [data, data?.username, router])

  return mobileVersion ? (
    <>
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
        {children}
      </MobileLayoutContainer>
      <Footer />
    </>
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
      {children}
    </DesktopLayoutContainer>
  )
}
export default DashboardLayout
