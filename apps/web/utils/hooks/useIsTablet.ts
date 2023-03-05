import { down } from "styled-breakpoints"
import { useBreakpoint } from "styled-breakpoints/react-styled"

const useIsTablet = () => useBreakpoint(down("md"))

export default useIsTablet
