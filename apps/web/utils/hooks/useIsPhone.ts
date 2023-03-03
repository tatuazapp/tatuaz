import { down } from "styled-breakpoints"
import { useBreakpoint } from "styled-breakpoints/react-styled"

const useIsPhone = () => useBreakpoint(down("sm"))

export default useIsPhone
