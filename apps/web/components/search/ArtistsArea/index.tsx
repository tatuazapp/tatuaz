import { SkeletonText } from "@chakra-ui/react"
import { FunctionComponent } from "react"
import InfiniteScroll from "react-infinite-scroll-component"
import { BriefArtistDto } from "../../../api/tatuazApi"
import formatCDNImageUrl from "../../../utils/format/formatCDNImageUrl"
import ArtistCard from "./ArtistCard"
import { ArtistCardAreaWrapper } from "./styles"

type ArtistsAreaProps = {
  items: BriefArtistDto[]
  isLoading?: boolean
  fetchNextPage: () => void
  hasNextPage?: boolean
}

const ArtistsArea: FunctionComponent<ArtistsAreaProps> = ({
  items,
  isLoading,
  fetchNextPage,
  hasNextPage,
}) => (
  <ArtistCardAreaWrapper>
    <InfiniteScroll
      dataLength={items.length}
      hasMore={hasNextPage || false}
      loader={
        <SkeletonText isLoaded={!isLoading} noOfLines={5} skeletonHeight={10} />
      }
      next={fetchNextPage}
    >
      {items.map((item) => (
        <ArtistCard
          key={item.username}
          artistDescription={item.bio ?? item.city ?? ""}
          artistName={item.username}
          foregroundPhotoUrl={formatCDNImageUrl(item.foregroundPhotoUri ?? "", {
            maxWidth: 256,
          })}
        />
      ))}
    </InfiniteScroll>
  </ArtistCardAreaWrapper>
)
export default ArtistsArea
