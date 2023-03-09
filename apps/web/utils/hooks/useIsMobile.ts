import { down } from "styled-breakpoints"
import { useBreakpoint } from "styled-breakpoints/react-styled"

const useIsMobile = () => useBreakpoint(down("lg"))

export default useIsMobile
