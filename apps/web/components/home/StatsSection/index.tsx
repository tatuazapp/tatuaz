import { FormattedMessage } from "react-intl"
import { TitleContainer, CardsContainer } from "./styles"
import StatsCard from "../StatsCard"

const StatsSection = () => (
  <>
    <TitleContainer>
      <FormattedMessage
        defaultMessage="Ostatni tydzień w pigułce"
        id="MMkYtO"
      />
    </TitleContainer>
    <CardsContainer>
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
    </CardsContainer>
  </>
)

export default StatsSection
