import type { NextPage } from "next"
import DesktopLayout from "../components/auth/DesktopLayout"
import SearchButtonArea from "../components/search/ButtonArea"
import SearchArea from "../components/search/SearchArea"
import { SearchContentWrapper } from "../components/search/SearchContentWrapper/styles"

const Index: NextPage = () => (
  <DesktopLayout>
    <SearchContentWrapper>
      <SearchArea />
      <SearchButtonArea />
    </SearchContentWrapper>
  </DesktopLayout>
)

export default Index
