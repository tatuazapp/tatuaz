import { Center } from "@chakra-ui/react"
import type { NextPage } from "next"
import { PageContentWrapper } from "../components/common/PageContentWrapper/styles"
import ArtistsPostSection from "../components/dashboard/ArtistsPostSection"
import { DashboardContentWrapper } from "../components/dashboard/DashboardContentWrapper/styles"
import TopArtistsSection from "../components/dashboard/TopArtistsSection"

const Index: NextPage = () => (
  // <AppLayout>
  <DashboardContentWrapper>
    <ArtistsPostSection />
    <TopArtistsSection />
  </DashboardContentWrapper>
  // { </AppLayout> }
)

export default Index
