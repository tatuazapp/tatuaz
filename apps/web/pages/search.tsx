import type { NextPage } from "next"
import DesktopLayout from "../components/auth/DesktopLayout"
import ArtistsGrid from "../components/search/ArtistsArea"
import SearchButtonArea from "../components/search/ButtonArea"
import SearchArea from "../components/search/SearchArea"
import { SearchContentWrapper } from "../components/search/SearchContentWrapper/styles"

const Index: NextPage = () => (
  <DesktopLayout>
    <SearchContentWrapper>
      <SearchArea />
      <SearchButtonArea />
      <ArtistsGrid />
    </SearchContentWrapper>
  </DesktopLayout>
)

export default Index
