import { Box } from "@chakra-ui/react"
import { FunctionComponent } from "react"
import Header from "./Header"
import WeekendCard from "./WeekendCard"
import { TitleContainer } from "./WeekendCard/styles"

type AppLayoutProps = {
  children?: React.ReactNode
}

const AppLayout: FunctionComponent<AppLayoutProps> = ({ children }) => (
  <>
    <Header />
    <Box>
      <TitleContainer>Ostatni tydzień w pigułce</TitleContainer>
    </Box>
    <Box paddingTop={150} display="flex" justifyContent="space-around">
      <WeekendCard
        description="
          Tyle osób zabookowało u nas sesję tatuażu"
        value="10 000"
      />
      <WeekendCard
        description="Artysta, który najszybciej zyskiwał popularność "
        value="Michal Dzakson"
      />
      <WeekendCard
        description="Artysta, który najszybciej zyskiwał popularność"
        value="Ejczu Ebezoka"
      />

      <WeekendCard
        description="Tylu nowych artystów dołączyło do Bookink"
        value="482"
      />
    </Box>

    {children}
  </>
)
export default AppLayout
