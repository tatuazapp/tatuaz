import { useInfiniteQuery } from "@tanstack/react-query"
import type { NextPage } from "next"
import { useState } from "react"
import { api } from "../../api/apiClient"
import { queryKeys } from "../../api/queryKeys"
import { SearchPostsFlag } from "../../api/tatuazApi"
import DashboardLayout from "../../components/dashboard/DashboardLayout"
import ArtistsArea from "../../components/search/ArtistsArea"
import SearchButtonArea from "../../components/search/ButtonArea"
import PostsArea from "../../components/search/PostsArea"
import SearchArea from "../../components/search/SearchArea"
import { SearchContentWrapper } from "../../components/search/SearchContentWrapper/styles"
import { contentButton } from "../../types/contentButton"

const SEARCH_PAGE_SIZE = 10
const buttonTypes: contentButton[] = ["Artists", "Posts"]

const Search: NextPage = () => {
  const [searchedPhrase, setSearchedPhrase] = useState<string>("")
  const [selectedType, setSelectedType] = useState<contentButton>(
    buttonTypes[0]
  )
  const [searchButtonClicked, setSearchButtonClicked] = useState<boolean>(false)

  const {
    data: artistsData,
    fetchNextPage: artistsFetchNextPage,
    hasNextPage: artistsHasNextPage,
    isLoading: artistsIsLoading,
  } = useInfiniteQuery(
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

  const {
    data: searchArtistsData,
    fetchNextPage: searchArtistsFetchNextPage,
    hasNextPage: searchArtistsHasNextPage,
    isLoading: searchArtistsIsLoading,
  } = useInfiniteQuery(
    [queryKeys.searchUsers],
    ({ pageParam = 1 }) =>
      api.identity.searchUsers({
        pageNumber: pageParam,
        pageSize: SEARCH_PAGE_SIZE,
        query: searchedPhrase,
        onlyArtists: true,
      }),
    {
      getNextPageParam: (lastPage) =>
        lastPage.value.pageNumber < lastPage.value.totalPages
          ? lastPage.value.pageNumber + 1
          : undefined,
    }
  )

  const {
    data: postsData,
    fetchNextPage: postsFetchNextPage,
    hasNextPage: postsHasNextPage,
    isLoading: postsIsLoading,
  } = useInfiniteQuery(
    [queryKeys.getPostFeed],
    ({ pageParam = 1 }) =>
      api.post.getPostFeed({
        searchPostsFlag: SearchPostsFlag.All,
        pageNumber: pageParam,
        pageSize: SEARCH_PAGE_SIZE,
      }),
    {
      getNextPageParam: (lastPage) => {
        const nextPage = lastPage.value?.pageNumber ?? 0
        return nextPage < lastPage.value?.totalPages ? nextPage + 1 : undefined
      },
    }
  )

  const {
    data: searchPostsData,
    fetchNextPage: searchPostsFetchNextPage,
    hasNextPage: searchPostsHasNextPage,
    isLoading: searchPostsIsLoading,
  } = useInfiniteQuery(
    [queryKeys.searchPosts],
    ({ pageParam = 1 }) =>
      api.post.searchPosts({
        pageNumber: pageParam,
        pageSize: SEARCH_PAGE_SIZE,
        query: searchedPhrase,
        searchPostsFlag: SearchPostsFlag.All,
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
        <SearchArea
          searchPhrase={searchedPhrase}
          setSearchButtonClicked={setSearchButtonClicked}
          setSearchPhrase={setSearchedPhrase}
        />
        <SearchButtonArea
          buttonTypes={buttonTypes}
          selectedType={selectedType}
          setSearchButtonClicked={setSearchButtonClicked}
          setSelectedType={setSelectedType}
        />
        {selectedType === buttonTypes[0] && !searchButtonClicked && (
          <ArtistsArea
            fetchNextPage={artistsFetchNextPage}
            hasNextPage={artistsHasNextPage}
            isLoading={artistsIsLoading}
            items={artistsData?.pages?.flatMap((page) => page.value.data) ?? []}
          />
        )}
        {selectedType === buttonTypes[0] && searchButtonClicked && (
          <ArtistsArea
            fetchNextPage={searchArtistsFetchNextPage}
            hasNextPage={searchArtistsHasNextPage}
            isLoading={searchArtistsIsLoading}
            items={
              searchArtistsData?.pages?.flatMap((page) => page.value.data) ?? []
            }
          />
        )}

        {selectedType === buttonTypes[1] && !searchButtonClicked && (
          <PostsArea
            fetchNextPage={postsFetchNextPage}
            hasNextPage={postsHasNextPage}
            isLoading={postsIsLoading}
            items={postsData?.pages?.flatMap((page) => page.value.data) ?? []}
          />
        )}

        {selectedType === buttonTypes[1] && searchButtonClicked && (
          <PostsArea
            fetchNextPage={searchPostsFetchNextPage}
            hasNextPage={searchPostsHasNextPage}
            isLoading={searchPostsIsLoading}
            items={
              searchPostsData?.pages?.flatMap((page) => page.value.data) ?? []
            }
          />
        )}
      </SearchContentWrapper>
    </DashboardLayout>
  )
}

export default Search
