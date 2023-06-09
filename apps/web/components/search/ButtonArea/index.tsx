import { ChevronDownIcon } from "@chakra-ui/icons"
import { Menu, MenuButton, Button, MenuList, MenuItem } from "@chakra-ui/react"
import { Paragraph } from "@tatuaz/ui"
import { FunctionComponent } from "react"
import { theme } from "../../../styles/theme"
import { contentButton } from "../../../types/contentButton"
import useIsSmallPhone from "../../../utils/hooks/useIsSmallMobile"
import { TypeButton, SearchButtonsWrapper } from "./styles"

type SearchButtonAreaProps = {
  buttonTypes: contentButton[]
  selectedType: contentButton
  setSelectedType: (type: contentButton) => void
  setSearchButtonClicked: (searchButtonClicked: boolean) => void
}

const SearchButtonArea: FunctionComponent<SearchButtonAreaProps> = ({
  buttonTypes,
  selectedType,
  setSelectedType,
  setSearchButtonClicked,
}) => {
  const isSmallMobile = useIsSmallPhone()

  return (
    <SearchButtonsWrapper>
      {isSmallMobile ? (
        <Menu>
          <MenuButton
            _expanded={{ backgroundColor: theme.colors.background1 }}
            _hover={{ backgroundColor: theme.colors.background1 }}
            as={Button}
            backgroundColor={theme.colors.background1}
            borderColor={theme.colors.secondary}
            borderRadius={theme.radius.small}
            borderWidth={1}
            color={theme.colors.secondary}
            fontSize={theme.sizes.small}
            fontWeight={500}
            paddingBottom={theme.space.xxsmall}
            paddingTop={theme.space.xxsmall}
            rightIcon={<ChevronDownIcon fontSize={theme.space.large} />}
          >
            {selectedType}
          </MenuButton>
          <MenuList
            backgroundColor={theme.colors.background1}
            borderColor={theme.colors.background2}
          >
            {buttonTypes
              .filter((x) => x !== selectedType)
              .map((option) => (
                <MenuItem
                  key={option}
                  _hover={{ backgroundColor: theme.colors.background2 }}
                  backgroundColor={theme.colors.background1}
                  color={theme.colors.secondary}
                  onClick={() => {
                    setSelectedType(option)
                    setSearchButtonClicked(false)
                  }}
                >
                  {option}
                </MenuItem>
              ))}
          </MenuList>
        </Menu>
      ) : (
        buttonTypes.map((buttonType) => (
          <TypeButton
            key={buttonType}
            isSelected={selectedType === buttonType}
            onClick={() => {
              setSelectedType(buttonType)
            }}
          >
            <Paragraph
              color={
                selectedType === buttonType
                  ? theme.colors.background1
                  : theme.colors.secondary
              }
              level={1}
            >
              {buttonType}
            </Paragraph>
          </TypeButton>
        ))
      )}
    </SearchButtonsWrapper>
  )
}
export default SearchButtonArea
