import AppLayout from "../components/auth/AppLayout"
import { PageContentWrapper } from "../components/common/PageContentWrapper/styles"
import PhotoCardSection from "../components/home/PhotoCardSection"
import StatsSection from "../components/home/StatsSection"

const Index = () => (
  <AppLayout>
    <PageContentWrapper>
      <StatsSection />
      <PhotoCardSection />
    </PageContentWrapper>
  </AppLayout>
)

export default Index
