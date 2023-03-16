import { Heading } from "@tatuaz/ui"
import { theme } from "../../../styles/theme"
import useIsPhone from "../../../utils/hooks/useIsPhone"
import useIsTablet from "../../../utils/hooks/useIsTablet"
import { TotalStatsWrapper, CircularSeparator, StatsItem } from "./styles"

const TotalStats = () => {
  const isTablet = useIsTablet()
  const isPhone = useIsPhone()
  return (
    <TotalStatsWrapper>
      <CircularSeparator />
      <StatsItem>
        <Heading
          color={theme.colors.primary}
          level={isPhone ? 2 : isTablet ? 3 : 1}
        >
          140+
        </Heading>
        <Heading color={theme.colors.primary} level={isTablet ? 5 : 4}>
          Artist
        </Heading>
      </StatsItem>
      <CircularSeparator />
      <StatsItem>
        <Heading
          color={theme.colors.primary}
          level={isPhone ? 2 : isTablet ? 3 : 1}
        >
          290+
        </Heading>
        <Heading color={theme.colors.primary} level={isTablet ? 5 : 4}>
          Happy Clients
        </Heading>
      </StatsItem>
      <CircularSeparator />
      <StatsItem>
        <Heading
          color={theme.colors.primary}
          level={isPhone ? 2 : isTablet ? 3 : 1}
        >
          380+
        </Heading>
        <Heading color={theme.colors.primary} level={isTablet ? 5 : 4}>
          Clients
        </Heading>
      </StatsItem>
      <CircularSeparator />
    </TotalStatsWrapper>
  )
}

export default TotalStats
