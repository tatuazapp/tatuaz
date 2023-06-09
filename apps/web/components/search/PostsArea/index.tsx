import { SkeletonText } from "@chakra-ui/react"
import { FunctionComponent } from "react"
import InfiniteScroll from "react-infinite-scroll-component"
import { BriefPostDto } from "../../../api/tatuazApi"
import ArtistPost from "../../dashboard/ArtistsPostSection/ArtistPost"
import { PostCardAreaWrapper } from "./styles"

type PostsAreaProps = {
  items: BriefPostDto[]
  isLoading?: boolean
  fetchNextPage: () => void
  hasNextPage?: boolean
}

const PostsArea: FunctionComponent<PostsAreaProps> = ({
  items,
  isLoading,
  fetchNextPage,
  hasNextPage,
}) => (
  <PostCardAreaWrapper>
    <InfiniteScroll
      dataLength={items.length}
      hasMore={hasNextPage || false}
      loader={
        <SkeletonText isLoaded={!isLoading} noOfLines={5} skeletonHeight={10} />
      }
      next={fetchNextPage}
    >
      {items.map((post) => (
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
    </InfiniteScroll>
  </PostCardAreaWrapper>
)
export default PostsArea
