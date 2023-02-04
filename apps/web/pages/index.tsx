import { SliceZone } from "@prismicio/react"
import type {
  GetStaticPropsContext,
  InferGetStaticPropsType,
  NextPage,
} from "next"
import AppLayout from "../components/auth/AppLayout"
import { PageContentWrapper } from "../components/common/PageContentWrapper/styles"
import { createClient } from "../prismicio"
import { HomepageIntro } from "../slices"

type IndexPageProps = InferGetStaticPropsType<typeof getStaticProps>

const Index: NextPage<IndexPageProps> = ({ intro }) => (
  <AppLayout>
    <PageContentWrapper>
      {/* <Profile /> */}
      <SliceZone
        components={{
          homepage_intro: HomepageIntro,
        }}
        slices={intro.data.slices}
      />
      {/* <StatsSection />
      <PhotoCardSection /> */}
    </PageContentWrapper>
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
