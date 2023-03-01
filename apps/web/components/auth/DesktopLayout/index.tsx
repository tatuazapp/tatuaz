import {
  Drawer,
  DrawerOverlay,
  DrawerContent,
  useDisclosure,
} from "@chakra-ui/react"
import { FunctionComponent } from "react"
import useIsDesktop from "../../../utils/hooks/useIsDesktop"
import Menu from "./Menu"
import NarrowMenu from "./NarrowMenu "
import { DesktopLayoutContainer } from "./styles"

type DesktopLayoutProps = {
  children?: React.ReactNode
}

const DesktopLayout: FunctionComponent<DesktopLayoutProps> = ({ children }) => {
  const { isOpen, onOpen, onClose } = useDisclosure()

  const showDrawer = !useIsDesktop()

  return (
    <DesktopLayoutContainer>
      {!showDrawer && <Menu />}

      {showDrawer && (
        <>
          <NarrowMenu onOpen={onOpen} />

          <Drawer placement="left" onClose={onClose} isOpen={isOpen}>
            <DrawerOverlay />
            <DrawerContent width="184px" maxW="184px">
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
