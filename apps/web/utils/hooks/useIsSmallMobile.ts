import { down } from "styled-breakpoints"
import { useBreakpoint } from "styled-breakpoints/react-styled"

const useIsSmallPhone = () => useBreakpoint(down("xs"))

export default useIsSmallPhone
