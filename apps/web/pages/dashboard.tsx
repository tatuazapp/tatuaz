import type { NextPage } from "next"
import DesktopLayout from "../components/auth/DesktopLayout"
import ArtistsPostSection from "../components/dashboard/ArtistsPostSection"
import { DashboardContentWrapper } from "../components/dashboard/DashboardContentWrapper/styles"
import TopArtistsSection from "../components/dashboard/TopArtistsSection"

const Index: NextPage = () => (
  <DesktopLayout>
    <DashboardContentWrapper>
      <ArtistsPostSection />
      <TopArtistsSection />
    </DashboardContentWrapper>
  </DesktopLayout>
)

export default Index
