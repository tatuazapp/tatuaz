import { SliceZone } from "@prismicio/react"
import type {
  GetStaticPropsContext,
  InferGetStaticPropsType,
  NextPage,
} from "next"
import AppLayout from "../components/auth/AppLayout"
import { PageContentWrapper } from "../components/common/PageContentWrapper/styles"
// import ArtistSection from "../components/landing/ArtistsSection"
import FAQsHeader from "../components/landing/FAQsHeader"
import GetATattooButton from "../components/landing/GetATattooButton"
import { HomepageIntroAndPhotosSection } from "../components/landing/HomepageIntroAndPhotosWrapper/HomepageIntroAndPhotosWrapperSection/styles"
import { HomepageIntroAndPhotosWrapper } from "../components/landing/HomepageIntroAndPhotosWrapper/styles"
import TotalStats from "../components/landing/TotalStats"
import { createClient } from "../prismicio"
import {
  ArtistSectionHeader,
  HomepageIntro,
  HomepagePhotos,
  FaqsSection,
} from "../slices"
// import FAQsSection from "../slices/FAQsSection"

type IndexPageProps = InferGetStaticPropsType<typeof getStaticProps>

const Index: NextPage<IndexPageProps> = ({
  homepageIntro,
  artistsectionheader,
  homepagephotos,
  faqssection,
}) => (
  <AppLayout>
    <PageContentWrapper>
      <HomepageIntroAndPhotosWrapper>
        {/* <Profile /> */}

        <HomepageIntroAndPhotosSection>
          <SliceZone
            components={{
              homepage_intro: HomepageIntro,
            }}
            slices={homepageIntro.data.slices}
          />
        </HomepageIntroAndPhotosSection>

        <HomepageIntroAndPhotosSection>
          <SliceZone
            components={{
              homepage_photos: HomepagePhotos,
            }}
            slices={homepagephotos.data.slices}
          />
        </HomepageIntroAndPhotosSection>

        {/* <StatsSection />
      <PhotoCardSection /> */}
      </HomepageIntroAndPhotosWrapper>
    </PageContentWrapper>
    <TotalStats />
    <PageContentWrapper>
      {/* <ArtistSectionHeader /> */}
      <SliceZone
        components={{
          artist_section_header: ArtistSectionHeader,
        }}
        slices={artistsectionheader.data.slices}
      />
      {/* <ArtistSection /> */}
      <FAQsHeader />
    </PageContentWrapper>
    {/* <FAQsSection /> */}
    <SliceZone
      components={{
        faqs_section: FaqsSection,
      }}
      slices={faqssection.data.slices}
    />
    {/* <FAQsMobileSection /> */}
    <GetATattooButton />
  </AppLayout>
)

export async function getStaticProps({ previewData }: GetStaticPropsContext) {
  const client = createClient({ previewData })
  const homepageIntro = await client.getSingle("homepageIntro")
  const artistsectionheader = await client.getSingle("ArtistSectionHeader")
  const homepagephotos = await client.getSingle("HomepagePhotos")
  const faqssection = await client.getSingle("FAQsSection")

  return {
    props: {
      homepageIntro,
      artistsectionheader,
      homepagephotos,
      faqssection,
    },
  }
}

export default Index
