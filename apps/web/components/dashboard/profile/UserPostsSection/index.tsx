import { Center, SkeletonText, Spinner, VStack } from "@chakra-ui/react"
import { useInfiniteQuery } from "@tanstack/react-query"
import { FunctionComponent } from "react"
import InfiniteScroll from "react-infinite-scroll-component"
import { FormattedMessage } from "react-intl"
import { api } from "../../../../api/apiClient"
import { queryKeys } from "../../../../api/queryKeys"
import ArtistPost from "../../ArtistsPostSection/ArtistPost"
import { USER_POST_WIDTH_LG } from "../../ArtistsPostSection/ArtistPost/styles"

type UserPostsSectionProps = {
  currentUser: string
  isEnabled: boolean
  isCurrentUser: boolean
}

const POST_PAGE_SIZE = 10

const UserPostsSection: FunctionComponent<UserPostsSectionProps> = ({
  currentUser,
  isEnabled,
  isCurrentUser,
}) => {
  const {
    data: userPosts,
    fetchNextPage,
    hasNextPage,
    isLoading,
  } = useInfiniteQuery(
    [queryKeys.getUserPosts, currentUser],
    ({ pageParam = 1 }) =>
      api.post.getUserPosts({
        username: currentUser,
        pageNumber: pageParam,
        pageSize: POST_PAGE_SIZE,
      }),
    {
      enabled: isEnabled,
      getNextPageParam: (lastPage) => {
        const nextPage = lastPage.value?.pageNumber ?? 0
        return nextPage < lastPage.value?.totalPages ? nextPage + 1 : undefined
      },
    }
  )

  const posts = userPosts?.pages?.flatMap((page) => page.value?.data ?? [])

  return (
    <SkeletonText
      isLoaded={!isLoading}
      mt={isCurrentUser ? 12 : 0}
      noOfLines={5}
      skeletonHeight={10}
    >
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
        <VStack gap={8} width={{ base: "100%", lg: USER_POST_WIDTH_LG }}>
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
    </SkeletonText>
  )
}

export default UserPostsSection
