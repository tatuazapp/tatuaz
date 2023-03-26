import type { NextPage } from "next"
import DashboardLayout from "../../components/dashboard/DashboardLayout"
import ArtistsArea from "../../components/search/ArtistsArea"
import SearchButtonArea from "../../components/search/ButtonArea"
import SearchArea from "../../components/search/SearchArea"
import { SearchContentWrapper } from "../../components/search/SearchContentWrapper/styles"

const Search: NextPage = () => (
  <DashboardLayout>
    <SearchContentWrapper>
      <SearchArea />
      <SearchButtonArea />
      <ArtistsArea />
    </SearchContentWrapper>
  </DashboardLayout>
)

export default Search
