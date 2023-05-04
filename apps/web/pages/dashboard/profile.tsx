import type { NextPage } from "next"
import { DashboardContentWrapper } from "../../components/dashboard/DashboardContentWrapper/styles"
import DashboardLayout from "../../components/dashboard/DashboardLayout"
import BackgroundPhotoSection from "../../components/dashboard/profile/BackgroundPhotoSection"
import TopArtistsSection from "../../components/dashboard/TopArtistsSection"

const UserProfile: NextPage = () => (
  <DashboardLayout>
    <DashboardContentWrapper>
      <BackgroundPhotoSection />
      <TopArtistsSection />
    </DashboardContentWrapper>
  </DashboardLayout>
)

export default UserProfile
