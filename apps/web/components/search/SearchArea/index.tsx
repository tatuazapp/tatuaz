import { Button } from "@chakra-ui/react"
import { ArrowUpRight } from "@styled-icons/bootstrap"
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

const SearchArea = () => {
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
      <SearchInput placeholder={placeholder.props.defaultMessage} type="text" />
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
