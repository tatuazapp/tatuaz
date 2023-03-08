import { ChevronDownIcon } from "@chakra-ui/icons"
import { Button, Menu, MenuButton, MenuItem, MenuList } from "@chakra-ui/react"
import { Paragraph } from "@tatuaz/ui"
import { useState } from "react"
import { theme } from "../../../../styles/theme"
import {
  ArtistsPostSectionButtonAreaWrapper,
  LeftContainer,
  TypeButton,
  TypeButtonsContainer,
} from "./styles"

const ArtistsPostSectionButtonArea = () => {
  const [selectedType, setSelectedType] = useState("All")
  const [currentPostSelectionOption, setCurrentPostSelectionOption] =
    useState("Popular")

  const buttonTypes = ["All", "Photos", "Posts"]
  const postSelectionOptions = ["Popular", "Newest", "Oldest"]

  return (
    <ArtistsPostSectionButtonAreaWrapper>
      <LeftContainer>
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
            {currentPostSelectionOption}
          </MenuButton>
          <MenuList
            backgroundColor={theme.colors.background1}
            borderColor={theme.colors.background2}
          >
            {postSelectionOptions
              .filter((x) => x !== currentPostSelectionOption)
              .map((option) => (
                <MenuItem
                  key={option}
                  _hover={{ backgroundColor: theme.colors.background2 }}
                  backgroundColor={theme.colors.background1}
                  color={theme.colors.secondary}
                  onClick={() => {
                    setCurrentPostSelectionOption(option)
                  }}
                >
                  {option}
                </MenuItem>
              ))}
          </MenuList>
        </Menu>
      </LeftContainer>
      <TypeButtonsContainer>
        {buttonTypes.map((buttonType) => (
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
            >
              {buttonType}
            </Paragraph>
          </TypeButton>
        ))}
      </TypeButtonsContainer>
    </ArtistsPostSectionButtonAreaWrapper>
  )
}

export default ArtistsPostSectionButtonArea
