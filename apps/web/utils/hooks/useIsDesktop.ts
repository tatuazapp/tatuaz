import { up } from "styled-breakpoints"
import { useBreakpoint } from "styled-breakpoints/react-styled"

const useIsDesktop = () => useBreakpoint(up("md"))

export default useIsDesktop
