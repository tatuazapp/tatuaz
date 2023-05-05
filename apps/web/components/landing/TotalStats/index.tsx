import { useQuery } from "@tanstack/react-query"
import { Heading } from "@tatuaz/ui"
import { FormattedMessage } from "react-intl"
import { api } from "../../../api/apiClient"
import { queryKeys } from "../../../api/queryKeys"
import { theme } from "../../../styles/theme"
import useIsPhone from "../../../utils/hooks/useIsPhone"
import useIsTablet from "../../../utils/hooks/useIsTablet"
import { TotalStatsWrapper, CircularSeparator, StatsItem } from "./styles"

const TotalStats = () => {
  const isTablet = useIsTablet()
  const isPhone = useIsPhone()

  const { data } = useQuery(
    [queryKeys.getRegisteredStats],
    api.statistics.getRegisteredStats,
    {
      refetchOnMount: false,
      refetchOnWindowFocus: false,
    }
  )

  // TODO: don't cast to this type, this is a temporary solution and needs to be fixed on backend
  const stats = data?.value as unknown as {
    artists: number
    clients: number
    users: number
  }

  return (
    <TotalStatsWrapper>
      <CircularSeparator />
      <StatsItem>
        <Heading
          color={theme.colors.primary}
          level={isPhone ? 2 : isTablet ? 3 : 1}
          textAlign="center"
        >
          {stats?.artists ?? "-"}
        </Heading>
        <Heading
          color={theme.colors.primary}
          level={isTablet ? 5 : 4}
          textAlign="center"
        >
          <FormattedMessage defaultMessage="Artyści" id="8JzbSH" />
        </Heading>
      </StatsItem>
      <CircularSeparator />
      <StatsItem>
        <Heading
          color={theme.colors.primary}
          level={isPhone ? 2 : isTablet ? 3 : 1}
          textAlign="center"
        >
          {stats?.clients ?? "-"}
        </Heading>
        <Heading
          color={theme.colors.primary}
          level={isTablet ? 5 : 4}
          textAlign="center"
        >
          <FormattedMessage defaultMessage="Klienci" id="RTmXj2" />
        </Heading>
      </StatsItem>
      <CircularSeparator />
      <StatsItem>
        <Heading
          color={theme.colors.primary}
          level={isPhone ? 2 : isTablet ? 3 : 1}
          textAlign="center"
        >
          {stats?.users ?? "-"}
        </Heading>
        <Heading
          color={theme.colors.primary}
          level={isTablet ? 5 : 4}
          textAlign="center"
        >
          <FormattedMessage defaultMessage="Użytkownicy" id="Xt52bW" />
        </Heading>
      </StatsItem>
      <CircularSeparator />
    </TotalStatsWrapper>
  )
}

export default TotalStats
