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
import TotalStats from "../components/landing/TotalStats"
import { createClient } from "../prismicio"
import {
  ArtistSectionHeader,
  HomepageIntro,
  HomepagePhotos,
  FaqsSection,
  ArtistsCarousel,
} from "../slices"

type IndexPageProps = InferGetStaticPropsType<typeof getStaticProps>

const Index: NextPage<IndexPageProps> = ({
  homepageIntro,
  artistsectionheader,
  homepagephotos,
  faqssection,
  artistscarousel,
}) => (
  <AppLayout>
    <PageContentWrapper>
      <HomepageIntroAndPhotosWrapper>
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
      </HomepageIntroAndPhotosWrapper>
    </PageContentWrapper>
    <TotalStats />

    <PageContentWrapper>
      <SliceZone
        components={{
          artist_section_header: ArtistSectionHeader,
        }}
        slices={artistsectionheader.data.slices}
      />
      <SliceZone
        components={{
          artists_carousel: ArtistsCarousel,
        }}
        slices={artistscarousel.data.slices}
      />
      <FAQsHeader />
    </PageContentWrapper>
    <SliceZone
      components={{
        faqs_section: FaqsSection,
      }}
      slices={faqssection.data.slices}
    />
    <GetATattooButton />
  </AppLayout>
)

export async function getStaticProps({ previewData }: GetStaticPropsContext) {
  const client = createClient({ previewData })
  const homepageIntro = await client.getSingle("homepageIntro")
  const artistSectionHeader = await client.getSingle("ArtistSectionHeader")
  const homepagePhotos = await client.getSingle("HomepagePhotos")
  const faqsSection = await client.getSingle("FAQsSection")
  const artistsCarousel = await client.getSingle("artistscarousel")

  return {
    props: {
      homepageIntro,
      artistsectionheader: artistSectionHeader,
      homepagephotos: homepagePhotos,
      faqssection: faqsSection,
      artistscarousel: artistsCarousel,
    },
  }
}

export default Index
