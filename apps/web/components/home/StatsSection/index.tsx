import { Box } from "@chakra-ui/react"
import StatsCard from "../StatsCard"
import { TitleContainer } from "../StatsCard/styles"

const StatsSection = () => (
  <>
    <Box>
      <TitleContainer>Ostatni tydzień w pigułce</TitleContainer>
    </Box>
    <Box display="flex" justifyContent="space-around" paddingTop={150}>
      {/* MOCK_START */}
      <StatsCard
        description="
      Tyle osób zabookowało u nas sesję tatuażu"
        value="10 000"
      />
      <StatsCard
        description="Artysta, który najszybciej zyskiwał popularność "
        value="Michal Dzakson"
      />
      <StatsCard
        description="Artysta, który najszybciej zyskiwał popularność"
        value="Ejczu Ebezoka KOKOloko"
      />
      <StatsCard
        description="Tylu nowych artystów dołączyło do Bookink"
        value="482"
      />
      {/* MOCK_END */}
    </Box>
  </>
)

export default StatsSection
