import { Center, Spinner } from "@chakra-ui/react"
import { useQuery } from "@tanstack/react-query"
import { Heading, Paragraph } from "@tatuaz/ui"
import { FormattedMessage } from "react-intl"
import { api } from "../../../api/apiClient"
import { queryKeys } from "../../../api/queryKeys"
import { theme } from "../../../styles/theme"
import {
  RightSectionContainer,
  TopArtistsSectionHeader,
  TopArtistsSectionViewMore,
  TopArtistsSectionWrapper,
} from "./styles"
import TopArtistsItem from "./TopArtistItem"
import UserSection from "./UserSection"

const ARTISTS_IN_SECTION = 5

const TopArtistsSection = () => {
  const { data, isLoading } = useQuery([queryKeys.getTopArtists], () =>
    api.identity.getTopArtists({
      pageNumber: 1,
      pageSize: ARTISTS_IN_SECTION,
    })
  )

  return (
    <RightSectionContainer>
      <UserSection />
      <TopArtistsSectionWrapper>
        <TopArtistsSectionHeader>
          <Heading level={4}>
            <FormattedMessage defaultMessage="Topowi artyści" id="J5nXcl" />
          </Heading>
          <TopArtistsSectionViewMore>
            <Paragraph color={theme.colors.primary} level={4}>
              <FormattedMessage defaultMessage="Zobacz więcej" id="sP2Svl" />
            </Paragraph>
          </TopArtistsSectionViewMore>
        </TopArtistsSectionHeader>
        <div>
          {isLoading ? (
            <Center mt={8}>
              <Spinner size="xl" />
            </Center>
          ) : (
            data?.value?.data?.map((artist) => (
              <TopArtistsItem
                key={artist.username}
                city={artist.city}
                name={artist.username}
                photoUri={artist.foregroundPhotoUri}
              />
            )) ?? (
              <Center>
                <Paragraph level={4}>
                  <FormattedMessage
                    defaultMessage="Aktualnie nie masz żadnych topowych artystów"
                    id="uxKf0p"
                  />
                </Paragraph>
              </Center>
            )
          )}
        </div>
      </TopArtistsSectionWrapper>
    </RightSectionContainer>
  )
}

export default TopArtistsSection
