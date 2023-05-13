import { useInfiniteQuery } from "@tanstack/react-query"
import type { NextPage } from "next"
import { api } from "../../api/apiClient"
import { queryKeys } from "../../api/queryKeys"
import DashboardLayout from "../../components/dashboard/DashboardLayout"
import ArtistsArea from "../../components/search/ArtistsArea"
import SearchButtonArea from "../../components/search/ButtonArea"
import SearchArea from "../../components/search/SearchArea"
import { SearchContentWrapper } from "../../components/search/SearchContentWrapper/styles"

const SEARCH_PAGE_SIZE = 10

const Search: NextPage = () => {
  const { data, fetchNextPage, hasNextPage, isLoading } = useInfiniteQuery(
    [queryKeys.getTopArtists],
    ({ pageParam = 1 }) =>
      api.identity.getTopArtists({
        pageNumber: pageParam,
        pageSize: SEARCH_PAGE_SIZE,
      }),
    {
      getNextPageParam: (lastPage) =>
        lastPage.value.pageNumber < lastPage.value.totalPages
          ? lastPage.value.pageNumber + 1
          : undefined,
    }
  )

  return (
    <DashboardLayout>
      <SearchContentWrapper>
        <SearchArea />
        <SearchButtonArea />
        <ArtistsArea
          fetchNextPage={fetchNextPage}
          hasNextPage={hasNextPage}
          isLoading={isLoading}
          items={data?.pages?.flatMap((page) => page.value.data) ?? []}
        />
      </SearchContentWrapper>
    </DashboardLayout>
  )
}

export default Search
