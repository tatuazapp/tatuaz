import { useMediaQuery } from "react-responsive"

export default function useIsMobile() {
  const isDesktop = useMediaQuery({ minWidth: 992 })

  const isTablet = useMediaQuery({ minWidth: 768, maxWidth: 991 })

  const isMobile = useMediaQuery({ maxWidth: 991 }) //CASE: NO TABLET MODE

  return {
    isDesktop,
    isTablet,
    isMobile,
  }
}
