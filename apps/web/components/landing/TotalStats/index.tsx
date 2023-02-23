import {
  TotalStatsWrapper,
  CircularSeparator,
  StatsItem,
  StatsItemTop,
  StatsItemBottom,
} from "./styles"

const TotalStats = () => (
  <TotalStatsWrapper>
    <CircularSeparator />
    <StatsItem>
      <StatsItemTop>
        <h2>140+</h2>
      </StatsItemTop>
      <StatsItemBottom>
        <h2>Artist</h2>
      </StatsItemBottom>
    </StatsItem>
    <CircularSeparator />
    <StatsItem>
      <StatsItemTop>
        <h2>290+</h2>
      </StatsItemTop>
      <StatsItemBottom>
        <h2>Happy Clients</h2>
      </StatsItemBottom>
    </StatsItem>
    <CircularSeparator />
    <StatsItem>
      <StatsItemTop>
        <h2>380+</h2>
      </StatsItemTop>
      <StatsItemBottom>
        <h2>Clients</h2>
      </StatsItemBottom>
    </StatsItem>
    <CircularSeparator />
  </TotalStatsWrapper>
)

export default TotalStats
