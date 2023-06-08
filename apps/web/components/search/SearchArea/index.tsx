import { Button } from "@chakra-ui/react"
import { ArrowUpRight } from "@styled-icons/bootstrap"
import { FunctionComponent } from "react"
import { FormattedMessage } from "react-intl"
import { theme } from "../../../styles/theme"
import useIsMobile from "../../../utils/hooks/useIsMobile"
import useIsPhone from "../../../utils/hooks/useIsPhone"
import UserSection from "../../dashboard/TopArtistsSection/UserSection"
import {
  SearchInput,
  SearchAreaWrapper,
  UserArea,
  MobileSearchIcon,
} from "./styles"

type SearchAreaProps = {
  searchPhrase: string
  setSearchPhrase: (searchPhrase: string) => void
  setSearchButtonClicked: (searchButtonClicked: boolean) => void
}

const SearchArea: FunctionComponent<SearchAreaProps> = ({
  searchPhrase,
  setSearchPhrase,
  setSearchButtonClicked,
}) => {
  const placeholder = (
    <FormattedMessage
      defaultMessage="Znajdź artystę, treść, itp."
      id="sVRdVG"
    />
  )

  const isMobile = useIsMobile()
  const isPhone = useIsPhone()

  return (
    <SearchAreaWrapper>
      <SearchInput
        placeholder={placeholder.props.defaultMessage}
        type="text"
        value={searchPhrase}
        onChange={(e) => {
          setSearchPhrase(e.target.value)
        }}
      />
      {isPhone ? (
        <MobileSearchIcon />
      ) : (
        <Button
          borderRadius={theme.space.small}
          color={theme.colors.background1}
          colorScheme="primary"
          height={{ base: "auto", md: theme.sizes.xxlarge }}
          marginLeft={{ base: "0", md: theme.space.xlarge }}
          rightIcon={<ArrowUpRight size={24} />}
          size="lg"
          onClick={() => {
            setSearchButtonClicked(searchPhrase !== "")
          }}
        >
          <FormattedMessage defaultMessage="Szukaj" id="xyzes5" />
        </Button>
      )}
      {!isMobile && (
        <UserArea>
          <UserSection />
        </UserArea>
      )}
    </SearchAreaWrapper>
  )
}

export default SearchArea
