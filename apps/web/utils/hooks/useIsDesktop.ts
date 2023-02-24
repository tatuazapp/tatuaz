import { up } from "styled-breakpoints"
import { useBreakpoint } from "styled-breakpoints/react-styled"

const useIsDesktop = () => useBreakpoint(up("lg"))

export default useIsDesktop
