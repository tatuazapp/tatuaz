import { SliceZone } from "@prismicio/react"
import { createClient } from "../prismicio"
import { components } from "../slices"

const Index = ({ page }) => (
  <SliceZone components={components} slices={page.data.slices} />
)

export default Index

export async function getStaticProps({ previewData }) {
  const client = createClient({ previewData })

  const page = await client.getSingle("test")

  return {
    props: {
      page,
    },
  }
}
