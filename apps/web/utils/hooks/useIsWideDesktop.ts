import { up } from "styled-breakpoints"
import { useBreakpoint } from "styled-breakpoints/react-styled"

const useIsWideDesktop = () => useBreakpoint(up("xl"))

export default useIsWideDesktop
