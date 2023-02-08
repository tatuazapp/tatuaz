import { SliceZone } from "@prismicio/react"
import type {
  GetStaticPropsContext,
  InferGetStaticPropsType,
  NextPage,
} from "next"
import AppLayout from "../components/auth/AppLayout"
import { PageContentWrapper } from "../components/common/PageContentWrapper/styles"
import FAQsHeader from "../components/landing/FAQsHeader"
import GetATattooButton from "../components/landing/GetATattooButton"
import { HomepageIntroAndPhotosSection } from "../components/landing/HomepageIntroAndPhotosWrapper/HomepageIntroAndPhotosWrapperSection/styles"
import { HomepageIntroAndPhotosWrapper } from "../components/landing/HomepageIntroAndPhotosWrapper/styles"
import { createClient } from "../prismicio"
import { ArtistSectionHeader, HomepageIntro } from "../slices"
import ArtistCard from "../slices/ArtistCard"
import FAQsMobileSection from "../slices/FAQsMobileSection"
import FAQsSection from "../slices/FAQsSection"
import TotalStats from "../slices/TotalStats"

type IndexPageProps = InferGetStaticPropsType<typeof getStaticProps>

const Index: NextPage<IndexPageProps> = ({ intro }) => (
  <AppLayout>
    <PageContentWrapper>
      <HomepageIntroAndPhotosWrapper>
        {/* <Profile /> */}

        <HomepageIntroAndPhotosSection>
          <SliceZone
            components={{
              homepage_intro: HomepageIntro,
              artist_section_header: ArtistSectionHeader,
            }}
            slices={intro.data.slices}
          />
        </HomepageIntroAndPhotosSection>

        <HomepageIntroAndPhotosSection>
          <div>
            <h1>kokdwawdawd wdadw dwado</h1>
          </div>
        </HomepageIntroAndPhotosSection>

        {/* <StatsSection />
      <PhotoCardSection /> */}
      </HomepageIntroAndPhotosWrapper>
    </PageContentWrapper>
    <TotalStats />
    <PageContentWrapper>
      {/* <ArtistSectionHeader /> */}
      <ArtistCard />
      <FAQsHeader />
    </PageContentWrapper>
    {/* <FAQsSection /> */}
    <FAQsMobileSection />
    <GetATattooButton />
  </AppLayout>
)

export async function getStaticProps({ previewData }: GetStaticPropsContext) {
  const client = createClient({ previewData })
  const intro = await client.getSingle("homepageIntro")

  return {
    props: {
      intro,
    },
  }
}

export default Index
