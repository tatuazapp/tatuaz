import { Center, Spinner, VStack } from "@chakra-ui/react"
import { useInfiniteQuery } from "@tanstack/react-query"
import { useState } from "react"
import InfiniteScroll from "react-infinite-scroll-component"
import { FormattedMessage } from "react-intl"
import { api } from "../../../api/apiClient"
import { queryKeys } from "../../../api/queryKeys"
import { SearchPostsFlag } from "../../../api/tatuazApi"
import ArtistPost from "./ArtistPost"
import ArtistsPostSectionButtonArea from "./ArtistPostSectionButtonsArea"
import { ArtistPostSectionWrapper } from "./styles"

export const POST_PAGE_SIZE = 10

const ArtistsPostSection = () => {
  const [selectedType, setSelectedType] = useState<SearchPostsFlag>(
    SearchPostsFlag.All
  )

  const {
    data: feedPosts,
    fetchNextPage,
    hasNextPage,
  } = useInfiniteQuery(
    [queryKeys.getPostFeed, selectedType],
    ({ pageParam = 1 }) =>
      api.post.getPostFeed({
        searchPostsFlag: selectedType,
        pageNumber: pageParam,
        pageSize: POST_PAGE_SIZE,
      }),
    {
      getNextPageParam: (lastPage) => {
        const nextPage = lastPage.value?.pageNumber ?? 0
        return nextPage < lastPage.value?.totalPages ? nextPage + 1 : undefined
      },
    }
  )

  const posts = feedPosts?.pages?.flatMap((page) => page.value?.data ?? [])

  return (
    <ArtistPostSectionWrapper>
      <ArtistsPostSectionButtonArea
        selectedType={selectedType}
        setSelectedType={setSelectedType}
      />
      <InfiniteScroll
        dataLength={posts?.length ?? 0}
        endMessage={
          <Center mb={10} mt={10}>
            <FormattedMessage
              defaultMessage="Ojej, nie ma już więcej postów 😥"
              id="wUsjqT"
            />
          </Center>
        }
        hasMore={!!hasNextPage}
        loader={
          <Center mb={10} mt={10}>
            <Spinner />
          </Center>
        }
        next={fetchNextPage}
      >
        <VStack gap={8} mt={12} width="100%">
          {posts?.map((post) => (
            <ArtistPost
              key={post.id}
              author={{
                name: post.authorName,
                photoUri: post.authorPhotoUri ?? "",
              }}
              commentsNumber={post.commentsCount}
              createdAt={post.createdAt as unknown as string}
              description={post.description}
              id={post.id}
              isLiked={post.isLikedByCurrentUser}
              likesNumber={post.likesCount}
              photoUris={post.photoUris}
            />
          ))}
        </VStack>
      </InfiniteScroll>
    </ArtistPostSectionWrapper>
  )
}

export default ArtistsPostSection
