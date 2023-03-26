import type { NextPage } from "next"
import ArtistsPostSection from "../../components/dashboard/ArtistsPostSection"
import { DashboardContentWrapper } from "../../components/dashboard/DashboardContentWrapper/styles"
import DashboardLayout from "../../components/dashboard/DashboardLayout"
import TopArtistsSection from "../../components/dashboard/TopArtistsSection"

const UserProfile: NextPage = () => (
  <DashboardLayout>
    <DashboardContentWrapper>
      <ArtistsPostSection />
      <TopArtistsSection />
    </DashboardContentWrapper>
  </DashboardLayout>
)

export default UserProfile
