import { Paragraph } from "@tatuaz/ui"
import { useRouter } from "next/router"
import { FunctionComponent } from "react"
import formatCDNImageUrl from "../../../../utils/format/formatCDNImageUrl"
import {
  ArtistPhoto,
  ArtistWrapper,
  TopArtistsSectionArtistListItem,
  VisitArtistIcon,
} from "./styles"

type TopArtistsItemProps = {
  name: string | undefined
  photoUri: string | undefined | null
  city: string | undefined | null
}

const TopArtistsItem: FunctionComponent<TopArtistsItemProps> = ({
  name,
  photoUri,
  city,
}) => {
  const router = useRouter()

  return (
    <TopArtistsSectionArtistListItem>
      <ArtistWrapper>
        <ArtistPhoto
          photoUrl={formatCDNImageUrl(photoUri ?? "", {
            minWidth: 64,
            maxWidth: 256,
          })}
        />
        <div>
          <Paragraph level={2}>{name}</Paragraph>
          <Paragraph level={4}>{city}</Paragraph>
        </div>
      </ArtistWrapper>

      <VisitArtistIcon
        role="button"
        tabIndex={0}
        onClick={() =>
          router.push(
            `/dashboard/profile?${new URLSearchParams({
              profileName: name ?? "",
            }).toString()}`
          )
        }
      />
    </TopArtistsSectionArtistListItem>
  )
}

export default TopArtistsItem
