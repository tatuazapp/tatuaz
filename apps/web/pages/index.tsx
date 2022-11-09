import AppLayout from "../components/auth/AppLayout"
import PhotoCardSection from "../components/home/PhotoCardSection"
import StatsSection from "../components/home/StatsSection"
import { PageContentWrapper } from "./styles"

const Index = () => (
  <AppLayout>
    <PageContentWrapper>
      <StatsSection />
      <PhotoCardSection />
    </PageContentWrapper>
  </AppLayout>
)

export default Index
