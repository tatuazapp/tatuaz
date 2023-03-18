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
          textAlign="center"
        >
          140+
        </Heading>
        <Heading
          color={theme.colors.primary}
          level={isTablet ? 5 : 4}
          textAlign="center"
        >
          Artist
        </Heading>
      </StatsItem>
      <CircularSeparator />
      <StatsItem>
        <Heading
          color={theme.colors.primary}
          level={isPhone ? 2 : isTablet ? 3 : 1}
          textAlign="center"
        >
          290+
        </Heading>
        <Heading
          color={theme.colors.primary}
          level={isTablet ? 5 : 4}
          textAlign="center"
        >
          Happy Clients
        </Heading>
      </StatsItem>
      <CircularSeparator />
      <StatsItem>
        <Heading
          color={theme.colors.primary}
          level={isPhone ? 2 : isTablet ? 3 : 1}
          textAlign="center"
        >
          380+
        </Heading>
        <Heading
          color={theme.colors.primary}
          level={isTablet ? 5 : 4}
          textAlign="center"
        >
          Clients
        </Heading>
      </StatsItem>
      <CircularSeparator />
    </TotalStatsWrapper>
  )
}

export default TotalStats
